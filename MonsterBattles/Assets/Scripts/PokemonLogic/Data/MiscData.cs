using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscData {
    public readonly static string[] AttackingEvents = new string[]
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

    public readonly static string[] AbilitiesNotAffectedByGastroAcid = new string[]
    {
        "battlebond", "comatose", "disguise", "multitype", "powerconstruct", "rkssystem", "schooling", "shieldsdown", "stancechange"
    };

    public readonly static string[] AbilitiesThatCantBeChangedTo = new string[]
    {
        "illusion", "battlebond", "comatose", "disguise", "multitype", "powerconstruct", "rkssystem", "schooling", "shieldsdown", "stancechange"
    };

    public readonly static string[] AbilitiesThatCantBeChangedFrom = new string[]
    {
        "battlebond", "comatose", "disguise", "multitype", "powerconstruct", "rkssystem", "schooling", "shieldsdown", "stancechange"
    };

    public readonly static string[] ShieldVolatiles = new string[]
    {
        "banefulbunker", "kingsshield", "protect", "spikyshield"
    };

    public readonly static string[] ShieldConditions = new string[]
    {
        "craftyshield", "matblock", "quickguard", "wideguard"
    };



}
