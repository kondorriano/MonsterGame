using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pokemon {

    public class TargetLocation
    {
        public Transform actualTarget;
        public Vector3 direction;

        public TargetLocation(Vector3 direction, Transform actualTarget = null)
        {
            this.actualTarget = actualTarget;
            this.direction = direction;
        }
    }    

    public class MoveSlot
    {
        public string moveName;
        public string id;
        public int pp;
        public int maxpp;
        public bool disabled;
        public string disabledSource; //POSSIBLY AN ENUM

        //Seems to be used for pp reduction
        public bool used;

        public MoveSlot()
        {
            this.moveName = "Empty";
            this.id = "empty";
            this.pp = 0;
            this.maxpp = 0;
            this.disabled = true;
            this.disabledSource = "";
            this.used = false;
        }

        public MoveSlot(string moveName, string id, int pp, int maxpp, bool disabled, string disabledSource, bool used)
        {
            this.moveName = moveName;
            this.id = id;
            this.pp = pp;
            this.maxpp = maxpp;
            this.disabled = disabled;
            this.disabledSource = disabledSource;
            this.used = used;
        }

        public MoveSlot ShallowCopy()
        {
            return (MoveSlot)this.MemberwiseClone();
        }
    }

    public class AttackBy
    {
        public BattleElement pokemon;
        public int damage;
        public string move;
        public bool thisTurn;

        public AttackBy(BattleElement pokemon = null, int damage = 0, string move = "", bool thisTurn = false)
        {
            this.pokemon = pokemon;
            this.damage = damage;
            this.move = move;
            this.thisTurn = thisTurn;
        }
    }

    //Access info //Must not change during battle
    public PokemonCharacter myPokemon;
    public TargetableElement targetData;
    public Battle.Team team;
    public Battle battle; // <--- Passed in the constructor
    public PokemonSet set;// <--- SELECTED DATA OF THE POKEMON, PASSED IN THE CONSTRUCTOR
    public TemplateData baseTemplate;
    public MoveSlot[] baseMoveSlots;
    public Globals.StatsTable baseStats;
    public string baseAbilityId;

    //Pokemon basic info
    public string species;
    public string name;
    public string speciesId;
    public int level;
    public Globals.GenderName gender;
    public int happiness;
    public string pokeball;
    public float heightm;
    public float weightkg;

    //Pokemon battle general info
    public TemplateData template;
    public MoveSlot[] moveSlots;
    public bool fainted;
    public string statusId; //STRING?????
    public int lastDamage;
    public AttackBy lastAttackedBy;
    public bool isActive;
    public int activeTurns;
    public Volatile statusData; //<---modifiable data
    public AbilityData abilityData; //<---modifiable data
    public ItemData itemData; //<---modifiable data
    public Dictionary<string, Volatile> volatiles; //POSSIBLY NEED ITS OWN CLASS
    public string abilityId;
    public Globals.BoostsTable boosts;
    public Globals.StatsTable stats;
    public int maxhp;
    public int hp;
    public int speed;

    //Type info
    public Globals.Type[] types;
    public Globals.Type addedType;
    public bool knownType;

    //Mega and Ultra info 
    public bool canMegaEvo;
    public bool canUltraBurst;

    //Move info
    public int lastMoveSlotId; //MAYBE OTHER CLAS??
    public string moveIdThisTurn;
    public bool moveLastTurnResult;
    public bool moveThisTurnResult;
    public bool duringMove;
    public Globals.HiddenPower hidpow;

    //Item info
    public string lastItem;
    public bool ateBerry;
    public bool usedItemThisTurn;
    public string itemId;

    //Switch info
    public bool switchFlag;
    public bool forceSwitchFlag;
    public bool switchCopyFlag;
    public int draggedInTurn;
    public bool newlySwitched;
    public bool beingCalledBack;

    //Trap info
    public bool trapped;
    public bool maybeTrapped;

    //Abilities
    public Pokemon illusion;
    public bool transformed;
    public bool gluttonyFlag;

    //Misc    
    public bool showCure;
    public bool hasStartedEvents;

    //NewlyAdded
    public bool inWonderRoom;
    public bool inGravity;
    public bool inTrickRoom;

    //Cooldown
    public int generalTurnCount;
    public float generalTurnCounter;

    public float abilityTurnCounter;

    public float itemTurnCounter;

    public bool inActionCooldown;
    public float actionTurnCounter;

    public Pokemon(Battle battle, PokemonSet set, Battle.Team team, PokemonCharacter myPokemon)
    {
        this.myPokemon = myPokemon;
        this.targetData = this.myPokemon.targetScript;
        this.team = team; //<---this.side = side;
        this.battle = battle;
        this.set = set;
        this.baseTemplate = Pokedex.BattlePokedex[set.speciesId];
        this.species = baseTemplate.species;
        this.name = set.name;
        this.speciesId = set.speciesId;
        this.template = this.baseTemplate;
        //this.movepp = { };
        this.moveSlots = new MoveSlot[set.movesId.Length];
        this.baseMoveSlots = new MoveSlot[set.movesId.Length];
        this.baseStats = this.baseTemplate.baseStats;
        this.trapped = false;
        this.maybeTrapped = false;
        //this.maybeDisabled = false;
        this.illusion = null;
        this.fainted = false;
        this.lastItem = "";
        this.ateBerry = false;
        this.statusId = "";
        //this.position = 0;
        this.switchFlag = false;
        this.forceSwitchFlag = false;
        this.switchCopyFlag = false;
        this.draggedInTurn = -1;
        this.lastMoveSlotId = -1;
        this.moveIdThisTurn = "";
        this.moveLastTurnResult = false;
        this.moveThisTurnResult = false;
        this.lastDamage = 0;
        this.lastAttackedBy = null;
        this.usedItemThisTurn = false;
        this.newlySwitched = false;
        this.beingCalledBack = false;
        this.isActive = false;
        this.activeTurns = 0;
        this.hasStartedEvents = false;
        this.transformed = false;
        this.duringMove = false;
        this.speed = 0;
        //this.abilityOrder = 0;
        this.level = set.level;
        this.gender = set.gender;
        this.happiness = set.happiness;
        this.pokeball = set.pokeball;
        //this.fullname = this.side.id + ': ' + this.name;
        //this.details = this.species + (this.level === 100 ? '' : ', L' + this.level) + (this.gender === '' ? '' : ', ' + this.gender) + (this.set.shiny ? ', shiny' : '');
        //this.id = this.fullname;
        this.statusData = null;
        this.volatiles = new Dictionary<string, Volatile>();
        this.heightm = this.template.heightm;
        this.weightkg = this.template.weightkg;
        this.baseAbilityId = set.abilityId;
        this.abilityId = this.baseAbilityId;
        this.itemId = set.itemId;
        this.abilityData = new AbilityData(id: this.abilityId);
        this.itemData = new ItemData(id: this.itemId);
        //this.speciesData = { id: this.speciesid};
        this.types = this.baseTemplate.types;
        this.addedType = Globals.Type.Null;
        this.knownType = true;
        this.canMegaEvo = CanMegaEvolve(this.template, this.set);
        this.canUltraBurst = CanUltraBurst(this.template, this.set);
        this.hidpow = new Globals.HiddenPower(set.ivs);
        this.boosts = new Globals.BoostsTable();
        this.stats = this.baseStats.ShallowCopy();
        //if (this.battle.gen === 1) this.modifiedStats = { atk: 0, def: 0, spa: 0, spd: 0, spe: 0};
        //this.subFainted = null;
        //this.isStale = 0;
        //this.isStaleCon = 0;
        //this.isStaleHP = this.maxhp;
        //this.isStalePPTurns = 0;
        //this.baseIvs = this.set.ivs;
        //this.baseHpType = this.hpType;
        //this.baseHpPower = this.hpPower;
        //this.apparentType = this.baseTemplate.types.join('/');

        //this.staleWarned = false;
        this.showCure = false;
        //this.originalSpecies = undefined;
        this.gluttonyFlag = false;


        MoveData auxMove;
        for(int i = 0; i < this.set.movesId.Length; ++i)
        {
            if (!Moves.BattleMovedex.ContainsKey(this.set.movesId[i]))
            {
                //ERROR SLOT
                Debug.Log("Error: " + this.set.movesId[i] + " is not in the database");
                this.baseMoveSlots[i] = new MoveSlot();
                this.moveSlots[i] = new MoveSlot();

                continue;
            }
            auxMove = Moves.BattleMovedex[this.set.movesId[i]];

            this.baseMoveSlots[i] = new MoveSlot(
                moveName: auxMove.name,
                id: auxMove.id,
                pp: auxMove.pp * (5+set.ppBoosts[i]) / 5,
                maxpp: auxMove.pp * (5 + set.ppBoosts[i]) / 5,
                disabled: false,
                disabledSource: "",
                used: false
            );
            this.moveSlots[i] = new MoveSlot(
                moveName: auxMove.name,
                id: auxMove.id,
                pp: auxMove.pp * (5 + set.ppBoosts[i]) / 5,
                maxpp: auxMove.pp * (5 + set.ppBoosts[i]) / 5,
                disabled: false,
                disabledSource: "",
                used: false
            );
        }

        //Batlle stats
        this.stats.SetBattleStats(this.set);
        UpdateSpeed();

        //Prepare set effects???
        //this.clearVolatile(); //TODO???

        this.maxhp = (this.template.maxHP > 0) ? this.template.maxHP : this.stats.hp;
        this.hp = this.maxhp;


        //NewlyAdded
        inWonderRoom = false;
        inGravity = false;
        inTrickRoom = false;

        //Cooldown
        generalTurnCount = 0;
        generalTurnCounter = battle.turnTime;

        abilityTurnCounter = battle.turnTime;

        itemTurnCounter = battle.turnTime;

        inActionCooldown = false;
        actionTurnCounter = 0;
    }

    public void LogPokemon()
    {
        string logPokemonBasic = "BasicInfo - Species: " + species + ", Name: " + name + ", SpeciesId: " + speciesId + ", Level: " + level + ", Gender: " + gender + ",\n" +
            "Happiness: " + happiness + ", Pokeball: " + pokeball + ", Height: " + heightm + ", Weight: " + weightkg + "\n";
        string logMoveSlots = "";
        for(int i = 0; i < moveSlots.Length; ++i)
        {
            logMoveSlots += moveSlots[i].moveName + ": Id - " + moveSlots[i].id + ", pp: " + moveSlots[i].pp + "/" + moveSlots[i].maxpp + ", is " + ((!moveSlots[i].disabled) ? "not" : "") + " disabled, DisableSource: " + moveSlots[i].disabledSource + ", has " + ((!moveSlots[i].used) ? "not" : "") + " been used\n";
        }
        string logBattleGeneral = "BattleGeneralInfo - TemplateDataSpecies: " + template.species + " Ability: " + abilityId + ", Hp: " + hp + "/" + maxhp + ",\nMoveSlots:\n" + logMoveSlots +
            "Stats: Hp: " + stats.hp + ", Atk: " + stats.atk + ", Def: " + stats.def + ", SpA: " + stats.spa + ", SpD: " + stats.spd + ", Spe: " + stats.spe + "\n" +
            "Boots: Hp: " + boosts.hp + ", Atk: " + boosts.atk + ", Def: " + boosts.def + ", SpA: " + boosts.spa + ", SpD: " + boosts.spd + ", Spe: " + boosts.spe + "\n" +
            "Fainted: " + fainted + ", StatusId: " + statusId + ", LastDamage: " + lastDamage + " by " + lastAttackedBy + ", IsActive: " + isActive + ", ActiveTurns: " + activeTurns + "\n" +
            "Not showing statusData or volatiles\n";

        string logtypes = "";
        for(int i= 0; i < types.Length; ++i)
        {
            logtypes += " " + types[i];
        }
        string logType = "TypeInfo - Types:" + logtypes + ", AddedTypes: " + addedType + ", KnownType: " + knownType + "\n";
        string logMeUl = "Mega and Ultra Info - Can mega evolve: " + canMegaEvo + ", Can ultra burst: " + canUltraBurst + "\n";
        string logMove = "MoveInfo - lastMoveName: " + lastMoveSlotId + ", MoveThisTurn: " + moveIdThisTurn + ", MoveLastTurnResult: " + moveLastTurnResult + ", MoveThisTurnResult: " + moveThisTurnResult + ", DuringMove: " + duringMove + ", HiddenPower: " + hidpow.hpPower + " " + hidpow.hpType + "\n";
        string logItem = "ItemInfo - Item: " + itemId + ", LastItem: " + lastItem + ", ateBerry: " + ateBerry + ", UsedItemThisTurn: " + usedItemThisTurn + "\n";
        string logSwitch = "SwitchInfo - SwitchFlag: " + switchFlag + ", ForcedSwitchFlag: " + forceSwitchFlag + ", switchCopyFlag: " + switchCopyFlag + ", draggedInTurn: " + draggedInTurn + ", newlySwitched: " + newlySwitched + ", beingCalledBack: " + beingCalledBack + "\n";
        string logTrap = "TrapInfo - Trapped: " + trapped + ", maybeTrapped: " + maybeTrapped + "\n";
        string logAbilities = "AbilitiesInfo - IllusionPoke: " + illusion + ", transformed: " + transformed + ", gluttonyFlag: " + gluttonyFlag + "\n";
        string logMisc = "MiscInfo - showCure: " + showCure + "\n";
        Debug.Log("Logging Pokemon information: \n" +
            "Won't be logging Access info \n" +
            logPokemonBasic + logBattleGeneral + logType + logMeUl + logMove + logItem + logSwitch + logTrap + logAbilities + logMisc
        );
    }

    bool CanMegaEvolve(TemplateData currentTemplate, PokemonSet set)
    {
        //Has no alternate forms, It should have one in the template
        if (currentTemplate.otherFormes == null) return false;

        ItemData item = GetInfoItem();
        TemplateData altForme;
        for(int i = 0; i < currentTemplate.otherFormes.Length; ++i)
        {
            //Doesnt exist in the database
            if (!Pokedex.BattlePokedex.ContainsKey(currentTemplate.otherFormes[i])) continue;
            altForme = Pokedex.BattlePokedex[currentTemplate.otherFormes[i]];
            //Not a mega form
            if (!altForme.isMega) continue;
            //Check if can evolve by Move
            if(altForme.requiredMove != "")
            {
                //Holds a Z item, so, cant mega evolve
                if (item != null && item.iszMove) continue;
                
                //Checking if the pokemon has the required move
                for(int j = 0; j < set.movesId.Length; ++j)
                {
                    //If the pokemon has the move and it is in the database
                    if (altForme.requiredMove == set.movesId[j] && Moves.BattleMovedex.ContainsKey(set.movesId[j])) return true;
                }
            }
            //Check if can evolve by item
            if (item == null) continue;
            //if it megaevolves from the baseSpecies and the pokemon is not the megaevolution and the evolution exists
            if (item.megaEvolves == currentTemplate.baseSpecies && item.megaStone != currentTemplate.species && item.megaStone == altForme.species) return true;
        }
        return false;
    }

    bool CanUltraBurst(TemplateData currentTemplate, PokemonSet set)
    {
        TemplateData baseTemplate = currentTemplate;
        //other formes are only placed in the baseForme, so we have to get it
        if (baseTemplate.species != baseTemplate.baseSpecies)
        {
            string baseTemplateId = Globals.getId(baseTemplate.baseSpecies);
            if (!Pokedex.BattlePokedex.ContainsKey(baseTemplateId)) return false;
            baseTemplate = Pokedex.BattlePokedex[baseTemplateId];
        }
        //Has no alternate forms, It should have one in the template
        if (baseTemplate.otherFormes == null) return false;

        ItemData item = GetInfoItem();
        TemplateData altForme;
        
        for (int i = 0; i < baseTemplate.otherFormes.Length; ++i)
        {
            //Doesnt exist in the database
            if (!Pokedex.BattlePokedex.ContainsKey(baseTemplate.otherFormes[i])) continue;
            altForme = Pokedex.BattlePokedex[baseTemplate.otherFormes[i]];
            //Not a ultra form
            if (!altForme.isUltra) continue;
            //Check if can burst by Move
            if (altForme.requiredMove != "")
            {
                //Holds a Z item, so, cant ultra burst
                if (item != null && item.iszMove) continue;

                //Checking if the pokemon has the required move
                for (int j = 0; j < set.movesId.Length; ++j)
                {
                    //If the pokemon has the move and it is in the database
                    if (altForme.requiredMove == set.movesId[j] && Moves.BattleMovedex.ContainsKey(set.movesId[j])) return true;
                }
            }
            //Check if can burst by item
            if (item == null || item.ultraBursts == null) continue;
            //Already ultra bursted
            if (item.ultraEffect == currentTemplate.species) continue;
            //if it ultra bursts from the species and the evolution exists
            for (int j = 0; j < item.ultraBursts.Length; ++j)
            {
                if (item.ultraBursts[j] == currentTemplate.species && item.ultraEffect == altForme.species) return true;
            }
        }
        return false;
    }

    public bool IgnoringItem()
    {
        ItemData item = GetInfoItem();
        bool ignoreKlutz = (item != null && item.ignoreKlutz);
        return !isActive || (HasAbilityActive(new string[] { "klutz" }) && !ignoreKlutz) || volatiles.ContainsKey("embargo") || volatiles.ContainsKey("magicroom");
    }

    public bool IgnoringAbility()
    {
        return !isActive || (volatiles.ContainsKey("gastroacid") && !MiscData.AbilitiesNotAffectedByGastroAcid.Contains(abilityId));
    }

    public bool HasAbilityActive(string[] abilities)
    {
        if (IgnoringAbility()) return false;

        for(int i = 0; i < abilities.Length; ++i)
        {
            if (Globals.getId(abilities[i]) == abilityId) return true;
        }

        return false;
    }

    public EffectData GetInfoStatus()
    {
        if (!Statuses.BattleStatuses.ContainsKey(statusId)) return null;
        return Statuses.BattleStatuses[statusId];
    }

    public AbilityData GetInfoAbility()
    {
        string ability = Globals.getId(abilityId);
        if (!Abilities.BattleAbilities.ContainsKey(ability)) return null;
        return Abilities.BattleAbilities[ability];
    }

    public ItemData GetInfoItem()
    {
        string item = Globals.getId(itemId);
        if (!Items.BattleItems.ContainsKey(item)) return null;
        return Items.BattleItems[item];
    }

    public MoveData GetInfoMove(string moveId)
    {
        string move = Globals.getId(moveId);
        if (!Moves.BattleMovedex.ContainsKey(move)) return null;
        return Moves.BattleMovedex[move];
    }

    public bool HasType(Globals.Type type)
    {
        Globals.Type[] pokeTypes = GetTypes();
        foreach(Globals.Type pokeType in pokeTypes)
        {
            if (pokeType == type) return true;
        }
        return false;
    }

    public Globals.Type[] GetTypes()
    {
        Globals.Type[] types = new Globals.Type[this.types.Length];
        for(int i = 0; i < types.Length; ++i)
        {
            types[i] = this.types[i];
        }
        Battle.RelayVar relayVar = new Battle.RelayVar(types: types);
        relayVar = battle.RunEvent("Type", targetData, null, null, relayVar);
        return relayVar.types;

    }

    public int RunEffectiveness(ActiveMove move)
    {
        int totalTypeMod = 0;
        foreach(Globals.Type type in types)
        {
            int typeMod = TypeChart.GetEffectiveness(move.activeData.moveData.type, type);
            this.targetData.consultingType = type;
            Battle.RelayVar relayVar = new Battle.RelayVar(integerValue: typeMod);
            relayVar = battle.SingleEvent("Effectiveness", move.activeData.moveData, null, targetData, move, null, relayVar);
            relayVar = battle.RunEvent("Effectiveness",targetData, myPokemon, move.activeData.moveData, relayVar);
            totalTypeMod += relayVar.integerValue;
        }

        return totalTypeMod;
    }

    public int Damage(int damage, BattleElement source = null, EffectData effect = null)
    {
        if (this.hp <= 0) return 0;
        damage = Mathf.Max(1, Mathf.FloorToInt(damage));
        this.hp -= damage;
        if(this.hp <= 0)
        {
            damage += this.hp;
            Faint(source, effect);
        }
        return damage;

    }

    public int CalculateStat(string statName, int boost = 0, int modifier = 1)
    {
        statName = Globals.getId(statName);
        if (statName == "hp") return this.maxhp;

        //Should be baseStat???
        int stat = this.stats.GetStatValue(statName);

        if(this.inWonderRoom)
        {
            if (statName == "def") stat = this.stats.GetStatValue("spd");
            else if(statName == "spd") stat = this.stats.GetStatValue("def");
        }

        //BOOST
        Globals.BoostsTable boosts = new Globals.BoostsTable();
        boosts.SetBoostValue(statName, boost);
        Battle.RelayVar relayVar = new Battle.RelayVar(boosts: boosts);
        relayVar = battle.RunEvent("ModifyBoost", targetData, null, null, relayVar);
        boost = relayVar.boosts.GetBoostValue(statName);
        boost = Mathf.Clamp(boost, -6, 6);

        if(boost >= 0) stat = Mathf.FloorToInt(stat * boosts.boostTable[boost]);
        else stat = Mathf.FloorToInt(stat / boosts.boostTable[-boost]);
        //END BOOST

        //stat modifier
        stat = stat * modifier; //if float problems check showdown github

        return stat;
    }

    int deductPP(int moveSlotId, int amount = 1)
    {
        if (moveSlotId < 0) return 0;
        MoveSlot move = this.moveSlots[moveSlotId];
        move.used = true;
        move.pp -= amount;
        
        if(move.pp < 0)
        {
            amount += move.pp;
            move.pp = 0;
        }

        return amount;
    }

    void MoveUsed(int moveSlotId, string moveId)
    {
        this.lastMoveSlotId = moveSlotId;
        this.moveIdThisTurn = moveId;
    }

    public bool AddVolatile(string statusId, BattleElement source = null, EffectData sourceEffect = null, string linkedStatusId = "")
    {
        EffectData status = battle.GetPureEffect(statusId);
        if (this.hp <= 0 && !status.affectsFainted) return false;
        if (linkedStatusId != "" && source is PokemonCharacter && ((PokemonCharacter)source).pokemonData.hp <= 0) return false;
        if(battle.currentEvent != null)
        {
            if(source == null) source = battle.currentEvent.source;
            if (sourceEffect == null) sourceEffect = battle.effectInEvent;
        }

        /*Already contains volatile*/
        if(this.volatiles.ContainsKey(status.id))
        {
            if (status.eventMethods.onRestart == null) return false;
            return !battle.SingleEvent("Restart", status, this.volatiles[status.id], targetData, source, sourceEffect).getEndEvent();
        }

        /*detect status immunity*/
        if (HasStatusImmunity(status.id)) return false;

        /*Trying to add volatile*/
        Battle.RelayVar relayVar = new Battle.RelayVar(effectValue: status);
        relayVar = battle.RunEvent("TryAddVolatile", targetData, source, sourceEffect, relayVar);
        if (relayVar.getEndEvent()) return false;

        Volatile newVolatile = new Volatile(status);
        newVolatile.SetData(turnTime: battle.turnTime, target: targetData, source: source, sourceEffect: sourceEffect);

        if(newVolatile.eventMethods.durationCallback != null)
        {
            relayVar = new Battle.RelayVar();
            newVolatile.duration = newVolatile.eventMethods.StartCallback("durationCallback", battle, relayVar,  targetData, source, sourceEffect).integerValue;
        }

        if(battle.SingleEvent("Start", status, newVolatile, targetData, source, sourceEffect).getEndEvent())
        {
            return false;
        }

        this.volatiles.Add(statusId, newVolatile);

        if(linkedStatusId != "" && source is PokemonCharacter)
        {
            Pokemon pokeSource = ((PokemonCharacter)source).pokemonData;
            if(!pokeSource.volatiles.ContainsKey(linkedStatusId))
            {
                pokeSource.AddVolatile(linkedStatusId, myPokemon, sourceEffect);
                pokeSource.volatiles[statusId].linkedStatus = statusId;
            }
            pokeSource.volatiles[statusId].linkedSources.Add(myPokemon);

            volatiles[statusId].linkedSources.Add(source);
            volatiles[statusId].linkedStatus = linkedStatusId;
        }

        return true;        
    }

    bool RemoveVolatile(string statusId)
    {
        if (this.hp <= 0) return false;
        if (!this.volatiles.ContainsKey(statusId)) return false;
        battle.SingleEvent("End", this.volatiles[statusId], this.volatiles[statusId], targetData);
        List<BattleElement> linkedSources = this.volatiles[statusId].linkedSources;
        string linkedStatus = this.volatiles[statusId].linkedStatus;
        this.volatiles.Remove(statusId);
        if(linkedSources.Count > 0)
        {
            RemoveLinkedVolatiles(linkedStatus, linkedSources);
        }
        return true;
    }

    void RemoveLinkedVolatiles(string linkedStatus, List<BattleElement> linkedSources)
    {
        foreach(BattleElement linkedPoke in linkedSources)
        {
            Pokemon poke = ((PokemonCharacter)linkedPoke).pokemonData;
            if (poke.volatiles.ContainsKey(linkedStatus))
            {
                poke.volatiles[linkedStatus].linkedSources.Remove(myPokemon);
                if (poke.volatiles[linkedStatus].linkedSources.Count == 0)
                    poke.RemoveVolatile(linkedStatus);
            }
        }
    }

    public Volatile GetVolatile(string statusId)
    {
        if (!this.volatiles.ContainsKey(statusId)) return null;
        return this.volatiles[statusId];
    }



    public bool HasStatusImmunity(string type)
    {
        if (fainted) return true;
        if (type == "") return false;
        if (TypeChart.HasImmunity(type, this.types)) return true;

        Battle.RelayVar relayVar = new Battle.RelayVar(stringValue: type);
        relayVar = battle.RunEvent("Immunity", targetData, null, null, relayVar);
        if (relayVar.getEndEvent()) return true;
        return false;
    }

    public void SetBoosts(Globals.BoostsTable boosts)
    {
        this.boosts.SetAllBoosts(boosts);
    }

    public int BoostBy(string boostName, int boostValue) 
    {
        int delta = boostValue;
        int boostResult = this.boosts.GetBoostValue(boostName);
        boostResult += boostValue;
        if(boostResult > 6)
        {
            delta -= boostResult - 6;
            boostResult = 6;
        } else if(boostResult < -6)
        {
            delta -= boostResult + 6;
            boostResult = -6;
        }

        this.boosts.SetBoostValue(boostName, boostResult);

        return delta;
    }

    public int Heal(int damage)
    {
        if (this.hp <= 0 || damage <= 0 || this.hp >= this.maxhp) return 0;
        this.hp += damage;
        if(this.hp > this.maxhp)
        {
            damage -= this.hp - this.maxhp;
            this.hp = this.maxhp;
        }
        return damage;
    }

    public int Faint(BattleElement source = null, EffectData effect = null)
    {
        if (this.fainted) return 0;
        int damage = this.hp;
        this.hp = 0;
        this.switchFlag = false;

        battle.RunEvent("BeforeFaint", targetData, source, effect);
        //team.pokemonLeft--;
        battle.RunEvent("Faint", targetData, source, effect);
        battle.SingleEvent("End", GetInfoAbility(), this.abilityData, targetData);
        ClearBattleData(false);
        this.fainted = true;
        this.isActive = false;
        this.hasStartedEvents = false;

        return damage;
    }

    public void ClearBattleData(bool includeSwitchFlags = true)
    {
        this.boosts = new Globals.BoostsTable();
        for(int i = 0; i < baseMoveSlots.Length; ++i)
        {
            moveSlots[i] = baseMoveSlots[i].ShallowCopy();
        }
        this.transformed = false;
        this.abilityId = this.baseAbilityId;
        //this.set.ivs = this.baseIvs;
        //this.hpType = this.baseHpType;
        //this.hpPower = this.baseHpPower;
        foreach (KeyValuePair<string, Volatile> entry in this.volatiles)
        {
            // do something with entry.Value or entry.Key
            if(entry.Value.linkedStatus != "")
            {
                RemoveLinkedVolatiles(entry.Value.linkedStatus, entry.Value.linkedSources);
            }
        }
        this.volatiles.Clear();

        if(includeSwitchFlags)
        {
            this.switchFlag = false;
            this.forceSwitchFlag = false;
        }

        this.lastMoveSlotId = -1;
        this.moveIdThisTurn = "";

        this.lastDamage = 0;
        this.lastAttackedBy = null;
        this.newlySwitched = true;
        this.beingCalledBack = false;

        FormeChange(Globals.getId(baseTemplate.species));

    }

    public void GotAttacked(string moveId, int damage, BattleElement source)
    {
        if (damage < 0) damage = 0;
        this.lastAttackedBy = new AttackBy(
            pokemon: source, damage: damage, move: moveId, thisTurn: true
            );
    }

    bool FormeChange(string templateId, bool useBattleEffect = true, EffectData sourceEffect = null, bool isPermanent = false)
    {
        if (sourceEffect == null && useBattleEffect) sourceEffect = battle.effectInEvent;

        if (Pokedex.BattlePokedex.ContainsKey(templateId)) return false;
        TemplateData rawTemplate = Pokedex.BattlePokedex[templateId];

        if (rawTemplate.ability0 == "") return false;
        //Just for format call
        //	let template = this.battle.singleEvent('ModifyTemplate', this.battle.getFormat(), null, this, source, null, rawTemplate);

        this.template = rawTemplate;
        SetTypes(rawTemplate.types, true);

        if (sourceEffect == null) return true;
        this.baseStats = this.template.baseStats;
        this.stats = this.baseStats.ShallowCopy();
        stats.SetBattleStats(this.set);

        this.speed = this.stats.GetStatValue("spe");

        if (sourceEffect.id == "" && sourceEffect.effectType == Globals.EffectTypes.Null) return true;

        if(isPermanent) this.baseTemplate = rawTemplate;
        if(sourceEffect.effectType != Globals.EffectTypes.Ability && sourceEffect.id != "relicsong" && sourceEffect.id != "zenmode")
        {
            if(this.illusion != null)
            {
                this.abilityId = ""; //We will change the ability but it would end illusion. Doing this we dont allow it
            }
            SetAbility(rawTemplate.ability0, null, true);
            if (isPermanent) this.baseAbilityId = this.abilityId;
        }

        return true;
    }

    bool SetAbility(string abilityId, Pokemon source, bool isFromFormeChange)
    {
        if (this.hp <= 0) return false;
        if (!Abilities.BattleAbilities.ContainsKey(abilityId)) return false;
        AbilityData newAbility = Abilities.BattleAbilities[abilityId];
        string oldAbilityId = this.abilityId;

        if(!isFromFormeChange)
        {
            if (MiscData.AbilitiesThatCantBeChangedTo.Contains(newAbility.id)) return false;
            if (MiscData.AbilitiesThatCantBeChangedFrom.Contains(oldAbilityId)) return false;
        }

        Battle.RelayVar relayVar = new Battle.RelayVar(effectValue: newAbility);
        if (battle.RunEvent("SetAbility", targetData, source.myPokemon, battle.effectInEvent, relayVar).getEndEvent()) return false;
        battle.SingleEvent("End", Abilities.BattleAbilities[oldAbilityId], this.abilityData, targetData, source.myPokemon);

        this.abilityId = newAbility.id;
        this.abilityData = new AbilityData(id: newAbility.id);
        this.abilityData.target = targetData;

        if (newAbility.id != "") battle.SingleEvent("Start", newAbility, this.abilityData, targetData, source.myPokemon);

        return true;
    }

    bool SetTypes(Globals.Type[] newTypes, bool enforce = false)
    {
        //type of Arceus, Silvally cannot be normally changed
        if (!enforce && (this.template.num == 493 || this.template.num == 773)) return false;

        this.types = newTypes;
        this.addedType = Globals.Type.Null;
        this.knownType = true;
        //	this.apparentType = this.types.join('/');
        return true;
    }

    public bool HasImmunity(Globals.Type type)
    {
        if (type == Globals.Type.Null || type == Globals.Type.Unknown) return false;
        if (this.fainted) return true;

        Battle.RelayVar relayVar = new Battle.RelayVar(types: new Globals.Type[] {type });
        bool negateResult = !battle.RunEvent("NegateImmunity", targetData, null, null, relayVar).getEndEvent();

        if(type == Globals.Type.Ground)
        {
            if (!IsGrounded(!negateResult)) return true;
        }

        if (!negateResult) return false;
        if (TypeChart.HasImmunity("" + type, this.types)) return true;

        return false;
    }

    bool IsGrounded(bool negateImmunity = false)
    {
        if (this.inGravity) return true;
        if (this.volatiles.ContainsKey("ingrain")) return true;
        if (this.volatiles.ContainsKey("smackdown")) return true;
        string item = (IgnoringItem()) ? "" : this.itemId;
        if (item == "ironball") return true;
        // If a Fire/Flying type uses Burn Up and Roost, it becomes ???/Flying-type, but it's still grounded.
        if (!negateImmunity && HasType(Globals.Type.Flying) && !this.volatiles.ContainsKey("roost")) return false;
        if (HasAbilityActive(new string[] { "levitate" }) && !myPokemon.ignoreAbility) return true;
        if (this.volatiles.ContainsKey("magnetrise")) return false;
        if (this.volatiles.ContainsKey("telekinesis")) return false;
        return (item == "airballoon");

    }

    void UpdateSpeed()
    {
        this.speed = GetActionSpeed();
    }

    int GetActionSpeed()
    {
        int speed = GetStat("spe", false, false);
        if (speed > 10000) speed = 10000;
        if (inTrickRoom) speed = 10000 - speed;
        return speed;

    }

    int GetStat(string statName, bool unboosted, bool unmodified)
    {
        statName = Globals.getId(statName);

        if (statName == "hp") return this.maxhp;

        int stat = this.stats.GetStatValue(statName);

        if(unmodified && this.inWonderRoom)
        {
            if (statName == "def") statName = "spd";
            else if (statName == "spd") statName = "def";
        }

        if(!unboosted)
        {
            Battle.RelayVar relayVar = new Battle.RelayVar(boosts: this.boosts.ShallowCopy());
            int boost = battle.RunEvent("ModifyBoost", targetData, null, null, relayVar).boosts.GetBoostValue(statName);
            boost = Mathf.Clamp(boost, -6, 6);
            float[] boostTable = { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };
            if (boost >= 0) stat = Mathf.FloorToInt(stat * boostTable[boost]);
            else stat = Mathf.FloorToInt(stat / boostTable[boost]);
        }

        if(!unmodified)
        {
            string eventName = "";
            switch(statName)
            {
                case "atk":
                    eventName = "Atk";
                    break;
                case "def":
                    eventName = "Def";
                    break;
                case "spa":
                    eventName = "SpA";
                    break;
                case "spd":
                    eventName = "SpD";
                    break;
                case "spe":
                    eventName = "Spe";
                    break;
            }
            Battle.RelayVar relayVar = new Battle.RelayVar(integerValue: stat);
            stat = battle.RunEvent("Modify" + eventName, targetData, null, null, relayVar).integerValue;
        }

        return stat;
    }

    public bool SetStatus(string statusId, BattleElement source = null, EffectData sourceEffect = null, bool ignoreImmunities = false)
    {
        if (this.hp <= 0) return false;
        if (this.statusId == statusId) return false;
        EffectData myStatus = battle.GetPureEffect(statusId);
        if(battle.currentEvent != null)
        {
            if (source == null) source = battle.currentEvent.source;
            if (sourceEffect == null) sourceEffect = battle.effectInEvent;
        }

        if (!ignoreImmunities && !(source is PokemonCharacter &&
            ((PokemonCharacter)source).pokemonData.HasAbilityActive(new string[] { "corrosion" }) && 
            (myStatus.id == "tox" || myStatus.id == "psn" )))
        {
            // the game currently never ignores immunities
            if(HasStatusImmunity((statusId == "tox") ? "psn" : statusId))
            {
                return false;
            }
        }

        string prevStatus = this.statusId;
        Volatile prevStatusData = this.statusData;

        Battle.RelayVar relayVar = new Battle.RelayVar(effectValue: myStatus);
        bool result = !battle.RunEvent("SetStatus", targetData, source, sourceEffect, relayVar).getEndEvent();
        if (!result) return false;

        this.statusId = myStatus.id;
        this.statusData = new Volatile(myStatus);
        this.statusData.SetData(battle.turnTime, targetData, source);

        if(myStatus.eventMethods.durationCallback != null)
        {
            relayVar = new Battle.RelayVar();
            this.statusData.duration = myStatus.eventMethods.StartCallback("durationCallback", battle, relayVar, targetData, source, sourceEffect).integerValue;
        }

        if(battle.SingleEvent("Start", myStatus, this.statusData, targetData, source, sourceEffect).getEndEvent())
        {
            this.statusId = prevStatus;
            this.statusData = prevStatusData;
            return false;
        }

        relayVar = new Battle.RelayVar(effectValue: myStatus);
        if(battle.RunEvent("AfterSetStatus", targetData, source, sourceEffect, relayVar).getEndEvent())
        {
            return false;
        }

        return true;
    }

    public bool TrySetStatus(string statusId, BattleElement source = null, EffectData sourceEffect = null)
    {
        string statusToSend = (this.statusId == "") ? statusId : this.statusId;
        return SetStatus(statusToSend, source, sourceEffect);
    }



    /**
    * runMove is the "outside" move caller. It handles deducting PP,
    * flinching, full paralysis, etc. All the stuff up to and including
    * the "POKEMON used MOVE" message.
    *
    * For details of the difference between runMove and useMove, see
    * useMove's info.
    *
    * externalMove skips LockMove and PP deduction, mostly for use by
    * Dancer.
    */
    public void RunMove(int moveSlotId, int moveSlotOption, bool zMove, bool externalMove, TargetLocation moveTarget)
    {
        /*
        [BeforeMove] (A)
        -> false => exit runMove
        display "POKEMON used MOVE!"
        */
        Battle.RelayVar relayVar;

        string moveId = (moveSlotId < 0) ? "struggle" : moveSlots[moveSlotId].id;

        //The info we will use to instantiate the move
        ActiveMove.ActiveMoveData activeMove = new ActiveMove.ActiveMoveData(moveId);
        activeMove.moveSlotId = moveSlotId;
        activeMove.moveSlotOption = moveSlotOption;

        //May override baseMove (used by encore)
        //this.runEvent('OverrideAction', pokemon, target, move);

        //Zmove detection -> getzmove
        //no need to recheck if zMove should be true because it was posible to do a zmove with this pokemon and this item
        if (zMove)
        {
            ItemData itemZ = GetInfoItem();

            activeMove.zPowered = true;
            Globals.MoveCategory zCategory = activeMove.moveData.category;

            //Zmove for a concrete move (catastropika with volt tackle, etc)
            if (activeMove.moveData.name == itemZ.zMoveFrom)
            {
                activeMove.ReSetData(Globals.getId(itemZ.zMoveName));
                activeMove.basePower = activeMove.moveData.basePower;
                activeMove.moveType = activeMove.moveData.type;
                activeMove.moveData.category = zCategory;//????
            }
            //Basic zMove
            else
            {
                if (activeMove.moveData.category != Globals.MoveCategory.Status)
                {
                    //Not status move, get zmoveInfo by type. Set baseMove zpower
                    activeMove.basePower = activeMove.moveData.zMovePower;
                    if(moveId == "weatherball")
                    {
                        relayVar = new Battle.RelayVar(effectValue: activeMove.moveData);
                        battle.SingleEvent("ModifyMove", activeMove.moveData, null,targetData, null, activeMove.moveData, relayVar);
                        activeMove.moveType = activeMove.moveData.type;
                    }
                    activeMove.ReSetData(Globals.getId(ZBasicMoveTable.ZBasicMoves["" + activeMove.moveType]));
                    activeMove.moveData.category = zCategory;
                }
            }
        }

        activeMove.isExternal = externalMove;
        relayVar = new Battle.RelayVar();
        relayVar = battle.RunEvent("BeforeMove", targetData, null, activeMove.moveData, relayVar);
        if(relayVar.getEndEvent())
        {
            battle.RunEvent("MoveAborted", targetData, null, activeMove.moveData);
            moveThisTurnResult = false;
            return;
        }

        if(activeMove.moveData.eventMethods.beforeMoveCallback != null)
        {
            relayVar = new Battle.RelayVar();
            //Returns true if move cant be done
            relayVar = activeMove.moveData.eventMethods.StartCallback("beforeMoveCallback", battle, relayVar, targetData, null, activeMove.moveData);
            if(relayVar.booleanValue)
            {
                moveThisTurnResult = false;
                return;
            }
        }

        if(!externalMove && moveSlotId >= 0)
        {
            int ppDrop = 1;
            relayVar = new Battle.RelayVar(integerValue: ppDrop);
            relayVar = battle.RunEvent("DeductPP", targetData, null, activeMove.moveData, relayVar);
            ppDrop = relayVar.integerValue;
            if (ppDrop > 0) deductPP(moveSlotId, ppDrop);

            MoveUsed(moveSlotId, moveId);
        }

        if(zMove)
        {
            if(illusion != null)
            {
                battle.SingleEvent("End", Abilities.BattleAbilities["illusion"], abilityData, targetData);
            }
            team.zMoveUsed = true;
        }


        InstantiateMove(activeMove, moveTarget);


        battle.SingleEvent("AfterMove", activeMove.moveData, null, targetData, null, activeMove.moveData);
        battle.RunEvent("AfterMove", targetData, null, activeMove.moveData);
        
        //Dancer ability moved to onAnyAfterMove? will use isExternal
    }

    public void InstantiateMove(ActiveMove.ActiveMoveData activeMove, TargetLocation moveTarget)
    {
        GameObject moveMesh = Resources.Load<GameObject>("Prefabs\\" + activeMove.moveId + "ActiveMove");
        moveMesh = GameObject.Instantiate(moveMesh, myPokemon.transform.position, myPokemon.transform.rotation);
        moveMesh.GetComponent<ActiveMove>().Init(battle, myPokemon, activeMove, moveTarget);
        myPokemon.SetActiveMove(moveMesh);
    }

    //Switch pokemon
    public void SwitchIn()
    {

    }

    //Mega evolve
    public void RunMegaEvo()
    {

    }

    public void StartActionCoolDown()
    {
        inActionCooldown = true;
        actionTurnCounter = battle.turnTime;
    }

    public void CoolDownManagement()
    {
        //General because yes
        generalTurnCounter -= Time.deltaTime;
        if (generalTurnCounter <= 0)
        {
            ++generalTurnCount;
            generalTurnCounter += battle.turnTime;
        }

        //Action turn
        if (inActionCooldown)
        {
            actionTurnCounter -= Time.deltaTime;
            if (actionTurnCounter <= 0)
            {
                inActionCooldown = false;
            }
        }

        //Status turn
        if (statusData != null)
        {
            statusData.volatileCounter -= Time.deltaTime;
            if (statusData.volatileCounter <= 0)
            {

                statusData.volatileCounter += battle.turnTime;
                //OnResidual
                if (statusData.eventMethods.onResidual != null)
                {
                    Battle.RelayVar relayVar = new Battle.RelayVar();
                    statusData.eventMethods.StartCallback("onResidual", battle, relayVar, targetData, null, null);
                }
            }
        }

        List<string> toRemoveVolatiles = new List<string>();

        //Volatiles turn
        foreach (KeyValuePair<string, Volatile> vol in volatiles)
        {
            vol.Value.volatileCounter -= Time.deltaTime;
            if (vol.Value.volatileCounter <= 0)
            {
                vol.Value.volatileCounter += battle.turnTime;
                //OnResidual
                if (vol.Value.eventMethods.onResidual != null)
                {
                    Battle.RelayVar relayVar = new Battle.RelayVar();
                    vol.Value.eventMethods.StartCallback("onResidual", battle, relayVar, targetData, null, null);
                }
                //Time
                if(vol.Value.time != -1)
                {
                    --vol.Value.time;
                    if(vol.Value.time == 0)
                    {
                        toRemoveVolatiles.Add(vol.Key);
                    }
                }
                //Duration
                else if (vol.Value.duration != -1)
                {
                    --vol.Value.duration;
                    if (vol.Value.duration == 0)
                    {
                        toRemoveVolatiles.Add(vol.Key);
                    }
                }                
            }
        }

        foreach(string volKey in toRemoveVolatiles)
        {
            RemoveVolatile(volKey);
        }

        if(abilityId != "")
        {
            abilityTurnCounter -= Time.deltaTime;
            if (abilityTurnCounter <= 0)
            {

                abilityTurnCounter += battle.turnTime;
                //OnResidual
                AbilityData ability = GetInfoAbility();
                if (ability.eventMethods.onResidual != null)
                {
                    Battle.RelayVar relayVar = new Battle.RelayVar();
                    ability.eventMethods.StartCallback("onResidual", battle, relayVar, targetData, null, null);
                }
            }
        }

        if (itemId != "")
        {
            itemTurnCounter -= Time.deltaTime;
            if (itemTurnCounter <= 0)
            {

                itemTurnCounter += battle.turnTime;
                //OnResidual
                ItemData item = GetInfoItem();
                if (item.eventMethods.onResidual != null)
                {
                    Battle.RelayVar relayVar = new Battle.RelayVar();
                    item.eventMethods.StartCallback("onResidual", battle, relayVar, targetData, null, null);
                }
            }
        }





    }
}
