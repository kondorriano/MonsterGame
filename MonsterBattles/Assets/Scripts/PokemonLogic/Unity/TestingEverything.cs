using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEverything : MonoBehaviour {

    public string pokemonid;
    public Globals.Type typeAttack;
    public Globals.Type typeDefense;

    public Globals.StatsTable hpIvs = new Globals.StatsTable(hp: 30, atk: 31, def: 31, spe: 31, spa: 30, spd: 31);




    private void Start()
    {

    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Z))
        {
            //TYPECHART
            int value = TypeChart.BattleTypeChart["" + typeDefense].damageTaken["" + typeAttack];
            string result = (value == 0) ? " is ok against " : (value == 1) ? " is super effective against " : (value == 2) ? "is not very effective against " : (value == 3) ? " does not affect " : "wat";
            Debug.Log(typeAttack + result + typeDefense);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //LOG TEMPLATEDATA
            LogPokemon();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {

            //BOOSTMODIFIERS STUFF
            int previousMod = 6144;
            int nextMod = 5325;
            int nextValue = (previousMod * nextMod + 2048);
            int test = nextValue >> 12;
            float end = (float)test / 4096f;
            float wat = (6144f / 4096f) * (5325f / 4096f);
            Debug.Log("((previousMod * nextMod + 2048) >> 12) / 4096 \n" +
                    "((" + previousMod + "*" + nextMod + "+ 2048) >> 12) / 4096 \n" +
                    "((" + previousMod + " * " + nextMod + " + 2048) = " + nextValue + "\n" +
                    nextValue + " >> 12 = " + test + "\n" +
                    test + " / 4096 = " + end
                );

            Debug.Log("" + 6144f/4096f + " * " + 5325f/4096f + " = " + wat);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //LOG BATTLE STATS
            Globals.StatsTable baseStats = new Globals.StatsTable(hp: 45, atk: 49, def: 49, spa: 65, spd: 65, spe: 45);
            Globals.StatsTable stats = baseStats.ShallowCopy();// new Globals.StatsTable(hp: 45, atk: 49, def: 49, spa: 65, spd: 65, spe: 45);
            PokemonSet set = new PokemonSet();
            set.ivs = new Globals.StatsTable();
            set.evs = new Globals.StatsTable();
            set.level = 50;
            set.nature = new Globals.Nature("Testing");//, plus: "hp", minus: "spe");

            stats.SetBattleStats(set);
            Debug.Log("Testing Stats: hp: " + stats.hp + ", atk:" + stats.atk + ", def:" + stats.def + ", spa:" + stats.spa + ", spd:" + stats.spd + ", spe:" + stats.spe);
            Debug.Log("Base stats used: hp: " + baseStats.hp + ", atk:" + baseStats.atk + ", def:" + baseStats.def + ", spa:" + baseStats.spa + ", spd:" + baseStats.spd + ", spe:" + baseStats.spe);

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //LOG HIDDEN POWER
            Globals.HiddenPower hp = new Globals.HiddenPower(hpIvs);
            Debug.Log("Hidden Power: " + hp.hpType + " " + hp.hpPower);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //LOG CAN MEGA EVOLVE
            TemplateData templateData = Pokedex.BattlePokedex["rayquaza"];
            PokemonSet set = new PokemonSet();
            set.itemId = "abomasite";
            set.movesId = new string[] { "tackle" };
            Debug.Log("Can mega evolve " + CanMegaEvolve(templateData, set));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //LOG CAN ULTRA BURST
            TemplateData templateData = Pokedex.BattlePokedex["necrozmadawnwings"];
            PokemonSet set = new PokemonSet();
            set.itemId = "ultranecroziumz";
            set.movesId = new string[] {"tackle" };
            Debug.Log("Can ultra burst " + CanUltraBurst(templateData, set));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //LOG POKEMON DATA
            Battle battle = new Battle();
            PokemonSet set = new PokemonSet(
                speciesId: "bulbasaur",
                name: "Flamenco",
                level: 50,
                gender: Globals.GenderName.M,
                happiness: 100,
                pokeball: "MajoBall",
                shiny: false,
                abilityId: "overgrow",
                itemId: "normaliumz",
                movesId: new string[] {"tackle", "absorb", "caca","10000000voltthunderbolt" },
                evs: new Globals.StatsTable(spd: 252, def: 252, spe: 6),
                ivs: BestHiddenPowers.HiddenPowers["Ice"],
                nature: Natures.BattleNatures["adamant"],
                ppBoosts: new int[] {3,1,0,2}
            );
            Pokemon testPoke = new Pokemon(battle, set, new Battle.Team(), null);
            testPoke.LogPokemon();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //TESTING CALLBACK GETTERS AND CALLERS
            MoveData move = Moves.BattleMovedex["test"];
            Debug.Log(move.id);

            Debug.Log(move.eventMethods.HasCallback("onModifyAtk"));
            Debug.Log(move.eventMethods.HasCallback("onTakeItem"));
            Debug.Log(move.eventMethods.HasCallback("caca"));
            Debug.Log(move.eventMethods.HasCallback("id"));

            Battle.RelayVar relayVar = new Battle.RelayVar();

            Debug.Log("I was a " + relayVar.integerValue + " before");
            move.eventMethods.StartCallback("onModifyAtk", null, relayVar, null, null, null);
            Debug.Log("But now, thanks to the StartCallback method, I am a " + relayVar.integerValue);

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //Single event test
            Battle b = new Battle();
            MoveData move = Moves.BattleMovedex["test"];

            Battle.RelayVar tororo = b.SingleEvent("ModifyAtk", move);
            Debug.Log(tororo.integerValue);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {

        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            Battle b = new Battle();
            Battle.Team t1 = new Battle.Team();
            PokemonSet set1 = new PokemonSet(speciesId: "bulbasaur", name: "Tentomon", level: 50, gender: Globals.GenderName.F, abilityId: "testingrunevent", itemId: "testingrunevent", movesId: new string[]{ "testingrunevent" });
            t1.pokemonsSets = new PokemonSet[] { set1 };
            GameObject go1 = new GameObject();
            PokemonCharacter pc1 = go1.AddComponent<PokemonCharacter>();
            pc1.Init(b, set1, t1);
            pc1.pokemonData.isActive = true;
            t1.pokemons = new PokemonCharacter[] {pc1 };
            t1.teamMoves = new ActiveMove[0];

            Battle.Team t2 = new Battle.Team();
            PokemonSet set2 = new PokemonSet(speciesId: "bulbasaur", name: "Tentomon", level: 50, gender: Globals.GenderName.F, abilityId: "testingrunevent", itemId: "testingrunevent", movesId: new string[] { "testingrunevent" });
            t2.pokemonsSets = new PokemonSet[] { set2 };
            GameObject go2 = new GameObject();
            PokemonCharacter pc2 = go2.AddComponent<PokemonCharacter>();
            pc2.Init(b, set2, t2);
            pc2.pokemonData.isActive = true;
            t2.pokemons = new PokemonCharacter[] { pc2 };
            t2.teamMoves = new ActiveMove[0];


            b.teams = new Battle.Team[] { t1,t2};

            MoveData move = Moves.BattleMovedex["testingrunevent"];
            Battle.RelayVar relayVar = new Battle.RelayVar(integerValue: move.basePower);
            relayVar = b.RunEvent("BasePower", pc1.targetScript, pc2, move, relayVar, true);
            Debug.Log(relayVar.integerValue);

            //public int GetDamage(Pokemon pokemon, Pokemon target, ActiveMove activeMove, int directDamage = -1)//movedata comes from active move?? //DirectDamage for confusion and stuff
            GameObject go3 = new GameObject();
            ActiveMove am = go3.AddComponent<ActiveMove>();
            am.activeData = new ActiveMove.ActiveMoveData(move.id);
            int damage = b.GetDamage(pc1.pokemonData, pc2.pokemonData, am);

            Debug.Log("Damage " + damage);




        }






    }

    void LogPokemon()
    {
        if(!Pokedex.BattlePokedex.ContainsKey(pokemonid))
        {
            Debug.Log(pokemonid + " is not in the pokedex database");
        } else
        {
            Pokedex.BattlePokedex[pokemonid].LogTemplateData(pokemonid);
        }

        if(!LearnSets.BattleLearnsets.ContainsKey(pokemonid))
        {
            Debug.Log(pokemonid + "'s learnset is not in the learnset database");
            return;
        }
    
        //We have move list, lets log it and check the moves
        LearnSets.LogLearnSet(pokemonid);
        string[] moveList = LearnSets.BattleLearnsets[pokemonid];
        MoveData move;

        for (int i = 0; i < moveList.Length; ++i)
        {
            if (!Moves.BattleMovedex.ContainsKey(moveList[i])) continue;
            move = Moves.BattleMovedex[moveList[i]];
            Debug.Log(move.name);
        }
    }


    bool CanMegaEvolve(TemplateData baseTemplate, PokemonSet set)
    {
        //Has no alternate forms, It should have one in the template
        if (baseTemplate.otherFormes == null) return false;

        ItemData item = (Items.BattleItems.ContainsKey(set.itemId) ? Items.BattleItems[set.itemId] : null);
        TemplateData altForme;
        for (int i = 0; i < baseTemplate.otherFormes.Length; ++i)
        {
            //Doesnt exist in the database
            if (!Pokedex.BattlePokedex.ContainsKey(baseTemplate.otherFormes[i])) continue;
            altForme = Pokedex.BattlePokedex[baseTemplate.otherFormes[i]];
            //Not a mega form
            if (!altForme.isMega) continue;
            //Check if can evolve by Move
            if (altForme.requiredMove != "")
            {
                //Holds a Z item, so, cant mega evolve
                if (item != null && item.iszMove) continue;

                //Checking if the pokemon has the required move
                for (int j = 0; j < set.movesId.Length; ++j)
                {
                    //If the pokemon has the move and it is in the database
                    if (altForme.requiredMove == set.movesId[j] && Moves.BattleMovedex.ContainsKey(set.movesId[j])) return true;
                }
            }
            //Check if can evolve by item
            if (item == null) continue;
            //if it megaevolves from the baseSpecies and the pokemon is not the megaevolution
            if (item.megaEvolves == baseTemplate.baseSpecies && item.megaStone != baseTemplate.species && item.megaStone == altForme.species) return true;
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

        ItemData item = (Items.BattleItems.ContainsKey(set.itemId) ? Items.BattleItems[set.itemId] : null);
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
}



/*
OPCIONES:
el activemove tiene distintos eventmethod variables para casa
U olvidarse del tema, seguir con otras cosas y posiblemente hacer tu propio sistema de callbacks, sabiendo qué quieres
 
in move - in effect
3-0    beforeMoveCallback?: (this: Battle, pokemon: Pokemon) => void
8-0    beforeTurnCallback?: (this: Battle, pokemon: Pokemon, target: Pokemon) => void
9-0    damageCallback?: (this: Battle, pokemon: Pokemon, target: Pokemon) => number | false
0-14    durationCallback?: (this: Battle, target: Pokemon, source: Pokemon, effect: UnknownEffect) => number
0-3    onAfterDamage?: (this: Battle, damage: number, target: Pokemon, soruce: Pokemon, move: Move) => void
       onAfterMoveSecondary?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
       onAfterEachBoost?: (this: Battle, boost: SparseBoostsTable, target: Pokemon, source: Pokemon) => void
3-0    onAfterHit?: (this: Battle, source: Pokemon, target: Pokemon, move: Move) => void
       onAfterSetStatus?: (this: Battle, status: Status, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onAfterSwitchInSelf?: (this: Battle, pokemon: Pokemon) => void
       onAfterUseItem?: (this: Battle, item: Item, pokemon: Pokemon) => void
       onAfterBoost?: (this: Battle, boost: SparseBoostsTable, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
2-0    onAfterMoveSecondarySelf?: (this: Battle, source: Pokemon, target: Pokemon, move: Move) => void
6-0    onAfterMove?: (this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void
       onAfterMoveSelf?: (this: Battle, pokemon: Pokemon) => void
       onAllyTryAddVolatile?: (this: Battle, status: Status, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onAllyBasePower?: (this: Battle, basePower: number, attacker: Pokemon, defender: Pokemon, move: Move) => void
       onAllyModifyAtk?: (this: Battle, atk: number) => void
       onAllyModifySpD?: (this: Battle, spd: number) => void
       onAllyBoost?: (this: Battle, boost: SparseBoostsTable, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onAllySetStatus?: (this: Battle, status: Status, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
0-1    onAllyTryHitSide?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
       onAllyFaint?: (this: Battle, target: Pokemon) => void
       onAllyAfterUseItem?: (this: Battle, item: Item, pokemon: Pokemon) => void
       onAllyModifyMove?: (this: Battle, move: Move) => void
       onAnyTryPrimaryHit?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
0-1    onAnyTryMove?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
       onAnyDamage?: (this: Battle, damage: number, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
0-1    onAnyBasePower?: (this: Battle, basePower: number, source: Pokemon, target: Pokemon, move: Move) => void
       onAnySetWeather?: (this: Battle, target: Pokemon, source: Pokemon, weather: Weather) => void
0-3    onAnyModifyDamage?: (this: Battle, damage: number, source: Pokemon, target: Pokemon, move: Move) => void
       onAnyRedirectTarget?: (this: Battle, target: Pokemon, source: Pokemon, source2: Pokemon, move: Move) => void
       onAnyAccuracy?: (this: Battle, accuracy: number, target: Pokemon, source: Pokemon, move: Move) => void
0-1    onAnyTryImmunity?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
       onAnyFaint?: (this: Battle) => void
       onAnyModifyBoost?: (this: Battle, boosts: SparseBoostsTable, target: Pokemon) => void
0-1    onAnyDragOut?: (this: Battle, pokemon: Pokemon) => void
0-1    onAnySetStatus?: (this: Battle, status: Status, pokemon: Pokemon) => void
       onAttract?: (this: Battle, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
0-2    onAccuracy?: (this: Battle, accuracy: number, target: Pokemon, source: Pokemon, move: Move) => number | boolean | null | void
9-9    onBasePower?: (this: Battle, basePower: number, pokemon: Pokemon, target: Pokemon, move: Move) => void
0-6    onTryImmunity?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
0-10    onBeforeMove?: (this: Battle, attacker: Pokemon, defender: Pokemon, move: Move) => void
       onBeforeSwitchIn?: (this: Battle, pokemon: Pokemon) => void
0-2    onBeforeSwitchOut?: (this: Battle, pokemon: Pokemon) => void
       onBeforeTurn?: (this: Battle, pokemon: Pokemon) => void
0-1    onBoost?: (this: Battle, boost: SparseBoostsTable, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onChargeMove?: (this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void
       onCheckShow?: (this: Battle, pokemon: Pokemon) => void
0-1    onCopy?: (this: Battle, pokemon: Pokemon) => void
0-2    onDamage?: (this: Battle, damage: number, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onDeductPP?: (this: Battle, target: Pokemon, source: Pokemon) => number | void
0-7    onDisableMove?: (this: Battle, pokemon: Pokemon) => void
0-1    onDragOut?: (this: Battle, pokemon: Pokemon) => void
       onEat?: ((this: Battle, pokemon: Pokemon) => void) | false
       onEatItem?: (this: Battle, item: Item, pokemon: Pokemon) => void
0-39    onEnd?: (this: Battle, pokemon: Pokemon) => void
0-3    onFaint?: (this: Battle, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onFlinch?: ((this: Battle, pokemon: Pokemon) => void) | boolean
0-1    onFoeAfterDamage?: (this: Battle, damage: number, target: Pokemon) => void
       onFoeBasePower?: (this: Battle, basePower: number, attacker: Pokemon, defender: Pokemon, move: Move) => void
0-2    onFoeBeforeMove?: (this: Battle, attacker: Pokemon, defender: Pokemon, move: Move) => void
0-1    onFoeDisableMove?: (this: Battle, pokemon: Pokemon) => void
       onFoeMaybeTrapPokemon?: (this: Battle, pokemon: Pokemon, source: Pokemon) => void
       onFoeModifyDef?: (this: Battle, def: number, pokemon: Pokemon) => number
0-3    onFoeRedirectTarget?: (this: Battle, target: Pokemon, source: Pokemon, source2: UnknownEffect, move: Move) => void
0-1    onFoeSwitchOut?: (this: Battle, pokemon: Pokemon) => void
0-1    onFoeTrapPokemon?: (this: Battle, pokemon: Pokemon) => void
       onFoeTryMove?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
84-7-secon 6-self 4    onHit?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
4-0    onHitField?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => boolean | void
4-0    onHitSide?: (this: Battle, side: Side, source: Pokemon) => void
0-4    onImmunity?: (this: Battle, type: string, pokemon: Pokemon) => void
0-4    onLockMove?: string | ((this: Battle, pokemon: Pokemon) => void)
       onLockMoveTarget?: (this: Battle) => number
0-1    onModifyAccuracy?: (this: Battle, accuracy: number, target: Pokemon, source: Pokemon, move: Move) => number | void
       onModifyAtk?: (this: Battle, atk: number, attacker: Pokemon, defender: Pokemon, move: Move) => number | void
0-2    onModifyBoost?: (this: Battle, boosts: SparseBoostsTable) => void
0-2    onModifyCritRatio?: (this: Battle, critRatio: number, source: Pokemon, target: Pokemon) => number | void
       onModifyDamage?: (this: Battle, damage: number, source: Pokemon, target: Pokemon, move: Move) => number | void
       onModifyDef?: (this: Battle, def: number, pokemon: Pokemon) => number | void
23-3    onModifyMove?: (this: Battle, move: Move, pokemon: Pokemon, target: Pokemon) => void
       onModifyPriority?: (this: Battle, priority: number, pokemon: Pokemon, target: Pokemon, move: Move) => number | void
       onModifySecondaries?: (this: Battle, secondaries: SecondaryEffect[]) => void
       onModifySpA?: (this: Battle, atk: number, attacker: Pokemon, defender: Pokemon, move: Move) => number | void
       onModifySpD?: (this: Battle, spd: number, pokemon: Pokemon) => number | void
0-2    onModifySpe?: (this: Battle, spe: number, pokemon: Pokemon) => number | void
0-1    onModifyWeight?: (this: Battle, weight: number, pokemon: Pokemon) => number | void
1-2    onMoveAborted?: (this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void
3-0    onMoveFail?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
0-2    onNegateImmunity?: ((this: Battle, pokemon: Pokemon, type: string) => void) | boolean
0-1    onOverrideAction?: (this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void
9-0    onPrepareHit?: (this: Battle, source: Pokemon, target: Pokemon, move: Move) => void
       onPreStart?: (this: Battle, pokemon: Pokemon) => void
       onPrimal?: (this: Battle, pokemon: Pokemon) => void
0-4    onRedirectTarget?: (this: Battle, target: Pokemon, source: Pokemon, source2: UnknownEffect) => void
0-12    onResidual?: (this: Battle, pokemon: Pokemon, source: Pokemon, effect: UnknownEffect) => void
0-17    onRestart?: (this: Battle, pokemon: Pokemon, source: Pokemon) => void
       onSetAbility?: (this: Battle, ability: string, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
0-3    onSetStatus?: (this: Battle, status: Status, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
0-1    onSourceAccuracy?: (this: Battle, accuracy: number, target: Pokemon, source: Pokemon, move: Move) => void
0-1    onSourceBasePower?: (this: Battle, basePower: number, attacker: Pokemon, defender: Pokemon, move: Move) => void
       onSourceFaint?: (this: Battle, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onSourceHit?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
       onSourceModifyAccuracy?: (this: Battle, accuracy: number, target: Pokemon, source: Pokemon) => number | void
       onSourceModifyAtk?: (this: Battle, atk: number, attacker: Pokemon, defender: Pokemon, move: Move) => number | void
0-4    onSourceModifyDamage?: (this: Battle, damage: number, source: Pokemon, target: Pokemon, move: Move) => number | void
       onSourceModifySecondaries?: (this: Battle, secondaries: SecondaryEffect[], target: Pokemon, source: Pokemon, move: Move) => void
       onSourceModifySpA?: (this: Battle, atk: number, attacker: Pokemon, defender: Pokemon, move: Move) => number | void
       onSourceTryHeal?: (this: Battle, damage: number, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onSourceTryPrimaryHit?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
       onStallMove?: (this: Battle, pokemon: Pokemon) => void
0-88    onStart?: (this: Battle, pokemon: Pokemon, source: Pokemon, effect: UnknownEffect, move: Move) => void
0-6    onSwitchIn?: (this: Battle, pokemon: Pokemon) => void
       onSwitchOut?: (this: Battle, pokemon: Pokemon) => void
       onTakeItem?: ((this: Battle, item: Item, pokemon: Pokemon, source: Pokemon) => void) | false
0-1    onTerrain?: (this: Battle, pokemon: Pokemon) => void
0-2    onTrapPokemon?: (this: Battle, pokemon: Pokemon) => void
25-0    onTry?: (this: Battle, attacker: Pokemon, defender: Pokemon, move: Move) => void
0-3    onTryAddVolatile?: (this: Battle, status: Status, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onTryEatItem?: (this: Battle, item: Item, pokemon: Pokemon) => void
0-1    onTryHeal?: ((this: Battle, damage: number, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void) | boolean
55-10    onTryHit?: ((this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void) | boolean
      onTryHitField?: (this: Battle, target: Pokemon, source: Pokemon) => boolean | void
5-0    onTryHitSide?: (this: Battle, side: Side, source: Pokemon) => void
3-1    onTryMove?: (this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void
0-1    onTryPrimaryHit?: (this: Battle, target: Pokemon, source: Pokemon, move: Move) => void
0-1    onType?: (this: Battle, types: string[], pokemon: Pokemon) => void
0-2    onUpdate?: (this: Battle, pokemon: Pokemon) => void
1-0    onUseMoveMessage?: (this: Battle, pokemon: Pokemon, target: Pokemon, move: Move) => void
       onWeather?: (this: Battle, target: Pokemon, source: Pokemon, effect: UnknownEffect) => void
       onWeatherModifyDamage?: (this: Battle, damage: number, attacker: Pokemon, defender: Pokemon, move: Move) => number | void

    // multiple definitions due to relayVar (currently not supported)
2-0    onAfterSubDamage?: (this: Battle, damage: any, target: any, source: any, effect: any) => void
3-0    onEffectiveness?: (this: Battle, typeMod: any, target: any, type: any, move: any) => void
*/
