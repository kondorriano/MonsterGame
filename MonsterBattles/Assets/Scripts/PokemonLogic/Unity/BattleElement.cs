using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleElement : MonoBehaviour {

    //public Globals.EventMethods eventMethods;
    [HideInInspector]
    public string id;
    [HideInInspector]
    public EffectData elementEffectData;
    [HideInInspector]
    public bool ignoreAbility;

    public virtual EffectData GetInfoEffect()
    {
        Debug.LogError("Should always have an override function");
        return null;
    }
}
