using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMethods
{
    //Battle.RunAction
    public Callbacks.EventCallback beforeTurnCallback;
    public Callbacks.EventCallback onAfterSwitchInSelf;
    public Callbacks.EventCallback onBeforeSwitchOut;
    public Callbacks.EventCallback onBeforeTurn;
    public Callbacks.EventCallback onCheckShow;
    public Callbacks.EventCallback onPrimal;
    //Battle.RunAction-DragIn
    public Callbacks.EventCallback onSwitchOut;
    public Callbacks.EventCallback onFoeSwitchOut;
    public Callbacks.EventCallback onPreStart;
    public Callbacks.EventCallback onSwitchIn;
    //showdon Battle.boost 
    public Callbacks.EventCallback onAfterEachBoost;
    public Callbacks.EventCallback onAfterBoost;
    public Callbacks.EventCallback onBoost;
    public Callbacks.EventCallback onAllyBoost;
    //Battle.GetDamage
    public Callbacks.EventCallback basePowerCallback; //used only by moves
    public Callbacks.EventCallback damageCallback;
    public Callbacks.EventCallback onBasePower;
    public Callbacks.EventCallback onSourceBasePower;
    public Callbacks.EventCallback onAllyBasePower;
    public Callbacks.EventCallback onFoeBasePower;
    public Callbacks.EventCallback onAnyBasePower;
    public Callbacks.EventCallback onModifyCritRatio;
    //Battle.Damage
    public Callbacks.EventCallback onAfterDamage;
    public Callbacks.EventCallback onFoeAfterDamage;
    public Callbacks.EventCallback onDamage;
    public Callbacks.EventCallback onAnyDamage;
    //Battle.SwitchIn-DragIn
    public Callbacks.EventCallback onBeforeSwitchIn;
    //Battle.faintMessages
    public Callbacks.EventCallback onFaint;
    public Callbacks.EventCallback onSourceFaint;
    public Callbacks.EventCallback onAllyFaint;
    public Callbacks.EventCallback onAnyFaint;
    //Battle.GetMoveTargets
    public Callbacks.EventCallback onRedirectTarget;
    public Callbacks.EventCallback onFoeRedirectTarget;
    public Callbacks.EventCallback onAnyRedirectTarget;
    //Battle.Heal
    public Callbacks.EventCallback onTryHeal;
    public Callbacks.EventCallback onSourceTryHeal;
    //Battle.ModifyDamage
    public Callbacks.EventCallback onModifyDamage;
    public Callbacks.EventCallback onSourceModifyDamage;
    public Callbacks.EventCallback onAnyModifyDamage;
    public Callbacks.EventCallback onWeatherModifyDamage;
    //Battle.nextTurn
    public Callbacks.EventCallback onDisableMove;
    public Callbacks.EventCallback onFoeDisableMove;
    public Callbacks.EventCallback onFoeMaybeTrapPokemon;
    public Callbacks.EventCallback onTrapPokemon;
    public Callbacks.EventCallback onFoeTrapPokemon;
    //Battle.ResidualEvent
    public Callbacks.EventCallback onResidual;
    //Battle.ResolveAction
    public Callbacks.EventCallback onModifyPriority;
    //Battle.SetWeather
    public Callbacks.EventCallback onAnySetWeather;

    //Pokemon.SetStatus-AddVolatile, Battle.SetWeather-SetTerrain-AddPseudoweather
    public Callbacks.EventCallback durationCallback;

    //Scripts.MoveHit && Battle.DragIn && Items
    public Callbacks.EventCallback onDragOut;
    public Callbacks.EventCallback onAnyDragOut;

    //Scripts.RunMove -> Pokemon.RunMove
    public Callbacks.EventCallback onOverrideAction; //Not needed anymore?
    public Callbacks.EventCallback onBeforeMove; //uses to call endEvent (doesnt return anything)
    public Callbacks.EventCallback onMoveAborted; //(doesnt return anything)
    public Callbacks.EventCallback beforeMoveCallback; //returns true if the move cant be done
    public Callbacks.EventCallback onAfterMove;
    public Callbacks.EventCallback onFoeBeforeMove;
    //Scripts.useMoveInner
    public Callbacks.EventCallback onAfterMoveSecondarySelf;
    public Callbacks.EventCallback onModifyMove;
    public Callbacks.EventCallback onAllyModifyMove;
    public Callbacks.EventCallback onTryMove;
    public Callbacks.EventCallback onFoeTryMove;
    public Callbacks.EventCallback onAnyTryMove;
    public Callbacks.EventCallback onDeductPP;
    public Callbacks.EventCallback onFoeDeductPP;

    public Callbacks.EventCallback onMoveFail;
    public Callbacks.EventCallback onUseMoveMessage;
    //Used in showdown Scripts.tryMoveHit
    public Callbacks.EventCallback onAfterMoveSecondary;
    public Callbacks.EventCallback onAccuracy; //Should be using onFoe
    public Callbacks.EventCallback onFoeAccuracy; 
    public Callbacks.EventCallback onSourceAccuracy; //Should be using on
    public Callbacks.EventCallback onAnyAccuracy;
    public Callbacks.EventCallback onTryImmunity;
    public Callbacks.EventCallback onAnyTryImmunity;
    public Callbacks.EventCallback onModifyAccuracy; //Should be using onFoe now
    public Callbacks.EventCallback onFoeModifyAccuracy;
    public Callbacks.EventCallback onSourceModifyAccuracy; //should be using on now
    public Callbacks.EventCallback onPrepareHit;
    public Callbacks.EventCallback onTry;
    //Scripts.TryMoveHit-MoveHit
    public Callbacks.EventCallback onTryHitSide;
    public Callbacks.EventCallback onAllyTryHitSide;
    public Callbacks.EventCallback onTryHit;
    public Callbacks.EventCallback onTryHitField;
    //showdown Scipts.moveHit
    public Callbacks.EventCallback onAfterHit;
    public Callbacks.EventCallback onTryPrimaryHit;
    public Callbacks.EventCallback onSourceTryPrimaryHit;
    public Callbacks.EventCallback onAnyTryPrimaryHit;
    public Callbacks.EventCallback onHit;
    public Callbacks.EventCallback onSourceHit;
    public Callbacks.EventCallback onHitField;
    public Callbacks.EventCallback onHitSide;
    public Callbacks.EventCallback onModifySecondaries;
    public Callbacks.EventCallback onSourceModifySecondaries;
    //Scripts.TryMoveHit && Pokemon.CalculateStat-GetStat 
    public Callbacks.EventCallback onModifyBoost;
    public Callbacks.EventCallback onAnyModifyBoost;

    //Scripts.RunMove && Pokemon.GetLockedMove
    public Callbacks.EventCallback onLockMove; //Not needed anymore? OnEndof turn now?

    //Pokemon.eatItem
    public Callbacks.EventCallback onEat;
    public Callbacks.EventCallback onEatItem;
    public Callbacks.EventCallback onTryEatItem;

    //showdown Pokemon.eatItem-useItem, used by Moves
    public Callbacks.EventCallback onAfterUseItem;
    public Callbacks.EventCallback onAllyAfterUseItem;

    //Pokemon.AddVolatile
    public Callbacks.EventCallback onTryAddVolatile;
    public Callbacks.EventCallback onAllyTryAddVolatile;
    //Pokemon.CopyVolatileFrom
    public Callbacks.EventCallback onCopy;
    //Pokemon.GetTypes
    public Callbacks.EventCallback onType;
    //Pokemon.GetWeight
    public Callbacks.EventCallback onModifyWeight;
    //Pokemon.runEffectiveness
    public Callbacks.EventCallback onEffectiveness;
    //Pokemon.runImmunity
    public Callbacks.EventCallback onNegateImmunity;
    //Pokemon.runStatusImmunity
    public Callbacks.EventCallback onImmunity;
    //Pokemon.SetAbility
    public Callbacks.EventCallback onSetAbility;
    //Pokemon.SetStatus
    public Callbacks.EventCallback onAfterSetStatus;
    public Callbacks.EventCallback onSetStatus;
    public Callbacks.EventCallback onAllySetStatus;
    public Callbacks.EventCallback onAnySetStatus;
    //Pokemon.TakeItem
    public Callbacks.EventCallback onTakeItem;
    //Pokemon.GetStat
    public Callbacks.EventCallback onModifySpe;

    //Battle.GetDamage, Pokemon.GetStat
    public Callbacks.EventCallback onModifyAtk;
    public Callbacks.EventCallback onSourceModifyAtk;
    public Callbacks.EventCallback onAllyModifyAtk;
    public Callbacks.EventCallback onModifySpD;
    public Callbacks.EventCallback onAllyModifySpD;
    public Callbacks.EventCallback onModifyDef;
    public Callbacks.EventCallback onFoeModifyDef;
    public Callbacks.EventCallback onModifySpA;
    public Callbacks.EventCallback onSourceModifySpA;

    //Side.chooseMove
    public Callbacks.EventCallback onLockMoveTarget;

    //Pokemon.SetAbility-RemoveVolatile && Scripts.RunMove &&Battle.Setweather-SetTerrain-RemovePseudoWeather-Dragin-FaintMessage-RunAction && Side.RemoveSideCondition
    public Callbacks.EventCallback onEnd;
    //Batte.addPseudoWeather && Pokemon.AddVolatile && Side.addSideCondition
    public Callbacks.EventCallback onRestart;
    //Battle.SetWather-SetTerrain-addPseudoWeather-dragin-RunAction && Pokemon.Setstatus-SetItem-SetAbility-AddVolatile && Side.addSideCondition
    public Callbacks.EventCallback onStart;
    //Battle.EACHEVENT-RunAction && Scripts.tryMoveHit
    public Callbacks.EventCallback onUpdate;

    //Status flinch volatile
    public Callbacks.EventCallback onFlinch;

    //Old gens??
    public Callbacks.EventCallback onAfterMoveSelf;
    public Callbacks.EventCallback onChargeMove;
    //Nextgen???
    public Callbacks.EventCallback onStallMove;

    //Move effects
    public Callbacks.EventCallback onAttract;
    //???
    public Callbacks.EventCallback onTerrain;
    public Callbacks.EventCallback onWeather;
    public Callbacks.EventCallback onAfterSubDamage;


    public EventMethods(
        /*EventMethods*/Callbacks.EventCallback basePowerCallback = null, Callbacks.EventCallback beforeMoveCallback = null, Callbacks.EventCallback beforeTurnCallback = null, Callbacks.EventCallback damageCallback = null, Callbacks.EventCallback durationCallback = null, Callbacks.EventCallback onAfterDamage = null, Callbacks.EventCallback onAfterMoveSecondary = null, Callbacks.EventCallback onAfterEachBoost = null, Callbacks.EventCallback onAfterHit = null, Callbacks.EventCallback onAfterSetStatus = null, Callbacks.EventCallback onAfterSwitchInSelf = null, Callbacks.EventCallback onAfterUseItem = null, Callbacks.EventCallback onAfterBoost = null, Callbacks.EventCallback onAfterMoveSecondarySelf = null, Callbacks.EventCallback onAfterMove = null, Callbacks.EventCallback onAfterMoveSelf = null, Callbacks.EventCallback onAllyTryAddVolatile = null, Callbacks.EventCallback onAllyBasePower = null, Callbacks.EventCallback onAllyModifyAtk = null, Callbacks.EventCallback onAllyModifySpD = null, Callbacks.EventCallback onAllyBoost = null, Callbacks.EventCallback onAllySetStatus = null, Callbacks.EventCallback onAllyTryHitSide = null, Callbacks.EventCallback onAllyFaint = null, Callbacks.EventCallback onAllyAfterUseItem = null, Callbacks.EventCallback onAllyModifyMove = null, Callbacks.EventCallback onAnyTryPrimaryHit = null, Callbacks.EventCallback onAnyTryMove = null, Callbacks.EventCallback onAnyDamage = null, Callbacks.EventCallback onAnyBasePower = null, Callbacks.EventCallback onAnySetWeather = null, Callbacks.EventCallback onAnyModifyDamage = null, Callbacks.EventCallback onAnyRedirectTarget = null, Callbacks.EventCallback onAnyAccuracy = null, Callbacks.EventCallback onAnyTryImmunity = null, Callbacks.EventCallback onAnyFaint = null, Callbacks.EventCallback onAnyModifyBoost = null, Callbacks.EventCallback onAnyDragOut = null, Callbacks.EventCallback onAnySetStatus = null, Callbacks.EventCallback onAttract = null, Callbacks.EventCallback onAccuracy = null, Callbacks.EventCallback onFoeAccuracy = null, Callbacks.EventCallback onBasePower = null, Callbacks.EventCallback onTryImmunity = null, Callbacks.EventCallback onBeforeMove = null, Callbacks.EventCallback onBeforeSwitchIn = null, Callbacks.EventCallback onBeforeSwitchOut = null, Callbacks.EventCallback onBeforeTurn = null, Callbacks.EventCallback onBoost = null, Callbacks.EventCallback onChargeMove = null, Callbacks.EventCallback onCheckShow = null, Callbacks.EventCallback onCopy = null, Callbacks.EventCallback onDamage = null, Callbacks.EventCallback onDeductPP = null, Callbacks.EventCallback onDisableMove = null, Callbacks.EventCallback onDragOut = null, Callbacks.EventCallback onEat = null, Callbacks.EventCallback onEatItem = null, Callbacks.EventCallback onEnd = null, Callbacks.EventCallback onFaint = null, Callbacks.EventCallback onFlinch = null, Callbacks.EventCallback onFoeAfterDamage = null, Callbacks.EventCallback onFoeBasePower = null, Callbacks.EventCallback onFoeBeforeMove = null, Callbacks.EventCallback onFoeDisableMove = null, Callbacks.EventCallback onFoeMaybeTrapPokemon = null, Callbacks.EventCallback onFoeModifyDef = null, Callbacks.EventCallback onFoeRedirectTarget = null, Callbacks.EventCallback onFoeSwitchOut = null, Callbacks.EventCallback onFoeTrapPokemon = null, Callbacks.EventCallback onFoeTryMove = null, Callbacks.EventCallback onHit = null, Callbacks.EventCallback onHitField = null, Callbacks.EventCallback onHitSide = null, Callbacks.EventCallback onImmunity = null, Callbacks.EventCallback onLockMove = null, Callbacks.EventCallback onLockMoveTarget = null, Callbacks.EventCallback onModifyAccuracy = null, Callbacks.EventCallback onFoeModifyAccuracy = null, Callbacks.EventCallback onModifyAtk = null, Callbacks.EventCallback onModifyBoost = null, Callbacks.EventCallback onModifyCritRatio = null, Callbacks.EventCallback onModifyDamage = null, Callbacks.EventCallback onModifyDef = null, Callbacks.EventCallback onModifyMove = null, Callbacks.EventCallback onModifyPriority = null, Callbacks.EventCallback onModifySecondaries = null, Callbacks.EventCallback onModifySpA = null, Callbacks.EventCallback onModifySpD = null, Callbacks.EventCallback onModifySpe = null, Callbacks.EventCallback onModifyWeight = null, Callbacks.EventCallback onMoveAborted = null, Callbacks.EventCallback onMoveFail = null, Callbacks.EventCallback onNegateImmunity = null, Callbacks.EventCallback onOverrideAction = null, Callbacks.EventCallback onPrepareHit = null, Callbacks.EventCallback onPreStart = null, Callbacks.EventCallback onPrimal = null, Callbacks.EventCallback onRedirectTarget = null, Callbacks.EventCallback onResidual = null, Callbacks.EventCallback onRestart = null, Callbacks.EventCallback onSetAbility = null, Callbacks.EventCallback onSetStatus = null, Callbacks.EventCallback onSourceAccuracy = null, Callbacks.EventCallback onSourceBasePower = null, Callbacks.EventCallback onSourceFaint = null, Callbacks.EventCallback onSourceHit = null, Callbacks.EventCallback onSourceModifyAccuracy = null, Callbacks.EventCallback onSourceModifyAtk = null, Callbacks.EventCallback onSourceModifyDamage = null, Callbacks.EventCallback onSourceModifySecondaries = null, Callbacks.EventCallback onSourceModifySpA = null, Callbacks.EventCallback onSourceTryHeal = null, Callbacks.EventCallback onSourceTryPrimaryHit = null, Callbacks.EventCallback onStallMove = null, Callbacks.EventCallback onStart = null, Callbacks.EventCallback onSwitchIn = null, Callbacks.EventCallback onSwitchOut = null, Callbacks.EventCallback onTakeItem = null, Callbacks.EventCallback onTerrain = null, Callbacks.EventCallback onTrapPokemon = null, Callbacks.EventCallback onTry = null, Callbacks.EventCallback onTryAddVolatile = null, Callbacks.EventCallback onTryEatItem = null, Callbacks.EventCallback onTryHeal = null, Callbacks.EventCallback onTryHit = null, Callbacks.EventCallback onTryHitField = null, Callbacks.EventCallback onTryHitSide = null, Callbacks.EventCallback onTryMove = null, Callbacks.EventCallback onTryPrimaryHit = null, Callbacks.EventCallback onType = null, Callbacks.EventCallback onUpdate = null, Callbacks.EventCallback onUseMoveMessage = null, Callbacks.EventCallback onWeather = null, Callbacks.EventCallback onWeatherModifyDamage = null, Callbacks.EventCallback onAfterSubDamage = null, Callbacks.EventCallback onEffectiveness = null, Callbacks.EventCallback onFoeDeductPP = null)
    {
        this.basePowerCallback = basePowerCallback;
        this.beforeMoveCallback = beforeMoveCallback;
        this.beforeTurnCallback = beforeTurnCallback;
        this.damageCallback = damageCallback;
        this.durationCallback = durationCallback;
        this.onAfterDamage = onAfterDamage;
        this.onAfterMoveSecondary = onAfterMoveSecondary;
        this.onAfterEachBoost = onAfterEachBoost;
        this.onAfterHit = onAfterHit;
        this.onAfterSetStatus = onAfterSetStatus;
        this.onAfterSwitchInSelf = onAfterSwitchInSelf;
        this.onAfterUseItem = onAfterUseItem;
        this.onAfterBoost = onAfterBoost;
        this.onAfterMoveSecondarySelf = onAfterMoveSecondarySelf;
        this.onAfterMove = onAfterMove;
        this.onAfterMoveSelf = onAfterMoveSelf;
        this.onAllyTryAddVolatile = onAllyTryAddVolatile;
        this.onAllyBasePower = onAllyBasePower;
        this.onAllyModifyAtk = onAllyModifyAtk;
        this.onAllyModifySpD = onAllyModifySpD;
        this.onAllyBoost = onAllyBoost;
        this.onAllySetStatus = onAllySetStatus;
        this.onAllyTryHitSide = onAllyTryHitSide;
        this.onAllyFaint = onAllyFaint;
        this.onAllyAfterUseItem = onAllyAfterUseItem;
        this.onAllyModifyMove = onAllyModifyMove;
        this.onAnyTryPrimaryHit = onAnyTryPrimaryHit;
        this.onAnyTryMove = onAnyTryMove;
        this.onAnyDamage = onAnyDamage;
        this.onAnyBasePower = onAnyBasePower;
        this.onAnySetWeather = onAnySetWeather;
        this.onAnyModifyDamage = onAnyModifyDamage;
        this.onAnyRedirectTarget = onAnyRedirectTarget;
        this.onAnyAccuracy = onAnyAccuracy;
        this.onAnyTryImmunity = onAnyTryImmunity;
        this.onAnyFaint = onAnyFaint;
        this.onAnyModifyBoost = onAnyModifyBoost;
        this.onAnyDragOut = onAnyDragOut;
        this.onAnySetStatus = onAnySetStatus;
        this.onAttract = onAttract;
        this.onAccuracy = onAccuracy;
        this.onFoeAccuracy = onFoeAccuracy;
        this.onBasePower = onBasePower;
        this.onTryImmunity = onTryImmunity;
        this.onBeforeMove = onBeforeMove;
        this.onBeforeSwitchIn = onBeforeSwitchIn;
        this.onBeforeSwitchOut = onBeforeSwitchOut;
        this.onBeforeTurn = onBeforeTurn;
        this.onBoost = onBoost;
        this.onChargeMove = onChargeMove;
        this.onCheckShow = onCheckShow;
        this.onCopy = onCopy;
        this.onDamage = onDamage;
        this.onDeductPP = onDeductPP;
        this.onDisableMove = onDisableMove;
        this.onDragOut = onDragOut;
        this.onEat = onEat;
        this.onEatItem = onEatItem;
        this.onEnd = onEnd;
        this.onFaint = onFaint;
        this.onFlinch = onFlinch;
        this.onFoeAfterDamage = onFoeAfterDamage;
        this.onFoeBasePower = onFoeBasePower;
        this.onFoeBeforeMove = onFoeBeforeMove;
        this.onFoeDisableMove = onFoeDisableMove;
        this.onFoeMaybeTrapPokemon = onFoeMaybeTrapPokemon;
        this.onFoeModifyDef = onFoeModifyDef;
        this.onFoeRedirectTarget = onFoeRedirectTarget;
        this.onFoeSwitchOut = onFoeSwitchOut;
        this.onFoeTrapPokemon = onFoeTrapPokemon;
        this.onFoeTryMove = onFoeTryMove;
        this.onHit = onHit;
        this.onHitField = onHitField;
        this.onHitSide = onHitSide;
        this.onImmunity = onImmunity;
        this.onLockMove = onLockMove;
        this.onLockMoveTarget = onLockMoveTarget;
        this.onModifyAccuracy = onModifyAccuracy;
        this.onFoeModifyAccuracy = onFoeModifyAccuracy;
        this.onModifyAtk = onModifyAtk;
        this.onModifyBoost = onModifyBoost;
        this.onModifyCritRatio = onModifyCritRatio;
        this.onModifyDamage = onModifyDamage;
        this.onModifyDef = onModifyDef;
        this.onModifyMove = onModifyMove;
        this.onModifyPriority = onModifyPriority;
        this.onModifySecondaries = onModifySecondaries;
        this.onModifySpA = onModifySpA;
        this.onModifySpD = onModifySpD;
        this.onModifySpe = onModifySpe;
        this.onModifyWeight = onModifyWeight;
        this.onMoveAborted = onMoveAborted;
        this.onMoveFail = onMoveFail;
        this.onNegateImmunity = onNegateImmunity;
        this.onOverrideAction = onOverrideAction;
        this.onPrepareHit = onPrepareHit;
        this.onPreStart = onPreStart;
        this.onPrimal = onPrimal;
        this.onRedirectTarget = onRedirectTarget;
        this.onResidual = onResidual;
        this.onRestart = onRestart;
        this.onSetAbility = onSetAbility;
        this.onSetStatus = onSetStatus;
        this.onSourceAccuracy = onSourceAccuracy;
        this.onSourceBasePower = onSourceBasePower;
        this.onSourceFaint = onSourceFaint;
        this.onSourceHit = onSourceHit;
        this.onSourceModifyAccuracy = onSourceModifyAccuracy;
        this.onSourceModifyAtk = onSourceModifyAtk;
        this.onSourceModifyDamage = onSourceModifyDamage;
        this.onSourceModifySecondaries = onSourceModifySecondaries;
        this.onSourceModifySpA = onSourceModifySpA;
        this.onSourceTryHeal = onSourceTryHeal;
        this.onSourceTryPrimaryHit = onSourceTryPrimaryHit;
        this.onStallMove = onStallMove;
        this.onStart = onStart;
        this.onSwitchIn = onSwitchIn;
        this.onSwitchOut = onSwitchOut;
        this.onTakeItem = onTakeItem;
        this.onTerrain = onTerrain;
        this.onTrapPokemon = onTrapPokemon;
        this.onTry = onTry;
        this.onTryAddVolatile = onTryAddVolatile;
        this.onTryEatItem = onTryEatItem;
        this.onTryHeal = onTryHeal;
        this.onTryHit = onTryHit;
        this.onTryHitField = onTryHitField;
        this.onTryHitSide = onTryHitSide;
        this.onTryMove = onTryMove;
        this.onTryPrimaryHit = onTryPrimaryHit;
        this.onType = onType;
        this.onUpdate = onUpdate;
        this.onUseMoveMessage = onUseMoveMessage;
        this.onWeather = onWeather;
        this.onWeatherModifyDamage = onWeatherModifyDamage;
        this.onAfterSubDamage = onAfterSubDamage;
        this.onEffectiveness = onEffectiveness;

        //new
        this.onFoeDeductPP = onFoeDeductPP;
    }

    public EventMethods ShallowCopy()
    {
        return (EventMethods)this.MemberwiseClone();
    }

    public bool HasCallback(string callbackName)
    {
        if (this.GetType().GetField(callbackName) == null) return false;
        if (this.GetType().GetField(callbackName).GetValue(this) == null) return false;
        return true;
    }

    public Battle.RelayVar StartCallback(string callbackName, Battle battle, Battle.RelayVar relayVar, TargetableElement target, BattleElement source, EffectData effect)
    {
        return ((Callbacks.EventCallback)this.GetType().GetField(callbackName).GetValue(this))(battle, relayVar, target, source, effect);
    }
}
