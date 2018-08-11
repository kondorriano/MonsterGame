using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableElement : MonoBehaviour{

    public Globals.Type consultingType = Globals.Type.Null;

    public BattleElement sourceElement = null;

    public Battle.Team teamTarget = null;
    public Battle battleTarget = null;

}
