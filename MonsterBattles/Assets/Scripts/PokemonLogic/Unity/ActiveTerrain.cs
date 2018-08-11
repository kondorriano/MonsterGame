using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetableElement))]
public class ActiveTerrain : BattleElement
{
    public TargetableElement targetScript;


    public void Init()
    {
        targetScript = GetComponent<TargetableElement>();
        targetScript.sourceElement = this;
    }

    public override EffectData GetInfoEffect()
    {
        if (!Moves.BattleMovedex.ContainsKey(id)) return null;
        return Moves.BattleMovedex[id];
    }
}
