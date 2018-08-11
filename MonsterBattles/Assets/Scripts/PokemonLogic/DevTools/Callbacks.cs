using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Callbacks {

    public delegate Battle.RelayVar EventCallback(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null);


    public static Battle.RelayVar TestOnModifyAtk(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        relayVar.integerValue = 10;
        return relayVar;
    }

    //OnStart
    public static Battle.RelayVar ConfusionOnStart(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        battle.effectDataInEvent.time = RandomScript.RandomBetween(2, 6);
        return relayVar;
    }

    //OnBeforeMove
    public static Battle.RelayVar ParOnBeforeMove(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        if (RandomScript.RandomChance(1, 4))
        {
            relayVar.EndEventHere();
        }
        return relayVar;
    }

    public static Battle.RelayVar FlinchOnBeforeMove(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        Debug.Log("You are not sup-POSED to be hia?");
        if (battle.RunEvent("Flinch", target).getEndEvent()) return relayVar;
        relayVar.EndEventHere();
        return relayVar;
    }

    public static Battle.RelayVar ConfusionOnBeforeMove(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        Pokemon poke = ((PokemonCharacter)target.sourceElement).pokemonData;
        //Discount time not here anymore

        if (!RandomScript.RandomChance(1, 3)) return relayVar;
        battle.Damage(battle.GetDamage(poke,poke, null, 40), poke.targetData, poke.myPokemon, 
            new MoveData(id: "confused", 
                effectType: Globals.EffectTypes.Move, 
                type: Globals.Type.Unknown));

        relayVar.EndEventHere();
        return relayVar;
    }

    //onBasePower
    public static Battle.RelayVar TechnicianOnBasePower(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        int basePower = relayVar.integerValue;
        if (basePower <= 60)
        {
            relayVar.integerValue = Mathf.FloorToInt(basePower * 1.5f);
        }
        return relayVar;
    }

    //OnModifyAtk
    public static Battle.RelayVar HustleOnModifyAtk(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        //battle.modify(relayVar.integerValue, 1.5);
        return relayVar;
    }

    public static Battle.RelayVar OvergrowOnModifyAtk(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        MoveData move = (MoveData)effect;
        Pokemon attacker = ((PokemonCharacter)target.sourceElement).pokemonData;
        if (move.type == Globals.Type.Grass && attacker.hp <= attacker.maxhp / 3)
        {
            Debug.Log("Overgrow boost Atk");
            // battle.chainModify(1.5); //TODO
        }
        return relayVar;
    }

    //OnModifySpa
    public static Battle.RelayVar OvergrowOnModifySpA(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        MoveData move = (MoveData)effect;
        Pokemon attacker = ((PokemonCharacter)target.sourceElement).pokemonData;

        if (move.type == Globals.Type.Grass && attacker.hp <= attacker.maxhp / 3)
        {
            Debug.Log("Overgrow boost Spa");
            // this.chainModify(1.5); //TODO
        }
        return relayVar;
    }

    //OnModifySpe
    public static Battle.RelayVar ParOnModifySpe(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        if (!(target.sourceElement is PokemonCharacter)) return relayVar;
        Pokemon pokemon = ((PokemonCharacter)target.sourceElement).pokemonData;
        if (pokemon.HasAbilityActive(new string[] { "quickfeet" }))
        {
            relayVar.integerValue = Mathf.FloorToInt(relayVar.integerValue * .5f);
        }
        return relayVar;
    }

    //OnResidual
    public static Battle.RelayVar BurnOnResidual(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        Pokemon pokemon = ((PokemonCharacter)target.sourceElement).pokemonData;
        battle.Damage(pokemon.maxhp / 16);
        return relayVar;
    }
    public static Battle.RelayVar PsnOnResidual(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        Pokemon pokemon = ((PokemonCharacter)target.sourceElement).pokemonData;
        battle.Damage(pokemon.maxhp / 8);
        return relayVar;
    }

    //OnTakeItem
    public static Battle.RelayVar MegaStoneOnTakeItem(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        ItemData item = (ItemData)relayVar.effectValue;
        Pokemon pSource = ((PokemonCharacter) source).pokemonData;
        if (item.megaEvolves == pSource.baseTemplate.baseSpecies) relayVar.EndEventHere();
        else relayVar.booleanValue = true;

        return relayVar;

    }

    public static Battle.RelayVar EndEventOnTakeItem(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        relayVar.EndEventHere();
        return relayVar;
    }

    public static Battle.RelayVar TestingStuff(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        relayVar.integerValue++;
        return relayVar;
    }

}
