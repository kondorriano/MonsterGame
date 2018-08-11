using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : EffectData
{
    public int rating;
    public bool isUnbreakable;
    public bool suppressWeather;

    /*NewlyAdded*/
    public TargetableElement target;
    public AbilityData(
        /*EventMethods*/Callbacks.EventCallback basePowerCallback = null, Callbacks.EventCallback beforeMoveCallback = null, Callbacks.EventCallback beforeTurnCallback = null, Callbacks.EventCallback damageCallback = null, Callbacks.EventCallback durationCallback = null, Callbacks.EventCallback onAfterDamage = null, Callbacks.EventCallback onAfterMoveSecondary = null, Callbacks.EventCallback onAfterEachBoost = null, Callbacks.EventCallback onAfterHit = null, Callbacks.EventCallback onAfterSetStatus = null, Callbacks.EventCallback onAfterSwitchInSelf = null, Callbacks.EventCallback onAfterUseItem = null, Callbacks.EventCallback onAfterBoost = null, Callbacks.EventCallback onAfterMoveSecondarySelf = null, Callbacks.EventCallback onAfterMove = null, Callbacks.EventCallback onAfterMoveSelf = null, Callbacks.EventCallback onAllyTryAddVolatile = null, Callbacks.EventCallback onAllyBasePower = null, Callbacks.EventCallback onAllyModifyAtk = null, Callbacks.EventCallback onAllyModifySpD = null, Callbacks.EventCallback onAllyBoost = null, Callbacks.EventCallback onAllySetStatus = null, Callbacks.EventCallback onAllyTryHitSide = null, Callbacks.EventCallback onAllyFaint = null, Callbacks.EventCallback onAllyAfterUseItem = null, Callbacks.EventCallback onAllyModifyMove = null, Callbacks.EventCallback onAnyTryPrimaryHit = null, Callbacks.EventCallback onAnyTryMove = null, Callbacks.EventCallback onAnyDamage = null, Callbacks.EventCallback onAnyBasePower = null, Callbacks.EventCallback onAnySetWeather = null, Callbacks.EventCallback onAnyModifyDamage = null, Callbacks.EventCallback onAnyRedirectTarget = null, Callbacks.EventCallback onAnyAccuracy = null, Callbacks.EventCallback onAnyTryImmunity = null, Callbacks.EventCallback onAnyFaint = null, Callbacks.EventCallback onAnyModifyBoost = null, Callbacks.EventCallback onAnyDragOut = null, Callbacks.EventCallback onAnySetStatus = null, Callbacks.EventCallback onAttract = null, Callbacks.EventCallback onAccuracy = null, Callbacks.EventCallback onFoeAccuracy = null, Callbacks.EventCallback onBasePower = null, Callbacks.EventCallback onTryImmunity = null, Callbacks.EventCallback onBeforeMove = null, Callbacks.EventCallback onBeforeSwitchIn = null, Callbacks.EventCallback onBeforeSwitchOut = null, Callbacks.EventCallback onBeforeTurn = null, Callbacks.EventCallback onBoost = null, Callbacks.EventCallback onChargeMove = null, Callbacks.EventCallback onCheckShow = null, Callbacks.EventCallback onCopy = null, Callbacks.EventCallback onDamage = null, Callbacks.EventCallback onDeductPP = null, Callbacks.EventCallback onDisableMove = null, Callbacks.EventCallback onDragOut = null, Callbacks.EventCallback onEat = null, Callbacks.EventCallback onEatItem = null, Callbacks.EventCallback onEnd = null, Callbacks.EventCallback onFaint = null, Callbacks.EventCallback onFlinch = null, Callbacks.EventCallback onFoeAfterDamage = null, Callbacks.EventCallback onFoeBasePower = null, Callbacks.EventCallback onFoeBeforeMove = null, Callbacks.EventCallback onFoeDisableMove = null, Callbacks.EventCallback onFoeMaybeTrapPokemon = null, Callbacks.EventCallback onFoeModifyDef = null, Callbacks.EventCallback onFoeRedirectTarget = null, Callbacks.EventCallback onFoeSwitchOut = null, Callbacks.EventCallback onFoeTrapPokemon = null, Callbacks.EventCallback onFoeTryMove = null, Callbacks.EventCallback onHit = null, Callbacks.EventCallback onHitField = null, Callbacks.EventCallback onHitSide = null, Callbacks.EventCallback onImmunity = null, Callbacks.EventCallback onLockMove = null, Callbacks.EventCallback onLockMoveTarget = null, Callbacks.EventCallback onModifyAccuracy = null, Callbacks.EventCallback onFoeModifyAccuracy = null, Callbacks.EventCallback onModifyAtk = null, Callbacks.EventCallback onModifyBoost = null, Callbacks.EventCallback onModifyCritRatio = null, Callbacks.EventCallback onModifyDamage = null, Callbacks.EventCallback onModifyDef = null, Callbacks.EventCallback onModifyMove = null, Callbacks.EventCallback onModifyPriority = null, Callbacks.EventCallback onModifySecondaries = null, Callbacks.EventCallback onModifySpA = null, Callbacks.EventCallback onModifySpD = null, Callbacks.EventCallback onModifySpe = null, Callbacks.EventCallback onModifyWeight = null, Callbacks.EventCallback onMoveAborted = null, Callbacks.EventCallback onMoveFail = null, Callbacks.EventCallback onNegateImmunity = null, Callbacks.EventCallback onOverrideAction = null, Callbacks.EventCallback onPrepareHit = null, Callbacks.EventCallback onPreStart = null, Callbacks.EventCallback onPrimal = null, Callbacks.EventCallback onRedirectTarget = null, Callbacks.EventCallback onResidual = null, Callbacks.EventCallback onRestart = null, Callbacks.EventCallback onSetAbility = null, Callbacks.EventCallback onSetStatus = null, Callbacks.EventCallback onSourceAccuracy = null, Callbacks.EventCallback onSourceBasePower = null, Callbacks.EventCallback onSourceFaint = null, Callbacks.EventCallback onSourceHit = null, Callbacks.EventCallback onSourceModifyAccuracy = null, Callbacks.EventCallback onSourceModifyAtk = null, Callbacks.EventCallback onSourceModifyDamage = null, Callbacks.EventCallback onSourceModifySecondaries = null, Callbacks.EventCallback onSourceModifySpA = null, Callbacks.EventCallback onSourceTryHeal = null, Callbacks.EventCallback onSourceTryPrimaryHit = null, Callbacks.EventCallback onStallMove = null, Callbacks.EventCallback onStart = null, Callbacks.EventCallback onSwitchIn = null, Callbacks.EventCallback onSwitchOut = null, Callbacks.EventCallback onTakeItem = null, Callbacks.EventCallback onTerrain = null, Callbacks.EventCallback onTrapPokemon = null, Callbacks.EventCallback onTry = null, Callbacks.EventCallback onTryAddVolatile = null, Callbacks.EventCallback onTryEatItem = null, Callbacks.EventCallback onTryHeal = null, Callbacks.EventCallback onTryHit = null, Callbacks.EventCallback onTryHitField = null, Callbacks.EventCallback onTryHitSide = null, Callbacks.EventCallback onTryMove = null, Callbacks.EventCallback onTryPrimaryHit = null, Callbacks.EventCallback onType = null, Callbacks.EventCallback onUpdate = null, Callbacks.EventCallback onUseMoveMessage = null, Callbacks.EventCallback onWeather = null, Callbacks.EventCallback onWeatherModifyDamage = null, Callbacks.EventCallback onAfterSubDamage = null, Callbacks.EventCallback onEffectiveness = null, Callbacks.EventCallback onFoeDeductPP = null,
        /*EffectData*/ string id = "", string name = "", int num = -1, bool affectsFainted = false, string desc = "", int[] drain = null, int duration = -1, EffectData effect = null, string isZ = "", int onBasePowerPriority = 0,
        /*EffectData (modifier priorities)*/ int onModifyAccuracyPriority = 0, int onModifyAtkPriority = 0, int onModifyCritRatioPriority = 0, int onModifyDefPriority = 0, int onModifyMovePriority = 0, int onModifyPriorityPriority = 0, int onModifySpAPriority = 0, int onModifySpDPriority = 0, int onModifyWeightPriority = 0,
        /*EffectData*/ int onResidualOrder = 0, int[] recoil = null, Globals.SecondaryEffect[] secondaries = null, Globals.SelfEffect self = null, string shortDesc = "",
        /*EffectType*/ Globals.EffectTypes effectType = Globals.EffectTypes.Ability,
        /*Effect*/ bool exists = false,
        /*AbilityData*/ int rating = 0, bool isUnbreakable = false, bool suppressWeather = false)
    {
        /*EventMethods*/
        this.eventMethods = new EventMethods(basePowerCallback, beforeMoveCallback, beforeTurnCallback, damageCallback, durationCallback, onAfterDamage, onAfterMoveSecondary, onAfterEachBoost, onAfterHit, onAfterSetStatus, onAfterSwitchInSelf, onAfterUseItem, onAfterBoost, onAfterMoveSecondarySelf, onAfterMove, onAfterMoveSelf, onAllyTryAddVolatile, onAllyBasePower, onAllyModifyAtk, onAllyModifySpD, onAllyBoost, onAllySetStatus, onAllyTryHitSide, onAllyFaint, onAllyAfterUseItem, onAllyModifyMove, onAnyTryPrimaryHit, onAnyTryMove, onAnyDamage, onAnyBasePower, onAnySetWeather, onAnyModifyDamage, onAnyRedirectTarget, onAnyAccuracy, onAnyTryImmunity, onAnyFaint, onAnyModifyBoost, onAnyDragOut, onAnySetStatus, onAttract, onAccuracy, onFoeAccuracy, onBasePower, onTryImmunity, onBeforeMove, onBeforeSwitchIn, onBeforeSwitchOut, onBeforeTurn, onBoost, onChargeMove, onCheckShow, onCopy, onDamage, onDeductPP, onDisableMove, onDragOut, onEat, onEatItem, onEnd, onFaint, onFlinch, onFoeAfterDamage, onFoeBasePower, onFoeBeforeMove, onFoeDisableMove, onFoeMaybeTrapPokemon, onFoeModifyDef, onFoeRedirectTarget, onFoeSwitchOut, onFoeTrapPokemon, onFoeTryMove, onHit, onHitField, onHitSide, onImmunity, onLockMove, onLockMoveTarget, onModifyAccuracy, onFoeModifyAccuracy, onModifyAtk, onModifyBoost, onModifyCritRatio, onModifyDamage, onModifyDef, onModifyMove, onModifyPriority, onModifySecondaries, onModifySpA, onModifySpD, onModifySpe, onModifyWeight, onMoveAborted, onMoveFail, onNegateImmunity, onOverrideAction, onPrepareHit, onPreStart, onPrimal, onRedirectTarget, onResidual, onRestart, onSetAbility, onSetStatus, onSourceAccuracy, onSourceBasePower, onSourceFaint, onSourceHit, onSourceModifyAccuracy, onSourceModifyAtk, onSourceModifyDamage, onSourceModifySecondaries, onSourceModifySpA, onSourceTryHeal, onSourceTryPrimaryHit, onStallMove, onStart, onSwitchIn, onSwitchOut, onTakeItem, onTerrain, onTrapPokemon, onTry, onTryAddVolatile, onTryEatItem, onTryHeal, onTryHit, onTryHitField, onTryHitSide, onTryMove, onTryPrimaryHit, onType, onUpdate, onUseMoveMessage, onWeather, onWeatherModifyDamage, onAfterSubDamage, onEffectiveness, onFoeDeductPP);
        /*EffectData*/
        this.id = id;
        this.name = name;
        this.num = num;
        this.affectsFainted = affectsFainted;
        this.desc = desc;
        this.duration = duration;
        this.drain = drain;
        this.effect = effect;
        this.effectType = effectType;
        this.isZ = isZ;

        this.onBasePowerPriority = onBasePowerPriority;

        this.onModifyAccuracyPriority = onModifyAccuracyPriority;
        this.onModifyAtkPriority = onModifyAtkPriority;
        this.onModifyCritRatioPriority = onModifyCritRatioPriority;
        this.onModifyDefPriority = onModifyDefPriority;
        this.onModifyMovePriority = onModifyMovePriority;
        this.onModifyPriorityPriority = onModifyPriorityPriority;
        this.onModifySpAPriority = onModifySpAPriority;
        this.onModifySpDPriority = onModifySpDPriority;
        this.onModifyWeightPriority = onModifyWeightPriority;

        this.onResidualOrder = onResidualOrder;
        this.recoil = recoil;
        this.secondaries = secondaries;
        this.self = self;
        this.shortDesc = shortDesc;

        /*Effect*/
        this.exists = exists;

        /*AbilityData*/
        this.effectType = effectType;
        this.rating = rating;
        this.isUnbreakable = isUnbreakable;
        this.suppressWeather = suppressWeather;

        /*NewlyAdded*/
        this.target = null;
    }


}
