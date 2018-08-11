using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses {

    public static Dictionary<string, EffectData> BattleStatuses = new Dictionary<string, EffectData>
    {
        {"brn", new EffectData(
            name: "brn",
            id: "brn",
            num: 0,
            effectType: Globals.EffectTypes.Status,
            onResidualOrder: 9,
            onResidual: Callbacks.BurnOnResidual
        )}

    };
}
