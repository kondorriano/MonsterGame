using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle {

    public class Stage
    {
        //Weather
        //Terrain
        //Atrezzo
    }

    public class Team
    {
        public PokemonSet[] pokemonsSets;
        public PokemonCharacter[] pokemons;
        public ActiveMove[] teamMoves; //should be subset of battle.activeMoves
        public bool zMoveUsed = false;
    }

    float turnTime = 5;

    Stage stage;
    public Team[] teams;


    ActiveWeather activeWeather; //Weather on field
    ActiveTerrain activeTerrain; //terrain on field
    ActiveMove[] activeMoves; //Moves on the field
    ActiveAtrezzo[] activeAtrezzo; //Attrezzo being used as an attack?

    int eventDepth = 0;

    public EffectData effectInEvent;
    public EffectData effectDataInEvent;
    public TargetableElement targetInEvent;
    public Event currentEvent;

    //FOR TESTING
    public Battle() { }

    public Battle(Stage stage, Team[] teams)
    {
        this.teams = teams;
        this.stage = stage;
        this.currentEvent = null;
        this.effectInEvent = null;
        this.effectDataInEvent = null;
        this.targetInEvent = null;

        //Generate all pokemon characters
        //Set active weather
        //Set active terrain
    }

    public void StartBattle()
    {
        //Set activePokemons
        //Animation of pokemon appearing
        //321 start
    }

    public void LogData(string data)
    {
        //Log in an ui new data
    }

    void Damage(int damage, Pokemon target, Pokemon pokemon, ActiveMove move)
    {

    }

    //I think it will be used for direct damages
    public int GetDamage(Pokemon pokemon, Pokemon target, ActiveMove activeMove, int directDamage = -1)//movedata comes from active move?? //DirectDamage for confusion and stuff
    {
        MoveData moveData = activeMove.activeData.moveData;
        RelayVar relayVar;

        if (moveData == null && directDamage == -1) return -1; //No moveData and no directDamage
        if(directDamage != -1)
        {
            moveData = new MoveData(basePower: directDamage, 
                                            type: Globals.Type.Unknown, 
                                            category: Globals.MoveCategory.Physical, 
                                            willCrit: false);
        }

        //if not ignoreImmunity return false (do it before?

        /*RETURN DIRECT DAMAGE*/
        if (moveData.ohko != Globals.OHKO.Null) return target.maxhp; //Fissure, Sheer cold
        if(moveData.eventMethods.damageCallback != null) //mirro coat, naturesmaddnes, hyper fang...
        {
            relayVar = new RelayVar();
            moveData.eventMethods.StartCallback("damageCallback", this,relayVar, target.targetData, pokemon.myPokemon, null);
            return relayVar.integerValue;
        }
        if (moveData.damageByLevel) return pokemon.level; //nightshade, seismic toss
        if (moveData.damage != -1) return moveData.damage; //Dragon rage, sonic boom

        /*USING BASE POWER*/
        //Category
        Globals.MoveCategory category = (moveData.category == Globals.MoveCategory.Null) ? Globals.MoveCategory.Physical : moveData.category;
        if (moveData.defensiveCategory != Globals.MoveCategory.Null) category = moveData.defensiveCategory;
        
        //BasePower
        int basePower = moveData.basePower;
        if(moveData.eventMethods.basePowerCallback != null)
        {
            relayVar = new RelayVar();
            moveData.eventMethods.StartCallback("basePowerCallback", this, relayVar, target.targetData, pokemon.myPokemon, moveData);
            basePower = relayVar.integerValue;
        }
        if (basePower < 0) return -1; //Return -1 means no dealing damage
        basePower = Mathf.Max(1, basePower); //Min value will be 1
        Debug.Log("Power after basePowerCallback: " + basePower);


        //CritRatio
        int[] critMultiplier = { 0,24,8,2,1};
        relayVar = new RelayVar(integerValue: moveData.critRatio);
        relayVar = RunEvent("ModifyCritRatio", pokemon.targetData, target.myPokemon, moveData, relayVar);
        int critRatio = Mathf.Clamp(relayVar.integerValue, 0, 4);

        //Set crit
        activeMove.activeData.crit = moveData.willCrit;
        if(!activeMove.activeData.crit)
        {
            activeMove.activeData.crit = RandomScript.RandomChance(1, critMultiplier[critRatio]);
        }
        if(activeMove.activeData.crit)
        {
            relayVar = new RelayVar(booleanValue: activeMove.activeData.crit);
            relayVar = RunEvent("CriticalHit", target.targetData, null, moveData);
            activeMove.activeData.crit = relayVar.booleanValue;
        }

        //Happens after crit calculation
        relayVar = new RelayVar(integerValue: basePower);
        relayVar = RunEvent("BasePower", pokemon.targetData, target.myPokemon, moveData, relayVar, true);
        if (relayVar.getEndEvent() && relayVar.integerValue != -1) return -1;
        basePower = Mathf.Max(1, relayVar.integerValue);

        Debug.Log("Power after basepower: " + basePower);


        //Starting?
        int level = pokemon.level;
        Pokemon attacker = pokemon;
        Pokemon defender = target;
        string attackStat = (category == Globals.MoveCategory.Physical) ? "Atk" : "SpA";
        string defenseStat = (category == Globals.MoveCategory.Physical) ? "Def" : "SpD";
        //statTable
        int attack;
        int defense;

        int atkBoosts = (moveData.useTargetOffensive) ? defender.boosts.GetBoostValue(attackStat) : attacker.boosts.GetBoostValue(attackStat);
        int defBoosts = (moveData.useSourceDefensive) ? attacker.boosts.GetBoostValue(defenseStat) : defender.boosts.GetBoostValue(defenseStat);

        bool ignoreNegativeOffensive = moveData.ignoreNegativeOffensive;
        bool ignorePositiveDefensive = moveData.ignorePositiveDefensive;

        if (activeMove.activeData.crit)
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
        else attack = attacker.CalculateStat(attackStat, atkBoosts);

        if (moveData.useSourceDefensive) defense = attacker.CalculateStat(defenseStat, defBoosts);
        else defense = defender.CalculateStat(defenseStat, defBoosts);

        Debug.Log("attack: " + attack + " defense: " + defense);

        //Apply stat modifiers
        relayVar = new RelayVar(integerValue: attack);
        relayVar = RunEvent("Modify" + attackStat, attacker.targetData, defender.myPokemon, moveData, relayVar);
        attack = relayVar.integerValue;

        relayVar = new RelayVar(integerValue: defense);
        relayVar = RunEvent("Modify" + defenseStat, defender.targetData, attacker.myPokemon, moveData, relayVar);
        defense = relayVar.integerValue;

        Debug.Log("After modifiers attack: " + attack + " defense: " + defense);


        //int(int(int(2 * L / 5 + 2) * A * P / D) / 50);        
        int baseDamage = Mathf.FloorToInt(Mathf.FloorToInt(Mathf.FloorToInt(2f * level / 5f + 2f) * basePower * attack / defense) / 50f);
        Debug.Log("baseDamage: " + baseDamage);

        // Calculate damage modifiers separately (order differs between generations)
        return ModifyDamage(baseDamage, pokemon, target, activeMove);
    }

    public int ModifyDamage(int baseDamage, Pokemon pokemon, Pokemon target, ActiveMove activeMove)
    {
        Globals.Type type = activeMove.activeData.moveData.type;
        baseDamage += 2;

        RelayVar relayVar;

        //MultiTarget should go here but not needed

        //Weather modifier
        relayVar = new RelayVar(integerValue: baseDamage);
        relayVar = RunEvent("WeatherModifyDamage", pokemon.targetData, target.myPokemon, activeMove.activeData.moveData, relayVar);
        baseDamage = relayVar.integerValue;

        Debug.Log("Weather modifier: " + baseDamage);


        //crit
        if (activeMove.activeData.crit)
        {
            baseDamage = Mathf.FloorToInt(baseDamage * 1.5f);
        }

        Debug.Log("Crit modifier: " + baseDamage);


        //Not a modifier
        baseDamage = RandomScript.Randomizer(baseDamage);
        Debug.Log("Random modifier: " + baseDamage);


        //STAB
        if (pokemon.HasType(type)) {
            baseDamage = Mathf.FloorToInt(baseDamage * ((activeMove.activeData.stab != -1) ? activeMove.activeData.stab : 1.5f));
        }

        Debug.Log("STAB modifier: " + baseDamage);


        //Types
        activeMove.activeData.typeMod = Mathf.Clamp(target.RunEffectiveness(activeMove), -6,6);
        if(activeMove.activeData.typeMod > 0)
        {
            for (int i = 0; i < activeMove.activeData.typeMod; i++)
            {
                baseDamage *= 2;
            }
        }

        if (activeMove.activeData.typeMod < 0)
        {
            for (int i = 0; i > activeMove.activeData.typeMod; i--)
            {
                baseDamage = Mathf.FloorToInt(baseDamage / 2f);
            }
        }

        Debug.Log("Types modifier: " + baseDamage);


        //Burn Status
        if (pokemon.statusId == "brn" && activeMove.activeData.moveData.category == Globals.MoveCategory.Physical && !pokemon.HasAbilityActive(new string[]{"guts"}))
        {
            if (activeMove.id != "facade") baseDamage = Mathf.FloorToInt(baseDamage*0.5f);
        }

        Debug.Log("Burn modifier: " + baseDamage);


        // Final modifier. Modifiers that modify damage after min damage check, such as Life Orb.
        relayVar = new RelayVar(integerValue: baseDamage);
        relayVar = RunEvent("ModifyDamage", pokemon.targetData, target.myPokemon, activeMove.activeData.moveData, relayVar);
        baseDamage = relayVar.integerValue;

        Debug.Log("other modifier: " + baseDamage);


        //Z breaking protect
        if (activeMove.activeData.zPowered && activeMove.activeData.zBrokeProtect)
        {
            baseDamage = Mathf.FloorToInt(baseDamage * 0.25f);
        }

        Debug.Log("z break protect modifier: " + baseDamage);


        if (baseDamage < 1) baseDamage = 1;

        return Mathf.FloorToInt(baseDamage);
    }

    public void Damage(int damage, Pokemon target = null, Pokemon source = null, EffectData effect = null, bool instafaint = false)
    {
        Debug.Log("HEY, DIS IS GONNA HURT");
    }

    public EffectData GetPureEffect(string effectId)
    {
        string name = effectId;
        string id = Globals.getId(name);
        EffectData effect;
        //Get effect from status
        if(Statuses.BattleStatuses.ContainsKey(id))
        {
            effect = Statuses.BattleStatuses[id].PureEffect(name);
        }
        //Get effect from move
        else if(Moves.BattleMovedex.ContainsKey(id) && Moves.BattleMovedex[id].effect != null)
        {
            name = Moves.BattleMovedex[id].name;
            effect = Moves.BattleMovedex[id].effect.PureEffect(name);
        }
        //Get effect from ability
        else if (Abilities.BattleAbilities.ContainsKey(id) && Abilities.BattleAbilities[id].effect != null)
        {
            name = Abilities.BattleAbilities[id].name;
            effect = Abilities.BattleAbilities[id].effect.PureEffect(name);
        }
        //Get effect from item
        else if (Items.BattleItems.ContainsKey(id) && Items.BattleItems[id].effect != null)
        {
            name = Items.BattleItems[id].name;
            effect = Items.BattleItems[id].effect.PureEffect(name);
        }
        //Get effect from format
        //Create "recoil" effect
        //Create "drain" effect
        else
        {
            effect = new EffectData(name: name);
        }
        return effect;
    }

    public bool Boost(Globals.BoostsTable boost, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        bool success = false;

        if(this.currentEvent != null)
        {
            if (target == null) target = this.currentEvent.target;
            if (source == null) source = this.currentEvent.source;
            if (effect == null) effect = this.effectInEvent;
        }

        if (target == null || !(target.sourceElement is PokemonCharacter) || ((PokemonCharacter)target.sourceElement).pokemonData.hp <= 0) return false;
        if (!((PokemonCharacter)target.sourceElement).pokemonData.isActive) return false;
        //if (this.gen > 5 && !target.side.foe.pokemonLeft) return false;
        RelayVar relayVar = new RelayVar(boosts: boost);
        boost = RunEvent("Boost", target, source, effect, relayVar).boosts;
        string[] boostedNames = boost.GetBoostedNames();
        foreach(string boostName in boostedNames)
        {
            int boostBy = ((PokemonCharacter)target.sourceElement).pokemonData.BoostBy(boostName, boost.GetBoostValue(boostName));

            if(boostBy != 0)
            {
                success = true;
                relayVar = new RelayVar(integerValue: boostBy);
                RunEvent("AfterEachBoost", target, source, effect, relayVar);
            }
        }
        relayVar = new RelayVar(boosts: boost);
        RunEvent("AfterBoost", target, source, effect, relayVar);

        return success;
    }

    public int Heal(int damage, TargetableElement target, BattleElement source, EffectData effect)
    {
        if (this.currentEvent != null)
        {
            if (target == null) target = this.currentEvent.target;
            if (source == null) source = this.currentEvent.source;
            if (effect == null) effect = this.effectInEvent;
        }
        if (damage < 1) damage = 1;
        RelayVar relayVar = new RelayVar(integerValue: damage);
        relayVar = RunEvent("TryHeal", target, source, effect, relayVar);
        damage = relayVar.integerValue;
        if (relayVar.getEndEvent()) return 0;
        if (!(target.sourceElement is PokemonCharacter)) return 0;
        Pokemon pokeData = ((PokemonCharacter)target.sourceElement).pokemonData;
        if (pokeData.hp <= 0 || !pokeData.isActive || pokeData.hp >= pokeData.maxhp) return 0;

        int finalDamage = pokeData.Heal(damage);
        relayVar = new RelayVar(integerValue: finalDamage);

        RunEvent("Heal", target, source, effect, relayVar);
        return finalDamage;
    }

    public class EventStatus
    {
        public EffectData status; //<--- Should be Readable only
        public string callback;
        public EffectData statusData; //<-- the effect of the element, can be modified
        public TargetableElement thing;
        public int priority;
        public int order;
        public int suborder;
        public int speed;

        public EventStatus(EffectData status, string callback, EffectData statusData = null, TargetableElement thing = null, int priority = 0)
        {
            this.status = status;
            this.callback = callback;
            this.statusData = statusData;
            this.thing = thing;
            this.priority = priority;
        }
    }

    public class Event
    {
        public string id;
        public TargetableElement target;
        public BattleElement source;
        public EffectData effect;
        //???? effectData
        //???? modifier
        //???? ceilModifier;

        public Event(string id, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
        {
            this.id = id;
            this.target = target;
            this.source = source;
            this.effect = effect;
        }
    }

    public class RelayVar
    {
        bool endEventHere = false;
        public bool booleanValue;
        public int integerValue;
        public string stringValue;
        public EffectData effectValue;
        public Globals.BoostsTable boosts;
        public Globals.Type[] types;

        public RelayVar(bool booleanValue = false, int integerValue = -1, string stringValue = "", EffectData effectValue = null, Globals.BoostsTable boosts = null, Globals.Type[] types = null)
        {
            endEventHere = false;
            this.booleanValue = booleanValue;
            this.integerValue = integerValue;
            this.stringValue = stringValue;
            this.effectValue = effectValue;
            this.boosts = boosts;
            this.types = types;
        }

        public void EndEventHere()
        {
            endEventHere = true;
        }

        public bool getEndEvent()
        {
            return endEventHere;
        }
    }

    public RelayVar SingleEvent(string eventId, EffectData effect, EffectData effectData = null, TargetableElement target = null, BattleElement source = null, EffectData sourceEffect = null, RelayVar relayVar = null)
    {
        if (eventDepth >= 8)
        {
            Debug.LogError("STACK LIMIT EXCEEDED");
        }

        if (relayVar == null) relayVar = new RelayVar();

        /*Effect has no callback with eventId*/
        if (!effect.eventMethods.HasCallback("on" + eventId)) return relayVar;

        Pokemon pokeTarget = (target == null || !(target.sourceElement is PokemonCharacter)) ? null : ((PokemonCharacter)target.sourceElement).pokemonData;
        //Status of target has changed, call it off
        if (effect.effectType == Globals.EffectTypes.Status && pokeTarget != null && pokeTarget.statusId != effect.id)
        {
            return relayVar;
        }

        /*Suppressed by Embargo, Klutz or Magic Room*/
        if (effect.effectType == Globals.EffectTypes.Item && pokeTarget != null && pokeTarget.IgnoringItem() &&
            eventId != "Start" && eventId != "TakeItem" && eventId != "Primal")
        {
            return relayVar;
        }

        /*Suppressed by Gastro Acid, ...*/
        if (effect.effectType == Globals.EffectTypes.Ability && pokeTarget != null && pokeTarget.IgnoringAbility() &&
            eventId != "End")
        {
            return relayVar;
        }

        /*Suppressed by Air Lock, Cloud Nine*/
        if (effect.effectType == Globals.EffectTypes.Weather && SuppressingWeather() &&
            eventId != "Start" && eventId != "Residual" && eventId != "End")
        {
            return relayVar;
        }        

        /*Setting the current event we are dealing with*/
        EffectData parentEffect = effectInEvent;
        EffectData parentEffectData = effectDataInEvent;
        TargetableElement parentTarget = targetInEvent;
        Event parentEvent = currentEvent;

        effectInEvent = effect;
        effectDataInEvent = effectData;
        targetInEvent = target;
        currentEvent = new Event(id: eventId, target: target, source: source, effect: sourceEffect);
        eventDepth++;

        /*Calling Callcack*/
        relayVar = effect.eventMethods.StartCallback("on" + eventId, this, relayVar, target, source, effect);

        /*Recovering last event info*/
        eventDepth--;
        effectInEvent = parentEffect;
        effectDataInEvent = parentEffectData;
        targetInEvent = parentTarget;
        currentEvent = parentEvent;

        return relayVar;
    }

    public int ComparePriority(EventStatus a, EventStatus b)
    {
        if(a.order != b.order) return -(b.order - a.order);
        if (a.priority != b.priority) return b.priority - a.priority;
        if (a.speed != b.speed) return b.speed - a.speed;
        if (a.suborder != b.suborder) return -(b.suborder - a.suborder);

        return Mathf.FloorToInt(RandomScript.GetRandomValue() - .5f);
    }

    public int ComparePokeSpeed(PokemonCharacter a, PokemonCharacter b)
    {
        if (a.pokemonData.speed != b.pokemonData.speed) return b.pokemonData.speed - a.pokemonData.speed;
        return Mathf.FloorToInt(RandomScript.GetRandomValue() - .5f);
    }

    public void EventForActives(string eventId, EffectData effect = null, RelayVar relayVar = null)
    {
        List<PokemonCharacter> actives = new List<PokemonCharacter>();
        if (effect == null && effectInEvent != null) effect = effectInEvent;

        foreach(Team t in teams)
        {
            foreach(PokemonCharacter p in t.pokemons) {
                if (p.pokemonData.isActive) actives.Add(p);
            }
        }

        actives.Sort(ComparePokeSpeed);

        foreach(PokemonCharacter poke in actives)
        {
            RunEvent(eventId, poke.targetScript, null, effect, relayVar);
        }
    }


    public RelayVar RunEvent(string eventId, TargetableElement target = null, BattleElement source = null, EffectData effect = null, RelayVar relayVar = null, bool onEffect = false, bool fastExit = false)
    {
        if (eventDepth >= 8)
        {
            Debug.LogError("STACK LIMIT EXCEEDED");
        }

        //Get relevant effects and sort them
        List<EventStatus> statuses = getRelevantEffects(target, eventId, source);
        statuses.Sort(ComparePriority);


        if (relayVar == null) relayVar = new RelayVar();

        //Current event we are dealing with
        Event parentEvent = currentEvent;
        currentEvent = new Event(id: eventId, target: target, source: source, effect: effect);
        eventDepth++;

        /*Add the callback for our effect to the beginning if oneffect is true*/
        if (onEffect && effect.eventMethods.HasCallback("on" + eventId))
        {
            statuses.Insert(0, new EventStatus(status: effect, callback: "on" + eventId, thing: target));
        }

        /*
        for (int i = 0; i < statuses.Count; ++i)
        {
            Debug.Log(statuses[i].callback + " " + statuses[i].status);
        }
        */

        /*Lets begin*/
        foreach (EventStatus currentStatus in statuses)
        {
            EffectData currentEffect = currentStatus.status;
            TargetableElement thing = currentStatus.thing;
            Pokemon pokeThing = (thing.sourceElement is PokemonCharacter) ? ((PokemonCharacter) thing.sourceElement).pokemonData : null;

            /*CurrentEffect has no callback with currentStatus.callback*/
            if (!currentEffect.eventMethods.HasCallback(currentStatus.callback)) continue;

            //Status of target has changed, call it off
            if (currentEffect.effectType == Globals.EffectTypes.Status && pokeThing != null && pokeThing.statusId != currentEffect.id)
            {
                continue;
            }

            //Suppressed by Mold Breaker, Teravolt, Turboblaze, 
            if (currentEffect.effectType == Globals.EffectTypes.Ability && !((AbilityData) currentEffect).isUnbreakable 
                && SuppressingAttackEvents(source) && thing != null && thing != GetPokemonSource(source))
            {
                if (MiscData.AttackingEvents.Contains(eventId)) continue;
                if (eventId == "Damage" && effect != null && effect.effectType == Globals.EffectTypes.Move) continue;
            }

            /*Suppressed by Embargo, Klutz or Magic Room*/
            if (currentEffect.effectType == Globals.EffectTypes.Item && pokeThing != null && pokeThing.IgnoringItem()
                && eventId != "Start" && eventId != "TakeItem" && eventId != "SwitchIn")
            {
                continue;
            }
            /*Suppressed by Gastro Acid, ...*/
            else if(currentEffect.effectType == Globals.EffectTypes.Ability && pokeThing != null && pokeThing.IgnoringAbility()
                && eventId != "End")
            {
                continue;
            }

            /*Suppressed by Air Lock, Cloud Nine*/
            if ((currentEffect.effectType == Globals.EffectTypes.Weather || eventId == "Weather") && SuppressingWeather()
                && eventId != "Residual" && eventId != "End")
            {
                continue;
            }

            //Storing last event info
            EffectData parentEffect = effectInEvent;
            EffectData parentEffectData = effectDataInEvent;
            TargetableElement parentTarget = targetInEvent;
            effectInEvent = currentEffect;
            effectDataInEvent = currentStatus.statusData;
            targetInEvent = thing;

            /*Calling Callback*/
            relayVar = currentEffect.eventMethods.StartCallback(currentStatus.callback, this, relayVar, target, source, effect);

            /*Recovering last event info*/
            effectInEvent = parentEffect;
            effectDataInEvent = parentEffectData;
            targetInEvent = parentTarget;

            if (relayVar.getEndEvent()) break; //ENDING EVENT
        }

        eventDepth--;
        currentEvent = parentEvent;    

        return relayVar;
    }

public List<EventStatus> getRelevantEffects(TargetableElement thing, string eventId, BattleElement foeThing = null)
    {
        List<EventStatus> statuses = new List<EventStatus>();


        /* Stuff in Battle (not pokemons)  */
        getRelevantEffectsBattle(statuses, thing, "on" + eventId); //on + eventId for everything


        //AFTER HERE WE ONLY CHECK STUFF FOR POKEMONS and team moves
        /*TEAM ELEMENTS*/
        //The target is a pokemon and not other active stuff (the pokemons itself will react to that, its allies to, its foes too. Maybe other active elements too if they were the source?)
        if (thing == null || !(thing.sourceElement is PokemonCharacter)) return statuses;
        Pokemon pokeThing = ((PokemonCharacter)thing.sourceElement).pokemonData;


        Team thingTeam = pokeThing.team;
        //Active moves that are side conditions (team conditions)
        getRelevantEffectsTeam(statuses, thing, thingTeam, "on" + eventId); //on + eventId for the target TEAM

        for(int i = 0; i < teams.Length; ++i)
        {
            getRelevantEffectsTeam(statuses, thing, teams[i], "onAny" + eventId); //onAny + eventId  for all TEAMS          
            if (teams[i] == thingTeam) continue;
            getRelevantEffectsTeam(statuses, thing, teams[i], "onFoe" + eventId); //onFoe + eventId  for enemy TEAMS          
        }
        
        /*FUCKING END TEAM ELEMENTS*/

        /*POKEMON ELEMENTS*/
        getRelevantEffectsPokemon(statuses, thing, "on" + eventId); //on + eventId for target pokemon

        if(foeThing is PokemonCharacter)
        {
            getRelevantEffectsPokemon(statuses, ((PokemonCharacter)foeThing).targetScript, "onSource" + eventId); //onsource + eventId for source pokemon
        }

        foreach (Team team in teams)
        {
            foreach (PokemonCharacter pokemon in team.pokemons)
            {
                if (!pokemon.pokemonData.isActive || pokemon.pokemonData.fainted) continue;
                getRelevantEffectsPokemon(statuses, pokemon.targetScript, "onAny" + eventId); //onAny + eventId for all pokemons
                if (team == pokeThing.team) getRelevantEffectsPokemon(statuses, pokemon.targetScript, "onAlly" + eventId); //onAlly + eventId for all pokemons in target team
                else getRelevantEffectsPokemon(statuses, pokemon.targetScript, "onFoe" + eventId); //onFoe + eventId for all pokemons in enemy teams
            }
        }
        /*End Pokemon elements*/

        return statuses;
    }

    void getRelevantEffectsBattle(List<EventStatus> statuses, TargetableElement thing, string callbackType)
    {
        //Active moves in battle in contact with thing? (includes pseudoweathers)
        /*
        foreach (ActiveMove activeMove in activeMoves)
        {
            Globals.EffectData moveData = activeMove.GetInfoEffect();
            if (moveData != null && moveData.eventMethods.HasCallback(callbackType))
            {
                statuses.Add(new EventStatus(status: moveData, callback: callbackType, statusData: activeMove.elementEffectData, thing: this));
                //resolvepriority
            }
        }
        */

        /*
        HERE add foething is Attrezo, or others?
        */

        //Active weather
        if (activeWeather != null )
        {
            EffectData weatherData = activeWeather.GetInfoEffect();
            if(weatherData != null && weatherData.eventMethods.HasCallback(callbackType))
            {
                statuses.Add(new EventStatus(status: weatherData, callback: callbackType, statusData: activeWeather.elementEffectData, thing: activeWeather.targetScript, priority: weatherData.GetValueFromOrderOrPriorityVariable(callbackType + "Priority")));
                //resolvepriority
            }
        }

        //Active terrain
        if (activeTerrain != null)
        {
            EffectData terrainData = activeTerrain.GetInfoEffect();
            if (terrainData != null && terrainData.eventMethods.HasCallback(callbackType))
            {
                statuses.Add(new EventStatus(status: terrainData, callback: callbackType, statusData: activeTerrain.elementEffectData, thing: activeTerrain.targetScript, priority: terrainData.GetValueFromOrderOrPriorityVariable(callbackType+"Priority")));
                //resolvepriority
            }
        }

        //Formats
        /*
        if(somewhatformat != null && somewhatformat.eventMethods.HasCallback(callbackType)) 
        {
            //push callbackType
            //resolvepriority
        }
         */

        //Extra events (dont know what was that
    }

    void getRelevantEffectsTeam(List<EventStatus> statuses, TargetableElement thing, Team thingTeam, string callbackType)
    {

        foreach (ActiveMove activeMove in thingTeam.teamMoves)
        {
            EffectData moveData = activeMove.GetInfoEffect();
            if (moveData != null && moveData.eventMethods.HasCallback(callbackType))
            {
                statuses.Add(new EventStatus(status: moveData, callback: callbackType, statusData: activeMove.elementEffectData, thing: activeMove.targetScript));
                //resolve
            }
        }
    }

    void getRelevantEffectsPokemon(List<EventStatus> statuses, TargetableElement thing, string callbackType)
    {
        if (thing == null || !(thing.sourceElement is PokemonCharacter)) return;
        Pokemon pokeThing = ((PokemonCharacter)thing.sourceElement).pokemonData;

        //Status of pokemon
        EffectData status = pokeThing.GetInfoStatus();
        if (status != null && status.eventMethods.HasCallback(callbackType))
        {
            statuses.Add(new EventStatus(status: status, callback: callbackType, statusData: pokeThing.statusData, thing: thing));
            //resolve
        }

        //Volatiles of pokemon
        foreach (KeyValuePair<string, Volatile> vol in pokeThing.volatiles) {
            //vol.key = volatile name
            //vol.value = volatile effect
            if (vol.Value.eventMethods.HasCallback(callbackType))
            {
                statuses.Add(new EventStatus(status: vol.Value, callback: callbackType, statusData: vol.Value, thing: thing));
                //resolve
            }
        }

        //Ability of pokemon
        AbilityData ability = pokeThing.GetInfoAbility();
        if (ability != null && ability.eventMethods.HasCallback(callbackType))
        {
            statuses.Add(new EventStatus(status: ability, callback: callbackType, statusData: pokeThing.abilityData, thing: thing));
            //resolve
        }

        //Item of pokemon
        ItemData item = pokeThing.GetInfoItem();
        if (item != null && item.eventMethods.HasCallback(callbackType))
        {
            statuses.Add(new EventStatus(status: item, callback: callbackType, statusData: pokeThing.itemData, thing: thing));
            //resolve
        }

        //Species of pokemon????
        /*
        Globals.TemplateData species = thing.baseTemplate;
		if (species.eventMethods.HasCallback(callbackType)) {
			//push callbacktype
            //resolve
        }
        */
    }


    TargetableElement GetPokemonSource(BattleElement source)
    {
        if (source == null) return null;
        if (!(source is ActiveMove)) return null;
        return ((ActiveMove)source).source.targetScript;
    }

    bool SuppressingAttackEvents(BattleElement source)
    {
        if (source == null) return false;

        return source.ignoreAbility;
    }

    bool SuppressingWeather()
    {
        foreach(Team t in teams)
        {
            foreach(PokemonCharacter p in t.pokemons)
            {
                if (p.pokemonData.isActive && !p.pokemonData.IgnoringAbility())
                {
                    AbilityData ability = p.pokemonData.GetInfoAbility();
                    if (ability != null && ability.suppressWeather) return true;
                }
            }
        }
        return false;
    }
}
