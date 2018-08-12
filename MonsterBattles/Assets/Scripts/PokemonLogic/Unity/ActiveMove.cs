using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetableElement))]
public class ActiveMove : BattleElement
{

    public class ActiveMoveData
    {
        //Should be given before instantiate
        public int basePower = -1;
        public Globals.Type moveType = Globals.Type.Unknown;
        public bool isExternal = false;

        //Init on ActiveMoveDataInit
        public MoveData moveData;
        public string moveId;
        public int moveSlotId = -1;
        public int moveSlotOption = -1;

        //Zinfo
        public bool zPowered = false;
        public bool zBrokeProtect = false;


        //Used in damageCalculation
        public float stab = -1;
        public int typeMod = 0;
        public bool crit = false;

        //For damage Calculation
        public int pokemonLevel = 1;
        public Globals.BoostsTable pokemonBoosts;
        public Globals.StatsTable pokemonStats;
        public bool pokemonWasInWonderRoom = false;

        //For modifier Calculation
        public float weatherModifier;
        public float randomModifier;
        public float stabModifier;

        public int accuracy = 100; //<0 if it allways hits (swords dance, aerial ace)  | >= 0 if it has a percentage

        //Moar stuff
        public bool negateSecondary = false;
        public bool hasSheerForce = false;
        public bool pranksterBoosted = false;

        public int totalDamage = 0;



        public ActiveMoveData(string moveId)
        {
            this.moveId = moveId;
            this.moveData = Moves.BattleMovedex[moveId].DeepCopy();
            this.basePower = this.moveData.basePower;
            this.moveType = this.moveData.type;
        }

        public void ReSetData(string moveId)
        {
            this.moveId = moveId;
            this.moveData = Moves.BattleMovedex[moveId].DeepCopy();
        }
    }

    [HideInInspector]
    public Battle battle;
    [HideInInspector]
    public TargetableElement targetScript;
    [HideInInspector]
    public PokemonCharacter source;
    [HideInInspector]
    public ActiveMoveData activeData;
    [HideInInspector]
    public Pokemon.TargetLocation targetLocation;



    public void Init(Battle battle, PokemonCharacter source, ActiveMoveData activeData, Pokemon.TargetLocation targetLocation)
    {
        this.battle = battle;
        this.battle.AddNewMove(this);
        targetScript = GetComponent<TargetableElement>();
        targetScript.sourceElement = this;
        this.source = source;
        this.activeData = activeData;
        this.targetLocation = targetLocation;

        PrepareDamageData();
        OnCreatedMove();
    }

    public override EffectData GetInfoEffect()
    {
        if (!Moves.BattleMovedex.ContainsKey(id)) return null;
        return Moves.BattleMovedex[id];
    }

    void PrepareDamageData()
    {
        activeData.pokemonLevel = source.pokemonData.level;
        activeData.pokemonBoosts = source.pokemonData.boosts.ShallowCopy();
        activeData.pokemonStats = source.pokemonData.stats.ShallowCopy();
        activeData.pokemonWasInWonderRoom = source.pokemonData.inWonderRoom;

        Battle.RelayVar relayVar;

        //Weather modifier
        relayVar = new Battle.RelayVar(integerValue: 100);
        relayVar = battle.RunEvent("WeatherModifyDamage", source.targetScript, null, activeData.moveData, relayVar);
        activeData.weatherModifier = relayVar.integerValue/100f;

        //Not a modifier
        activeData.randomModifier = RandomScript.Randomizer(100)/100f;

        //STAB
        if (source.pokemonData.HasType(activeData.moveType))
        {
            activeData.stabModifier = ((activeData.stab != -1) ? activeData.stab : 1.5f);
        }
        else activeData.stabModifier = 1;

        bool hitResult = !battle.SingleEvent("PrepareHit", activeData.moveData, null, null, source, activeData.moveData).getEndEvent();
        //if (!hitResult) return false;

        battle.RunEvent("PrepareHit", source.targetScript, null, activeData.moveData);

    }


    //This has to be called after immunities, setting zbroke protect and all of the tryhit and movehit things 
    int GetDamage(Pokemon target)
    {
        MoveData moveData = activeData.moveData;
        Battle.RelayVar relayVar;


        /*RETURN DIRECT DAMAGE*/
        if (moveData.ohko != Globals.OHKO.Null) return target.maxhp; //Fissure, Sheer cold
        if (moveData.eventMethods.damageCallback != null) //mirro coat, naturesmaddnes, hyper fang...
        {
            relayVar = new Battle.RelayVar();
            moveData.eventMethods.StartCallback("damageCallback", battle, relayVar, target.targetData, source, null);
            return relayVar.integerValue;
        }
        if (moveData.damageByLevel) return activeData.pokemonLevel; //nightshade, seismic toss
        if (moveData.damage != -1) return moveData.damage; //Dragon rage, sonic boom

        /*USING BASE POWER*/
        //Category
        Globals.MoveCategory category = (moveData.category == Globals.MoveCategory.Null) ? Globals.MoveCategory.Physical : moveData.category;
        if (moveData.defensiveCategory == Globals.MoveCategory.Null) category = moveData.defensiveCategory;

        //BasePower
        int basePower = activeData.basePower;
        if (moveData.eventMethods.basePowerCallback != null)
        {
            relayVar = new Battle.RelayVar();
            moveData.eventMethods.StartCallback("basePowerCallback", battle, relayVar, target.targetData, source, moveData);
            basePower = relayVar.integerValue;
        }
        if (basePower < 0) return -1; //Return -1 means no dealing damage
        basePower = Mathf.Max(1, basePower); //Min value will be 1

        //CritRatio
        int[] critMultiplier = { 0, 24, 8, 2, 1 };
        relayVar = new Battle.RelayVar(integerValue: moveData.critRatio);
        relayVar = battle.RunEvent("ModifyCritRatio", source.targetScript, target.myPokemon, moveData, relayVar);
        int critRatio = Mathf.Clamp(relayVar.integerValue, 0, 4);

        //Set crit
        activeData.crit = moveData.willCrit;
        if (!activeData.crit)
        {
            activeData.crit = RandomScript.RandomChance(1, critMultiplier[critRatio]);
        }
        if (activeData.crit)
        {
            relayVar = new Battle.RelayVar(booleanValue: activeData.crit);
            relayVar = battle.RunEvent("CriticalHit", target.targetData, null, moveData);
            activeData.crit = relayVar.booleanValue;
        }

        //Happens after crit calculation
        relayVar = new Battle.RelayVar(integerValue: basePower);
        relayVar = battle.RunEvent("BasePower", source.targetScript, target.myPokemon, moveData, relayVar, true);
        if (relayVar.getEndEvent() && relayVar.integerValue != -1) return 0;
        basePower = Mathf.Max(1, relayVar.integerValue);

        //Starting?
        int level = activeData.pokemonLevel;
        Pokemon defender = target;
        string attackStat = (category == Globals.MoveCategory.Physical) ? "Atk" : "SpA";
        string defenseStat = (category == Globals.MoveCategory.Physical) ? "Def" : "SpD";

        //statTable
        int attack;
        int defense;

        int atkBoosts = (moveData.useTargetOffensive) ? defender.boosts.GetBoostValue(attackStat) : activeData.pokemonBoosts.GetBoostValue(attackStat);
        int defBoosts = (moveData.useSourceDefensive) ? activeData.pokemonBoosts.GetBoostValue(defenseStat) : defender.boosts.GetBoostValue(defenseStat);

        bool ignoreNegativeOffensive = moveData.ignoreNegativeOffensive;
        bool ignorePositiveDefensive = moveData.ignorePositiveDefensive;

        if (activeData.crit)
        {
            ignoreNegativeOffensive = true;
            ignorePositiveDefensive = true;
        }

        bool ignoreOffensive = moveData.ignoreOffensive || (ignoreNegativeOffensive && atkBoosts < 0);
        bool ignoreDefensive = moveData.ignoreDefensive || (ignorePositiveDefensive && defBoosts > 0);

        if (ignoreOffensive)
        {
            //this.debug('Negating (sp)atk boost/penalty.');
            atkBoosts = 0;
        }

        if (ignoreDefensive)
        {
            //this.debug('Negating (sp)def boost/penalty.');
            defBoosts = 0;
        }

        if (moveData.useTargetOffensive) attack = defender.CalculateStat(attackStat, atkBoosts);
        else attack = CalculateMoveStat(attackStat, atkBoosts);

        if (moveData.useSourceDefensive) defense = CalculateMoveStat(defenseStat, defBoosts);
        else defense = defender.CalculateStat(defenseStat, defBoosts);

        //Apply stat modifiers
        relayVar = new Battle.RelayVar(integerValue: attack);
        relayVar = battle.RunEvent("Modify" + attackStat, source.targetScript, defender.myPokemon, moveData, relayVar);
        attack = relayVar.integerValue;

        relayVar = new Battle.RelayVar(integerValue: defense);
        relayVar = battle.RunEvent("Modify" + defenseStat, defender.targetData, source, moveData, relayVar);
        defense = relayVar.integerValue;

        //int(int(int(2 * L / 5 + 2) * A * P / D) / 50);        
        int baseDamage = Mathf.FloorToInt(Mathf.FloorToInt(Mathf.FloorToInt(2f * level / 5f + 2f) * basePower * attack / defense) / 50f);

        return ModifyDamage(baseDamage, target);
    }

    int ModifyDamage(int baseDamage, Pokemon target)
    {
        Globals.Type type = activeData.moveType;
        baseDamage += 2;

        Battle.RelayVar relayVar;

        //Weather modifier calculated on the beginning
        baseDamage = Mathf.FloorToInt(baseDamage * activeData.weatherModifier);

        //crit
        if (activeData.crit) baseDamage = Mathf.FloorToInt(baseDamage * 1.5f);

        //Not a modifier calculated on the beginning
        baseDamage = Mathf.FloorToInt(baseDamage * activeData.randomModifier);

        //STAB calculated on the beginning
        baseDamage = Mathf.FloorToInt(baseDamage * activeData.stabModifier);

        //Types
        activeData.typeMod = Mathf.Clamp(target.RunEffectiveness(this), -6, 6);
        if (activeData.typeMod > 0)
        {
            for (int i = 0; i < activeData.typeMod; i++)
            {
                baseDamage *= 2;
            }
        }

        if (activeData.typeMod < 0)
        {
            for (int i = 0; i > activeData.typeMod; i--)
            {
                baseDamage = Mathf.FloorToInt(baseDamage / 2f);
            }
        }

        //Burn Status 
        if (source.pokemonData.statusId == "brn" && activeData.moveData.category == Globals.MoveCategory.Physical && !source.pokemonData.HasAbilityActive(new string[] { "guts" }))
        {
            if (activeData.moveId != "facade") baseDamage = Mathf.FloorToInt(baseDamage * 0.5f);
        }

        // Final modifier. Modifiers that modify damage after min damage check, such as Life Orb.
        relayVar = new Battle.RelayVar(integerValue: baseDamage);
        relayVar = battle.RunEvent("ModifyDamage", source.targetScript, target.myPokemon, activeData.moveData, relayVar);
        baseDamage = relayVar.integerValue;

        //Z breaking protect
        if (activeData.zPowered && activeData.zBrokeProtect)
        {
            baseDamage = Mathf.FloorToInt(baseDamage * 0.25f);
        }

        if (baseDamage < 1) baseDamage = 1;

        return Mathf.FloorToInt(baseDamage);
    }


    int CalculateMoveStat(string statName, int boost = 0, int modifier = 1)
    {
        statName = Globals.getId(statName);
        if (statName == "hp") return source.pokemonData.maxhp;

        //Should be baseStat???
        int stat = activeData.pokemonStats.GetStatValue(statName);

        if (activeData.pokemonWasInWonderRoom)
        {
            if (statName == "def") stat = activeData.pokemonStats.GetStatValue("spd");
            else if (statName == "spd") stat = activeData.pokemonStats.GetStatValue("def");
        }

        //BOOST
        Globals.BoostsTable boosts = new Globals.BoostsTable();
        boosts.SetBoostValue(statName, boost);
        Battle.RelayVar relayVar = new Battle.RelayVar(boosts: boosts);
        relayVar = battle.RunEvent("ModifyBoost", source.targetScript, null, null, relayVar);
        boost = relayVar.boosts.GetBoostValue(statName);
        boost = Mathf.Clamp(boost, -6, 6);

        if (boost >= 0) stat = Mathf.FloorToInt(stat * boosts.boostTable[boost]);
        else stat = Mathf.FloorToInt(stat / boosts.boostTable[-boost]);
        //END BOOST

        //stat modifier
        stat = stat * modifier; //if float problems check showdown github

        return stat;
    }

    void OnCreatedMove()
    {
        //if (sourceEffect) move.sourceEffect = sourceEffect.id;

        Battle.RelayVar relayVar = new Battle.RelayVar(effectValue: activeData.moveData);

        battle.SingleEvent("ModifyMove", activeData.moveData, null, source.targetScript, null, activeData.moveData, relayVar);
        activeData.moveData = (MoveData) battle.RunEvent("ModifyMove", source.targetScript, null, activeData.moveData, relayVar).effectValue;

        /*ZMOVE SET STUFF*/
        if(activeData.zPowered)
        {
            if(activeData.moveData.zMoveBoost != null)
            {
                battle.Boost(activeData.moveData.zMoveBoost, source.targetScript, source, new EffectData(id: "zpower"));
            } else if(activeData.moveData.zMoveEffect == "heal")
            {
                battle.Heal(source.pokemonData.maxhp, source.targetScript, source, new EffectData(id: "zpower"));
            } else if (activeData.moveData.zMoveEffect == "healreplacement")
            {
                activeData.moveData.self = new Globals.SelfEffect(sideConditionId: "healreplacement");
            } else if (activeData.moveData.zMoveEffect == "clearnegativeboost")
            {
                source.pokemonData.boosts.ClearNegatives();
            } else if (activeData.moveData.zMoveEffect == "redirect")
            {
                source.pokemonData.AddVolatile("followme", source, new EffectData(id: "zpower"));
            } else if (activeData.moveData.zMoveEffect == "crit2")
            {
                source.pokemonData.AddVolatile("focusenergy", source, new EffectData(id: "zpower"));
            }
            else if (activeData.moveData.zMoveEffect == "curse")
            {
                if(source.pokemonData.HasType(Globals.Type.Ghost))
                {
                    battle.Heal(source.pokemonData.maxhp, source.targetScript, source, new EffectData(id: "zpower"));
                } else
                {
                    battle.Boost(new Globals.BoostsTable(atk: 1), source.targetScript, source, new EffectData(id: "zpower"));
                }
            }

        }
        /*END ZMOVE STUFF*/

        if(battle.SingleEvent("TryMove", activeData.moveData, null, source.targetScript, null, activeData.moveData).getEndEvent() ||
            battle.RunEvent("TryMove", source.targetScript, null, activeData.moveData).getEndEvent())
        {
            activeData.moveData.mindBlownRecoil = false;
            MakeMoveFail();
        }

        //Singleevent UseMoveMessage (just for magnitude number display)

        if(activeData.moveData.ignoreImmunity == "")
        {
            activeData.moveData.ignoreImmunity = (activeData.moveData.category == Globals.MoveCategory.Status) ? "All" : "";
        }

        if(activeData.moveData.selfdestruct == "always")
        {
            source.pokemonData.Faint(source, activeData.moveData);
        }

        //ACCURACY
        GetAccuracy();
    }

    int GetAccuracy()
    {
        //boosts table
        int accuracy = activeData.moveData.accuracy;
        //boosts, boost
        /*HAS ACCURACY*/
        if(accuracy >= 0)
        {
            if (!activeData.moveData.ignoreAccuracy)
            {
                float[] boostTable = { 1f, 4f / 3f, 5f / 3f, 2f, 7f / 3f, 8f / 3f, 3f };

                Battle.RelayVar relayVar = new Battle.RelayVar(boosts: source.pokemonData.boosts.ShallowCopy());
                relayVar = battle.RunEvent("ModifyBoost", source.targetScript, null, null, relayVar);
                int boost = Mathf.Clamp(relayVar.boosts.GetBoostValue("accuracy"), -6, 6);
                if (boost > 0) accuracy = Mathf.FloorToInt(accuracy * boostTable[boost]);
                else accuracy = Mathf.FloorToInt(accuracy / boostTable[-boost]);
            }

            //ignoreEvasion here (not needed)

        }

        /*IS OHKO*/
        if (activeData.moveData.ohko != Globals.OHKO.Null)
        {
            accuracy = 30;
            if(activeData.moveData.ohko != Globals.OHKO.Ice && source.pokemonData.HasType(Globals.Type.Ice))
            {
                accuracy = 20;
            }

            //accuracy by level here (not used)
        } else
        {
            Battle.RelayVar relayVar = new Battle.RelayVar(integerValue: accuracy);
            accuracy = battle.RunEvent("ModifyAccuracy", source.targetScript, null, activeData.moveData, relayVar).integerValue;
        }

        /*ALWAYSHITS*/
        if (activeData.moveData.id == "toxic" && source.pokemonData.HasType(Globals.Type.Poison)) accuracy = -1;
        else
        {
            Battle.RelayVar relayVar = new Battle.RelayVar(integerValue: accuracy);
            accuracy = battle.RunEvent("Accuracy", source.targetScript, null, activeData.moveData, relayVar).integerValue;
        }

        accuracy = Mathf.Max(accuracy, 1);
        activeData.accuracy = accuracy;

        return accuracy;
    }

    protected void MakeMoveFail()
    {
        //singleevent MOVEFAIL
        //move fail animations
    }

    /*
     if some targets
        damage = tryhitmove
        if damage stuff moveResult = true
        if(has selfboost && moveresult) moveHit
        
         
     */

    void MoveSuccessAfterEffects(TargetableElement target)
    {
        
        if (source.pokemonData.hp <= 0) {
            source.pokemonData.Faint(source, activeData.moveData);
        }

        if (!activeData.negateSecondary && !(activeData.hasSheerForce && source.pokemonData.HasAbilityActive(new string[]{ "sheerforce"})))
        {
            BattleElement targetSource = (target == null) ? null : target.sourceElement;
            battle.SingleEvent("AfterMoveSecondarySelf", activeData.moveData, null, source.targetScript, targetSource, activeData.moveData);
            battle.RunEvent("AfterMoveSecondarySelf", source.targetScript, targetSource, activeData.moveData);
        }
    }



    //Called when hit a target
    public int TryMoveHit(TargetableElement target)
    {
        activeData.zBrokeProtect = false;
        bool hitResult = true;

        if (battle.SingleEvent("Try", activeData.moveData, null, source.targetScript, target.sourceElement, activeData.moveData).getEndEvent()) return -1;

        //Affecting directly to a side o to the field
        if(target.teamTarget != null || target.battleTarget != null)
        {
            if(target.battleTarget != null)
            {
                hitResult = !battle.RunEvent("TryHitField", target, source, activeData.moveData).getEndEvent();
            } else
            {
                hitResult = !battle.RunEvent("TryHitSide", target, source, activeData.moveData).getEndEvent();
            }

            if (!hitResult) return -1;

            return MoveHit(target);
        }

        //Immunity
        hitResult = !battle.RunEvent("TryImmunity", target, source, activeData.moveData).getEndEvent();
        if (!hitResult) return -1;

        if (activeData.moveData.ignoreImmunity == "")
            activeData.moveData.ignoreImmunity = (activeData.moveData.category == Globals.MoveCategory.Status) ? "All" : "";

        //TryHit
        hitResult = !battle.RunEvent("TryHit", target, source, activeData.moveData).getEndEvent();
        if (!hitResult) return -1;

        //Immunity
        if (((PokemonCharacter)target.sourceElement).pokemonData.HasImmunity(activeData.moveType) &&
            activeData.moveData.ignoreImmunity == "") return -1;

        //Powder Immunity
        if (target.sourceElement is PokemonCharacter && (activeData.moveData.flags & Globals.MoveFlags.Powder) != 0 &&
            target != source.targetScript && TypeChart.HasImmunity("powder", ((PokemonCharacter)target.sourceElement).pokemonData.types)) return -1;

        //Prankster Immunity
        if(activeData.pranksterBoosted && target.sourceElement is PokemonCharacter && source.pokemonData.HasAbilityActive(new string[] {"prankster"}) 
            && ((PokemonCharacter)target.sourceElement).pokemonData.team != source.pokemonData.team 
            && TypeChart.HasImmunity("prankster", ((PokemonCharacter)target.sourceElement).pokemonData.types)
            ) return -1;

        //Now it surely hits!!!

        //Breaks protect
        if(activeData.moveData.breaksProtect)
        {
            //Remove ShieldVolatiles
            //Remove ShieldConditions
        }

        //StealsBoosts
        if(activeData.moveData.stealsBoosts && target.sourceElement is PokemonCharacter)
        {
            Globals.BoostsTable boosts = ((PokemonCharacter)target.sourceElement).pokemonData.boosts.ShallowCopy();
            boosts.ClearNegatives();
            if(boosts.HasPositiveBoosts())
            {
                battle.Boost(boosts, source.targetScript, source);
                ((PokemonCharacter)target.sourceElement).pokemonData.SetBoosts(boosts);
            }
        }

        //If MultiHit (here?)

        //Else
        int damage = MoveHit(target);
        activeData.totalDamage += damage;

        //set to target gotAttacked
        if ((target.sourceElement is PokemonCharacter) && source.pokemonData != ((PokemonCharacter)target.sourceElement).pokemonData)
            ((PokemonCharacter)target.sourceElement).pokemonData.GotAttacked(activeData.moveId, damage, source);

        //return if no damage
        if (damage < 0) return damage;

        //eachevent update
        battle.EventForActives("Update");
        
        //Secondary events
        if((target.sourceElement is PokemonCharacter) && !activeData.negateSecondary && !(activeData.hasSheerForce /*&& source.pokemonData.HasAbilityActive(new string[] { "sheerforce" })*/))
        {
            battle.SingleEvent("AfterMoveSecondary", activeData.moveData, null, target, source, activeData.moveData);
            battle.RunEvent("AfterMoveSecondary", target, source, activeData.moveData);
        }

        return damage;
    }

    protected int MoveHit(TargetableElement target, EffectData moveData = null, bool isSecondary = false, bool isSelf = false)
    {
        int damage = -1;
        if (moveData == null) moveData = activeData.moveData;
        /*
        TryHitField (singleevent)
        TryHitSide (singleevent)
        TryHit (singleevent)
        TryPrimaryHit (runevent)
        */

        if(target.sourceElement is PokemonCharacter)
        {
            int didSomething = -1;
            Pokemon pokeTarget = ((PokemonCharacter)target.sourceElement).pokemonData;
            damage = GetDamage(pokeTarget);
            /*
            selfdestruct stuff
            */
            if(damage >= 0 && !pokeTarget.fainted)
            {
                if(activeData.moveData.noFaint && damage >= pokeTarget.hp)
                {
                    damage = pokeTarget.hp - 1;
                }
                damage = battle.Damage(damage, target, source, activeData.moveData);
                //Damage interrumped
                if (damage <= 0) return -1;
                didSomething = 1;
            }
            /*
            boosts stuff
            heal stuff
            status stuff
            forceStatus stuff
            volatileStatus stuff
            sideCondition stuff
            weather stuff
            terrain stuff
            pseudoWeather stuff
            forceSwitch stuff
            selfSwitch stuff

            //HIT EVENTS
            HitField (singleevent)
            HitSide (singleevent)
            Hit (singleevent)
            Hit (runevent)
            AfterHit (singleevent)
            */
            //if the move didnt do someting return false
            if (didSomething < 0) didSomething = 1;

            if(didSomething == 0 && moveData.self == null &&
                (moveData is MoveData) && ((MoveData)moveData).selfdestruct == "")
            {
                return -1;
            }


        }


        //Move has self && !selfdropped
        //move has secondaries
        if(moveData.secondaries != null)
        {

            Globals.SecondaryEffect[] secondaries = new Globals.SecondaryEffect[moveData.secondaries.Length];
            for(int i = 0; i < secondaries.Length; ++i)
            {
                secondaries[i] = moveData.secondaries[i].DeepCopy();
            }

            Battle.RelayVar relayVar = new Battle.RelayVar(secondaries: secondaries);
            secondaries = battle.RunEvent("ModifySecondaries", target, source, activeData.moveData, relayVar).secondaries;

            foreach(Globals.SecondaryEffect secondary in secondaries)
            {
                int secondaryRoll = RandomScript.RandomBetween(0, 100);
                if(secondary.chance < 0 || secondaryRoll < secondary.chance)
                {
                    TreatSecondaries(target, secondary, isSelf);
                }
            }
        }
        //Dragout
        //SelfSwitch
        return damage;
    }

    void TreatSecondaries(TargetableElement target, Globals.SecondaryEffect secondary, bool isSelf)
    {
        bool hitResult = false;
        if(target.sourceElement is PokemonCharacter)
        {
            int didSomething = -1;
            Pokemon pokeTarget = ((PokemonCharacter)target.sourceElement).pokemonData;

            //Secondary boosts
            if(secondary.boosts != null && !pokeTarget.fainted)
            {
                hitResult = battle.Boost(secondary.boosts, target, source, secondary.SecondaryToMove());
                didSomething = (hitResult) ? 1 : 0;
            }

            //Secondary status
            if(secondary.status != "")
            {
                hitResult = pokeTarget.TrySetStatus(secondary.status, source, secondary.SecondaryToMove());
                if (!hitResult) return;
                if(didSomething < 1) didSomething = (hitResult) ? 1 : 0;
            }

            //Secondary volatile
            if (secondary.volatileStatus != "")
            {
                //FLONCH
                if (secondary.volatileStatus != "flinch")
                {
                    pokeTarget.StartActionCoolDown();
                }
                else
                {
                    hitResult = pokeTarget.AddVolatile(secondary.volatileStatus, source, secondary.SecondaryToMove());
                    if (didSomething < 1) didSomething = (hitResult) ? 1 : 0;
                }
            }
        }
    }

    protected void MoveEnd()
    {

        //recoil && totaldamage

        //strugglerecoil
    }


}
