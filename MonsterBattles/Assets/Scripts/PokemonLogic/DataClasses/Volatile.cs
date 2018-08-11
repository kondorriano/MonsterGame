using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volatile : EffectData
{
    public TargetableElement target;
    public BattleElement source;
    public EffectData sourceEffect;

    public List<BattleElement> linkedSources;
    public string linkedStatus;

    public Volatile(EffectData status)
    {
        /*EventMethods*/
        this.eventMethods = status.eventMethods;
        /*EffectData*/
        this.id = status.id;
        this.name = status.name;
        this.num = status.num;
        this.affectsFainted = status.affectsFainted;
        this.desc = status.desc;
        this.drain = status.drain;
        this.effect = status.effect;
        this.effectType = status.effectType;
        this.isZ = status.isZ;

        this.onModifyAccuracyPriority = status.onModifyAccuracyPriority;
        this.onModifyAtkPriority = status.onModifyAtkPriority;
        this.onModifyCritRatioPriority = status.onModifyCritRatioPriority;
        this.onModifyDefPriority = status.onModifyDefPriority;
        this.onModifyMovePriority = status.onModifyMovePriority;
        this.onModifyPriorityPriority = status.onModifyPriorityPriority;
        this.onModifySpAPriority = status.onModifySpAPriority;
        this.onModifySpDPriority = status.onModifySpDPriority;
        this.onModifyWeightPriority = status.onModifyWeightPriority;

        this.onResidualOrder = status.onResidualOrder;
        this.secondaries = status.secondaries;
        this.shortDesc = status.shortDesc;

        /*Effect*/
        this.exists = status.exists;

        linkedSources = new List<BattleElement>();
    }

    public void SetData(TargetableElement target = null, BattleElement source = null, EffectData sourceEffect = null, int duration = -1)
    {
        this.target = target;
        this.source = source;
        this.sourceEffect = sourceEffect;
    }
}
