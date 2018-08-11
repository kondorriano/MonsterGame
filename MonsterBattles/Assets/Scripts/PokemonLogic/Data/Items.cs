using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items {
    public readonly static Dictionary<string, ItemData> BattleItems = new Dictionary<string, ItemData>
    {
        {"abomasite", new ItemData(
            id: "abomasite",
            name: "Abomasite",
            spritenum: 575,
            megaStone: "Abomasnow-Mega",
            megaEvolves: "Abomasnow",
            onTakeItem: Callbacks.MegaStoneOnTakeItem,            
		    num: 674,
		    gen: 6,
		    desc: "If held by an Abomasnow, this item allows it to Mega Evolve in battle.",
            onBasePower: Callbacks.TestingStuff,
            onAllyBasePower: Callbacks.TestingStuff,
            onFoeBasePower: Callbacks.TestingStuff,
            onSourceBasePower: Callbacks.TestingStuff,
            onAnyBasePower: Callbacks.TestingStuff
        )},
        {"normaliumz", new ItemData(
            id: "normaliumz",
            name: "Normalium Z",
            spritenum: 631,
            onTakeItem: Callbacks.EndEventOnTakeItem,
            iszMove: true,
            zMoveType: Globals.Type.Normal,
            num: 776,
            gen: 7,
            desc: "If holder has a Normal move, this item allows it to use a Normal Z-Move."
        )},
        {"pikashuniumz", new ItemData(
            id: "pikashuniumz",
            name: "Pikashunium Z",
            spritenum: 659,
            onTakeItem: Callbacks.EndEventOnTakeItem,
            iszMove: true,
            zMoveName: "10,000,000 Volt Thunderbolt",
            zMoveFrom: "Thunderbolt",
            zMoveUser: new string[] {"Pikachu-Original", "Pikachu-Hoenn", "Pikachu-Sinnoh", "Pikachu-Unova", "Pikachu-Kalos", "Pikachu-Alola", "Pikachu-Partner" },
            num: 836,
            gen: 7,
            desc: "If held by cap Pikachu with Thunderbolt, it can use 10,000,000 Volt Thunderbolt."
        )},
        {"ultranecroziumz", new ItemData(
            id: "ultranecroziumz",
            name: "Ultranecrozium Z",
            spritenum: 687,
            onTakeItem: Callbacks.EndEventOnTakeItem,
            iszMove: true,
            zMoveName: "Light That Burns the Sky",
            zMoveFrom: "Photon Geyser",
            zMoveUser: new string[] {"Necrozma-Ultra" },
            num: 923,
            gen: 7,
            desc: "Dusk Mane/Dawn Wings Necrozma: Ultra Burst, then Z-Move w/ Photon Geyser.",
            ultraEffect: "Necrozma-Ultra",
            ultraBursts: new string[] { "Necrozma-Dusk-Mane", "Necrozma-Dawn-Wings" }
        )},
        {"testingrunevent", new ItemData(
            id: "testingrunevent",
            name: "testingrunevent",
            spritenum: 575,
            num: 674,
            gen: 6,
            desc: "If held by an Abomasnow, this item allows it to Mega Evolve in battle.",
            onBasePower: Callbacks.TestingStuff,
            onAllyBasePower: Callbacks.TestingStuff,
            onFoeBasePower: Callbacks.TestingStuff,
            onSourceBasePower: Callbacks.TestingStuff,
            onAnyBasePower: Callbacks.TestingStuff
        )}




    };
}
