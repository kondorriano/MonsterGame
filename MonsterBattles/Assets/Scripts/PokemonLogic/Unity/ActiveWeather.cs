using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetableElement))]
public class ActiveWeather : BattleElement
{

    public TargetableElement targetScript;


    public void Init()
    {
        targetScript = GetComponent<TargetableElement>();
        targetScript.sourceElement = this;
    }

    public override EffectData GetInfoEffect()
    {
        if (!Statuses.BattleStatuses.ContainsKey(id)) return null;
        return Statuses.BattleStatuses[id];
    }
}
