using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnSets {

    //string[] Should be changed to a class. The class will have a move and the ways to learn it explained below
    //Learnset: {string move, string[] moveSource}
    //example Learnset: {amnesia, ["7E", "6E", "5E", "4E"]}
    /**
     * Describes a possible way to get a move onto a pokemon.
     *
     * First character is a generation number, 1-7.
     * Second character is a source ID, one of:
     *
     * - L = start or level-up, 3rd char+ is the level
     * - M = TM/HM
     * - T = tutor
     * - E = egg
     * - S = event, 3rd char+ is the index in .eventPokemon
     * - D = Dream World, only 5D is valid
     * - V = Virtual Console transfer, only 7V is valid
     * - C = NOT A REAL SOURCE, see note, only 3C/4C is valid
     *
     * C marks certain moves learned by a pokemon's prevo. It's used to
     * work around the chainbreeding checker's shortcuts for performance;
     * it lets the pokemon be a valid father for teaching the move, but
     * is otherwise ignored by the learnset checker (which will actually
     * check prevos for compatibility).
     */

    //Done just with the moves, not using the movesource. Yet.
    public readonly static Dictionary<string, string[]> BattleLearnsets = new Dictionary<string, string[]>
    {
        {"bulbasaur",
            new string[]{"amnesia","attract", "bide", "bind", "block", "bodyslam", "bulletseed", "captivate", "celebrate", "charm", "confide", "curse", "cut", "defensecurl", "doubleedge", "doubleteam",
                "echoedvoice", "endure", "energyball", "facade", "falseswipe", "flash", "frenzyplant", "frustration", "furycutter", "gigadrain", "grassknot", "grasspledge", "grasswhistle", "grassyterrain",
                "growl", "growth", "headbutt", "hiddenpower", "ingrain", "knockoff", "leafstorm", "leechseed", "lightscreen", "magicalleaf", "megadrain", "mimic", "mudslap", "naturalgift", "naturepower",
                "petaldance", "poisonpowder", "powerwhip", "protect", "rage", "razorleaf", "razorwind", "reflect", "rest", "return", "rocksmash", "round", "safeguard", "secretpower", "seedbomb", "skullbash",
                "sleeppowder", "sleeptalk", "sludge", "sludgebomb", "snore", "solarbeam", "strength", "stringshot", "substitute", "sunnyday", "swagger", "sweetscent", "swordsdance", "synthesis", "tackle",
                "takedown", "toxic", "venoshock", "vinewhip", "weatherball", "workup", "worryseed"
        }}
    };

    public static void LogLearnSet(string id)
    {
        string[] learnset = BattleLearnsets[id];
        if (learnset == null) return;
        string moves = id + " move list: ";
        for(int i = 0; i < learnset.Length; ++i)
        {
            moves += learnset[i] + ", ";
        }
        Debug.Log(moves);
    }
}
