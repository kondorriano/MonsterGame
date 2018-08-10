using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
List of flags and their descriptions:
authentic: Ignores a target's substitute.
bite: Power is multiplied by 1.5 when used by a Pokemon with the Ability Strong Jaw.
bullet: Has no effect on Pokemon with the Ability Bulletproof.
charge: The user is unable to make a move between turns.
contact: Makes contact.
dance: When used by a Pokemon, other Pokemon with the Ability Dancer can attempt to execute the same move.
defrost: Thaws the user if executed successfully while the user is frozen.
distance: Can target a Pokemon positioned anywhere in a Triple Battle.
gravity: Prevented from being executed or selected during Gravity's effect.
heal: Prevented from being executed or selected during Heal Block's effect.
mirror: Can be copied by Mirror Move.
mystery: Unknown effect.
nonsky: Prevented from being executed or selected in a Sky Battle.
powder: Has no effect on Grass-type Pokemon, Pokemon with the Ability Overcoat, and Pokemon holding Safety Goggles.
protect: Blocked by Detect, Protect, Spiky Shield, and if not a Status move, King's Shield.
pulse: Power is multiplied by 1.5 when used by a Pokemon with the Ability Mega Launcher.
punch: Power is multiplied by 1.2 when used by a Pokemon with the Ability Iron Fist.
recharge: If this move is successful, the user must recharge on the following turn and cannot make a move.
reflectable: Bounced back to the original user by Magic Coat or the Ability Magic Bounce.
snatch: Can be stolen from the original user and instead used by another Pokemon using Snatch.
sound: Has no effect on Pokemon with the Ability Soundproof.
*/

public class Moves {
    public static Dictionary<string, MoveData> BattleMovedex = new Dictionary<string, MoveData>
    {
        {"10000000voltthunderbolt", new MoveData(
            num: 719,
            accuracy: -1,
            basePower: 195,
            category: Globals.MoveCategory.Special,
            desc: "Has a very high chance for a critical hit.",
            shortDesc: "Very high critical hit ratio.",
            id: "10000000voltthunderbolt",
            name: "10,000,000 Volt Thunderbolt",
            pp: 1,
            priority: 0,
            isZ: "pikashuniumz",
            critRatio: 3,
            type: Globals.Type.Electric,
            contestType: Globals.ContestTypes.Cool
        )},
        {"absorb", new MoveData(
            num: 71,
            accuracy: 100,
            basePower: 20,
            category: Globals.MoveCategory.Special,
            desc: "The user recovers 1/2 the HP lost by the target, rounded half up. If Big Root is held by the user, the HP recovered is 1.3x normal, rounded half down.",
            shortDesc: "User recovers 50% of the damage dealt.",
            id: "absorb",
            name: "Absorb",
            pp: 25,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror | Globals.MoveFlags.Heal,
            drain: new int[] {1, 2},
            type: Globals.Type.Grass,
            zMovePower: 100,
            contestType: Globals.ContestTypes.Clever
        )},
        {"tackle", new MoveData(
            num: 33,
            accuracy: 100,
            basePower: 40,
            category: Globals.MoveCategory.Physical,
            desc: "No additional effect.",
            shortDesc: "No additional effect.",
            id: "tackle",
            name: "Tackle",
            pp: 35,
            priority: 0,
            flags: Globals.MoveFlags.Contact | Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror,
            type: Globals.Type.Normal,
            zMovePower: 100,
            contestType: Globals.ContestTypes.Tough,
            onBasePower: Callbacks.TestingStuff,
            onAllyBasePower: Callbacks.TestingStuff,
            onFoeBasePower: Callbacks.TestingStuff,
            onSourceBasePower: Callbacks.TestingStuff,
            onAnyBasePower: Callbacks.TestingStuff
        )},
        {"test", new MoveData(
            onModifyAtk: Callbacks.TestOnModifyAtk,
            id: "DisIsATest",
            drain: new int[] {1, 2},
            onModifyAtkPriority: 2

        )},
        {"testingrunevent", new MoveData(
            num: 33,
            accuracy: 100,
            basePower: 60,
            category: Globals.MoveCategory.Physical,
            desc: "No additional effect.",
            shortDesc: "No additional effect.",
            id: "testingrunevent",
            name: "testingrunevent",
            pp: 35,
            priority: 0,
            flags: Globals.MoveFlags.Contact | Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror,
            type: Globals.Type.Normal,
            zMovePower: 120,
            contestType: Globals.ContestTypes.Tough,
            onBasePower: Callbacks.TestingStuff,
            onAllyBasePower: Callbacks.TestingStuff,
            onFoeBasePower: Callbacks.TestingStuff,
            onSourceBasePower: Callbacks.TestingStuff,
            onAnyBasePower: Callbacks.TestingStuff
        )},
    };
}
