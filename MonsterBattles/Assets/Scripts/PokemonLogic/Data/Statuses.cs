using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses {

    public readonly static Dictionary<string, EffectData> BattleStatuses = new Dictionary<string, EffectData>
    {
        {"brn", new EffectData(
            name: "brn",
            id: "brn",
            num: 0,
            effectType: Globals.EffectTypes.Status,
            onResidualOrder: 9,
            onResidual: Callbacks.BurnOnResidual
        )},
        {"par", new EffectData(
            name: "par",
            id: "par",
            num: 0,
            effectType: Globals.EffectTypes.Status,
            onModifySpe: Callbacks.ParOnModifySpe,
            onBeforeMovePriority: 1,
            onBeforeMove: Callbacks.ParOnBeforeMove
        )},
        {"psn", new EffectData(
            name: "psn",
            id: "psn",
            num: 0,
            effectType: Globals.EffectTypes.Status,
            onResidualOrder: 9,
            onResidual: Callbacks.PsnOnResidual
        )},
        {"confusion", new EffectData(
            name: "confusion",
            id: "confusion",
            num: 0,
            // this is a volatile status
            onStart: Callbacks.ConfusionOnStart,
            onBeforeMovePriority: 3,
            onBeforeMove: Callbacks.ConfusionOnBeforeMove
        )},
        {"drain", new EffectData(
            name: "Drain",
            effectType: Globals.EffectTypes.Drain
        )},
        {"flinch", new EffectData(
            name: "flinch",
            id: "flinch",
            num: 0,
            duration: 1,
            onBeforeMovePriority: 8,
            onBeforeMove: Callbacks.FlinchOnBeforeMove
        )}
    };
}
