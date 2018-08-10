using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleElement : MonoBehaviour {

    //public Globals.EventMethods eventMethods;
    public string id;
    public EffectData elementEffectData;
    public bool ignoreAbility;

    public virtual EffectData GetInfoEffect()
    {
        Debug.LogError("Should always have an override function");
        return null;
    }
}
