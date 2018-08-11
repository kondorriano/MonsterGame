using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Natures {
    public readonly static Dictionary<string, Globals.Nature> BattleNatures = new Dictionary<string, Globals.Nature>
    {
        {"adamant", new Globals.Nature(name: "Adamant", plus: "atk", minus: "spa")},
        {"bashful", new Globals.Nature(name: "Bashful")},
        {"bold", new Globals.Nature(name: "Bold", plus: "def", minus: "atk")},
        {"brave", new Globals.Nature(name: "Brave", plus: "atk", minus: "spe")},
        {"calm", new Globals.Nature(name: "Calm", plus: "spd", minus: "atk")},
        {"careful", new Globals.Nature(name: "Careful", plus: "spd", minus: "spa")},
        {"docile", new Globals.Nature(name: "Docile")},
        {"gentle", new Globals.Nature(name: "Gentle", plus: "spd", minus: "def")},
        {"hardy", new Globals.Nature(name: "Hardy")},
        {"hasty", new Globals.Nature(name: "Hasty", plus: "spe", minus: "def")},
        {"impish", new Globals.Nature(name: "Impish", plus: "def", minus: "spa")},
        {"jolly", new Globals.Nature(name: "Jolly", plus: "spe", minus: "spa")},
        {"lax", new Globals.Nature(name: "Lax", plus: "def", minus: "spd")},
        {"lonely", new Globals.Nature(name: "Lonely", plus: "atk", minus: "def")},
        {"mild", new Globals.Nature(name: "Mild", plus: "spa", minus: "def")},
        {"modest", new Globals.Nature(name: "Modest", plus: "spa", minus: "atk")},
        {"naive", new Globals.Nature(name: "Naive", plus: "spe", minus: "spd")},
        {"naughty", new Globals.Nature(name: "Naughty", plus: "atk", minus: "spd")},
        {"quiet", new Globals.Nature(name: "Quiet", plus: "spa", minus: "spe")},
        {"quirky", new Globals.Nature(name: "Quirky")},
        {"rash", new Globals.Nature(name: "Rash", plus: "spa", minus: "spd")},
        {"relaxed", new Globals.Nature(name: "Relaxed", plus: "def", minus: "spe")},
        {"sassy", new Globals.Nature(name: "Sassy", plus: "spd", minus: "spe")},
        {"serious", new Globals.Nature(name: "Serious")},
        {"timid", new Globals.Nature(name: "Timid", plus: "spe", minus: "atk")}
    };

}
