using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities {

    public static Dictionary<string, AbilityData> BattleAbilities = new Dictionary<string, AbilityData>
    {
        {"overgrow", new AbilityData(
            desc: "When this Pokemon has 1/3 or less of its maximum HP, rounded down, its attacking stat is multiplied by 1.5 while using a Grass-type attack.",
            shortDesc: "At 1/3 or less of its max HP, this Pokemon's attacking stat is 1.5x with Grass attacks.",
            onModifyAtkPriority: 5,
            onModifyAtk: Callbacks.OvergrowOnModifyAtk,
            onModifySpAPriority: 5,
            onModifySpA: Callbacks.OvergrowOnModifySpA,
            id: "overgrow",
            name: "Overgrow",
            rating: 2,
            num: 65
        )},
        {"technician", new AbilityData(
            desc: "This Pokemon's moves of 60 power or less have their power multiplied by 1.5. Does affect Struggle.",
            shortDesc: "This Pokemon's moves of 60 power or less have 1.5x power. Includes Struggle.",
            onBasePowerPriority: 8,
            onBasePower: Callbacks.TechnicianOnBasePower,
            id: "technician",
            name: "Technician",
            rating: 4,
            num: 101
        )},
        {"testingrunevent", new AbilityData(
            desc: "When this Pokemon has 1/3 or less of its maximum HP, rounded down, its attacking stat is multiplied by 1.5 while using a Grass-type attack.",
            shortDesc: "At 1/3 or less of its max HP, this Pokemon's attacking stat is 1.5x with Grass attacks.",
            id: "testingrunevent",
            name: "testingrunevent",
            rating: 2,
            num: 65,
            onBasePower: Callbacks.TestingStuff,
            onAllyBasePower: Callbacks.TestingStuff,
            onFoeBasePower: Callbacks.TestingStuff,
            onSourceBasePower: Callbacks.TestingStuff,
            onAnyBasePower: Callbacks.TestingStuff
        )}
    };
}
