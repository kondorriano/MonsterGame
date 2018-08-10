using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscData {
    public static string[] AttackingEvents = new string[]
    {
        "BeforeMove",
        "BasePower",
        "Immunity",
        "RedirectTarget",
        "Heal",
        "SetStatus",
        "CriticalHit",
        "ModifyAtk", "ModifyDef", "ModifySpA", "ModifySpD", "ModifySpe", "ModifyAccuracy",
        "ModifyBoost",
        "ModifyDamage",
        "ModifySecondaries",
        "ModifyWeight",
        "TryAddVolatile",
        "TryHit",
        "TryHitSide",
        "TryMove",
        "Boost",
        "DragOut",
        "Effectiveness"
    };

    public static string[] AbilitiesNotAffectedByGastroAcid = new string[]
    {
        "battlebond", "comatose", "disguise", "multitype", "powerconstruct", "rkssystem", "schooling", "shieldsdown", "stancechange"
    };

    public static string[] AbilitiesThatCantBeChangedTo = new string[]
    {
        "illusion", "battlebond", "comatose", "disguise", "multitype", "powerconstruct", "rkssystem", "schooling", "shieldsdown", "stancechange"
    };

    public static string[] AbilitiesThatCantBeChangedFrom = new string[]
    {
        "battlebond", "comatose", "disguise", "multitype", "powerconstruct", "rkssystem", "schooling", "shieldsdown", "stancechange"
    };

    public static string[] ShieldVolatiles = new string[]
    {
        "banefulbunker", "kingsshield", "protect", "spikyshield"
    };

    public static string[] ShieldConditions = new string[]
    {
        "craftyshield", "matblock", "quickguard", "wideguard"
    };



}
