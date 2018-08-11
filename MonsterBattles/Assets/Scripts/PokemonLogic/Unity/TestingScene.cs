using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScene : MonoBehaviour {

    Battle b;

    PokemonSet[] sets1 = new PokemonSet[]
    {
        new PokemonSet(
            speciesId: "bulbasaur",
            name: "Frodo Bulbón",
            level: 50,
            gender: Globals.GenderName.F,
            abilityId: "Overgrow",
            movesId: new string[]
            {
                "tackle"
            }
            )
    };

    PokemonSet[] sets2 = new PokemonSet[]
    {
        new PokemonSet(
            speciesId: "meowth",
            name: "Smeowg",
            level: 50,
            gender: Globals.GenderName.M,
            abilityId: "Technician",
            movesId: new string[]
            {
                "tackle"
            }
            )
    };

    //            PokemonSet set1 = new PokemonSet(   movesId: new string[]{ "testingrunevent" });

    private void Start()
    {
        Battle.Team[] teams = new Battle.Team[]
        {
            new Battle.Team(sets1),
            new Battle.Team(sets2)
        };

        b = new Battle(null, teams);
        teams[0].pokemons[0].pokemonData.isActive = true;
        teams[1].pokemons[0].pokemonData.isActive = true;

    }

}
