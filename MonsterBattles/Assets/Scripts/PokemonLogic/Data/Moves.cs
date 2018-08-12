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
    public readonly static Dictionary<string, MoveData> BattleMovedex = new Dictionary<string, MoveData>
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
        { "sludgebomb", new MoveData(
            num: 188,
            accuracy: 100,
            basePower: 90,
            category: Globals.MoveCategory.Special,
            desc: "Has a 30% chance to poison the target.",
            shortDesc: "30% chance to poison the target.",
            id: "sludgebomb",
            isViable: true,
            name: "Sludge Bomb",
            pp: 10,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror | Globals.MoveFlags.Bullet,
            secondaries: new Globals.SecondaryEffect[]
            {
                new Globals.SecondaryEffect(
                chance: 30,
                status: "psn"
                )
            },
            type: Globals.Type.Poison,
            zMovePower: 175,
            contestType: Globals.ContestTypes.Tough
        )},
        {"gigadrain", new MoveData(
            num: 202,
            accuracy: 100,
            basePower: 75,
            category: Globals.MoveCategory.Special,
            desc: "The user recovers 1/2 the HP lost by the target, rounded half up. If Big Root is held by the user, the HP recovered is 1.3x normal, rounded half down.",
            shortDesc: "User recovers 50% of the damage dealt.",
            id: "gigadrain",
            isViable: true,
            name: "Giga Drain",
            pp: 10,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror | Globals.MoveFlags.Heal,
            drain: new int[]{1, 2 },
            type: Globals.Type.Grass,
            zMovePower: 140,
            contestType: Globals.ContestTypes.Clever
        )},
        {"bodyslam", new MoveData(
            num: 34,
            accuracy: 100,
            basePower: 85,
            category: Globals.MoveCategory.Physical,
            desc: "Has a 30% chance to paralyze the target. Damage doubles and no accuracy check is done if the target has used Minimize while active.",
            shortDesc: "30% chance to paralyze the target.",
            id: "bodyslam",
            isViable: true,
            name: "Body Slam",
            pp: 15,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror | Globals.MoveFlags.Contact | Globals.MoveFlags.Nonsky,
            secondaries: new Globals.SecondaryEffect[]
            {
                new Globals.SecondaryEffect(
                chance: 30,
                status: "par"
                )
            },
            type: Globals.Type.Normal,
            zMovePower: 160,
            contestType: Globals.ContestTypes.Tough
            
        )},
        {"payday", new MoveData(
            num: 6,
            accuracy: 100,
            basePower: 40,
            category: Globals.MoveCategory.Physical,
            desc: "No additional effect.",
            shortDesc: "Scatters coins.",
            id: "payday",
            name: "Pay Day",
            pp: 20,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror,
            type: Globals.Type.Normal,
            zMovePower: 100,
            contestType: Globals.ContestTypes.Clever
        )},
        {"waterpulse", new MoveData(
            num: 352,
            accuracy: 100,
            basePower: 60,
            category: Globals.MoveCategory.Special,
            desc: "Has a 20% chance to confuse the target.",
            shortDesc: "20% chance to confuse the target.",
            id: "waterpulse",
            name: "Water Pulse",
            pp: 20,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror | Globals.MoveFlags.Pulse | Globals.MoveFlags.Distance,
            secondaries: new Globals.SecondaryEffect[]
            {
                new Globals.SecondaryEffect(
                chance: 30,
                volatileStatus: "confusion"
                )
            },
            type: Globals.Type.Water,
            zMovePower: 120,
            contestType: Globals.ContestTypes.Beautiful
        )},
        {"aerialace", new MoveData(
            num: 332,
            accuracy: -1,
            basePower: 60,
            category: Globals.MoveCategory.Physical,
            desc: "This move does not check accuracy.",
            shortDesc: "This move does not check accuracy.",
            id: "aerialace",
            isViable: true,
            name: "Aerial Ace",
            pp: 20,
            priority: 0,
            flags: Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror | Globals.MoveFlags.Contact | Globals.MoveFlags.Distance,
            type: Globals.Type.Flying,
            zMovePower: 120,
            contestType: Globals.ContestTypes.Cool
        )},
        {"bite", new MoveData(
            num: 44,
            accuracy: 100,
            basePower: 60,
            category: Globals.MoveCategory.Physical,
            desc: "Has a 30% chance to flinch the target.",
            shortDesc: "30% chance to flinch the target.",
            id: "bite",
            name: "Bite",
            pp: 25,
            priority: 0,
            flags: Globals.MoveFlags.Bite | Globals.MoveFlags.Contact | Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror,
            secondaries: new Globals.SecondaryEffect[]
            {
                new Globals.SecondaryEffect(
                chance: 30,
                volatileStatus: "flinch"
                )
            },
            type: Globals.Type.Dark,
            zMovePower: 120,
            contestType: Globals.ContestTypes.Tough
        )},
        {"rocksmash", new MoveData(
            num: 249,
            accuracy: 100,
            basePower: 40,
            category: Globals.MoveCategory.Physical,
            desc: "Has a 50% chance to lower the target's Defense by 1 stage.",
            shortDesc: "50% chance to lower the target's Defense by 1.",
            id: "rocksmash",
            name: "Rock Smash",
            pp: 15,
            priority: 0,
            flags: Globals.MoveFlags.Contact | Globals.MoveFlags.Protect | Globals.MoveFlags.Mirror,
            secondaries: new Globals.SecondaryEffect[]
            {
                new Globals.SecondaryEffect(
                chance: 50,
                boosts: new Globals.BoostsTable(def: -1)
                )
            },
            type: Globals.Type.Fighting,
            zMovePower: 100,
            contestType: Globals.ContestTypes.Tough
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
