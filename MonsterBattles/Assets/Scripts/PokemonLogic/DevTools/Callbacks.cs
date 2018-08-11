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

    //OnResidual
    public static Battle.RelayVar BurnOnResidual(Battle battle, Battle.RelayVar relayVar, TargetableElement target = null, BattleElement source = null, EffectData effect = null)
    {
        Pokemon pokemon = ((PokemonCharacter)target.sourceElement).pokemonData;
        pokemon.Damage(pokemon.maxhp / 16);
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
