using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Globals {

    public static string getId(string text)
    {
        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        return rgx.Replace(text.ToLower(), "");
    }

    public class BoostsTable
    {
        public int hp;
        public int atk;
        public int def;
        public int spa;
        public int spd;
        public int spe;
        public int accuracy;
        public int evasion;
        public float[] boostTable = { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

        public BoostsTable(int hp = 0, int atk = 0, int def = 0, int spa = 0, int spd = 0, int spe = 0, int accuracy = 0, int evasion = 0)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.spa = spa;
            this.spd = spd;
            this.spe = spe;
            this.accuracy = accuracy;
            this.evasion = evasion;
        }

        public BoostsTable ShallowCopy()
        {
            return (BoostsTable)this.MemberwiseClone();
        }

        public void SetAllBoosts(BoostsTable boosts)
        {
            this.hp = boosts.hp;
            this.atk = boosts.atk;
            this.def = boosts.def;
            this.spa = boosts.spa;
            this.spd = boosts.spd;
            this.spe = boosts.spe;
            this.accuracy = boosts.accuracy;
            this.evasion = boosts.evasion;
        }

        public int GetBoostValue(string boostName)
        {
            boostName = getId(boostName);
            switch (boostName)
            {
                case "hp":
                    return hp;
                case "atk":
                    return atk;
                case "def":
                    return def;
                case "spa":
                    return spa;
                case "spd":
                    return spd;
                case "spe":
                    return spe;
                case "accuracy":
                    return accuracy;
                case "evasion":
                    return evasion;
            }
            return -1;
        }

        public void SetBoostValue(string boostName, int boostValue)
        {
            boostName = getId(boostName);
            switch (boostName)
            {
                case "hp":
                    hp = boostValue;
                    break;
                case "atk":
                    atk = boostValue;
                    break;
                case "def":
                    def = boostValue;
                    break;
                case "spa":
                    spa = boostValue;
                    break;
                case "spd":
                    spd = boostValue;
                    break;
                case "spe":
                    spe = boostValue;
                    break;
                case "accuracy":
                    accuracy = boostValue;
                    break;
                case "evasion":
                    evasion = boostValue;
                    break;
            }
        }

        public string[] GetBoostedNames()
        {
            List<string> boosted = new List<string>();
            if (hp != 0) boosted.Add("hp");
            if (atk != 0) boosted.Add("atk");
            if (def != 0) boosted.Add("def");
            if (spa != 0) boosted.Add("spa");
            if (spd != 0) boosted.Add("spd");
            if (spe != 0) boosted.Add("spe");
            if (accuracy != 0) boosted.Add("accuracy");
            if (evasion != 0) boosted.Add("evasion");

            return boosted.ToArray();
        }

        public void ClearNegatives()
        {
            if (hp < 0) hp = 0;
            if (atk < 0) atk = 0;
            if (def < 0) def = 0;
            if (spa < 0) spa = 0;
            if (spd < 0) spd = 0;
            if (spe < 0) spe = 0;
            if (accuracy < 0) accuracy = 0;
            if (evasion < 0) evasion = 0;
        }

        public bool HasPositiveBoosts()
        {
            return hp > 0 || atk > 0 || def > 0 || spa > 0 || spd > 0 || spe > 0 || accuracy > 0 || evasion > 0;
        }
    }

    public enum ContestTypes
    {
        Beautiful,
        Clever,
        Cool,
        Cute,
        Tough,
        Null
    }

    public enum EffectTypes
    {
        Effect,
        Pokemon,
        Move,
        Item,
        Ability,
        Format,
        Ruleset,
        Weather,
        Status,
        Rule,
        ValidatorRule,
        Drain,
        Recoil,
        Null
    }

    [System.Flags]
    public enum EggGroups
    {
        None = 0,
        Amorphous = 1 << 0,
        Bug = 1 << 1,
        Ditto = 1 << 2,
        Dragon = 1 << 3,
        Fairy = 1 << 4,
        Field = 1 << 5,
        Flying = 1 << 6,
        Grass = 1 << 7,
        HumanLike = 1 << 8,
        Mineral = 1 << 9,
        Monster = 1 << 10,
        Undiscovered = 1 << 11,
        Water1 = 1 << 12,
        Water2 = 1 << 13,
        Water3 = 1 << 14,
        Length = 1 << 15
    }       

    public enum GenderName
    {
        M,
        F,
        N,
        MorF
    }

    public class HiddenPower
    {
        public Globals.Type hpType;
        public int hpPower;

        public HiddenPower(Globals.StatsTable ivs)
        {
            Globals.Type[] hpTypes = new Globals.Type[] {

                Globals.Type.Fighting, Globals.Type.Flying, Globals.Type.Poison,
                Globals.Type.Ground, Globals.Type.Rock, Globals.Type.Bug, Globals.Type.Ghost,
                Globals.Type.Steel, Globals.Type.Fire, Globals.Type.Water, Globals.Type.Grass,
                Globals.Type.Electric, Globals.Type.Psychic, Globals.Type.Ice,
                Globals.Type.Dragon, Globals.Type.Dark
            };

            int hpTypeX = 0;
            //int hpPowerX = 0;
            int i = 1;

            hpTypeX += i * (ivs.hp % 2);
            //hpPowerX += i * (ivs.hp/2) %2;
            i *= 2;

            hpTypeX += i * (ivs.atk % 2);
            //hpPowerX += i * (ivs.atk / 2) % 2;
            i *= 2;

            hpTypeX += i * (ivs.def % 2);
            //hpPowerX += i * (ivs.def / 2) % 2;
            i *= 2;

            hpTypeX += i * (ivs.spe % 2);
            //hpPowerX += i * (ivs.spe / 2) % 2;
            i *= 2;

            hpTypeX += i * (ivs.spa % 2);
            //hpPowerX += i * (ivs.spa / 2) % 2;
            i *= 2;

            hpTypeX += i * (ivs.spd % 2);
            //hpPowerX += i * (ivs.spd / 2) % 2;

            hpType = hpTypes[(hpTypeX * 15 / 63)];
            //hpPower = (hpPowerX * 40 / 63) + 30;
            hpPower = 60;
        }
    }

    public enum MoveCategory
    {
        Physical,
        Special,
        Status,
        Null
    }

    [System.Flags]
    public enum MoveFlags
    {
        None = 0,
        Authentic = 1 << 0, //Ignores a target's substitute.
        Bite = 1 << 1, //Power is multiplied by 1.5 when used by a Pokemon with the Ability Strong Jaw.
        Bullet = 1 << 2, //Has no effect on Pokemon with the Ability Bulletproof.
        Charge = 1 << 3, //The user is unable to make a move between turns.
        Contact = 1 << 4, //Makes contact.
        Dance = 1 << 5, //When used by a Pokemon, other Pokemon with the Ability Dancer can attempt to execute the same move.
        Defrost = 1 << 6, //Thaws the user if executed successfully while the user is frozen.
        Distance = 1 << 7, //Can target a Pokemon positioned anywhere in a Triple Battle.
        Gravity = 1 << 8, //Prevented from being executed or selected during Gravity's effect.
        Heal = 1 << 9, //Prevented from being executed or selected during Heal Block's effect.
        Mirror = 1 << 10, //Can be copied by Mirror Move.
        Mystery = 1 << 11, //Unknown effect.
        Nonsky = 1 << 12, //Prevented from being executed or selected in a Sky Battle.
        Powder = 1 << 13, //Has no effect on Grass-type Pokemon, Pokemon with the Ability Overcoat, and Pokemon holding Safety Goggles.
        Protect = 1 << 14, //Blocked by Detect, Protect, Spiky Shield, and if not a Status move, King's Shield.
        Pulse = 1 << 15, //Power is multiplied by 1.5 when used by a Pokemon with the Ability Mega Launcher.
        Punch = 1 << 16, //Power is multiplied by 1.2 when used by a Pokemon with the Ability Iron Fist.
        Recharge = 1 << 17, //If this move is successful, the user must recharge on the following turn and cannot make a move.
        Reflectable = 1 << 18, //Bounced back to the original user by Magic Coat or the Ability Magic Bounce.
        Snatch = 1 << 19, //Can be stolen from the original user and instead used by another Pokemon using Snatch.
        Sound = 1 << 20 //Has no effect on Pokemon with the Ability Soundproof.
    }

    public class Nature
    {
        public string name;
        public string plus;
        public string minus;

        public Nature(string name, string plus = "", string minus = "")
        {
            this.name = name;
            this.plus = plus;
            this.minus = minus;
        }
    }

    public enum OHKO
    {
        Basic,
        Ice,
        Null
    }

    public class SecondaryEffect
    {
        /*
        Ability ability;
        */
        public BoostsTable boosts;
        public int chance;
        /*
        bool dustproof;
        SelfEffect self;
        */
        public string status;
        public string volatileStatus;
        /*
        onAfterHit?: EffectData["onAfterHit"]
        onHit?: EffectData["onHit"] 
        */

        public SecondaryEffect(BoostsTable boosts = null, int chance = -1, string status ="", string volatileStatus = "")
        {
            this.boosts = boosts;
            this.chance = chance;
            this.status = status;
            this.volatileStatus = volatileStatus;
        }

        public SecondaryEffect DeepCopy()
        {
            SecondaryEffect other = (SecondaryEffect)this.MemberwiseClone();
            other.boosts = this.boosts.ShallowCopy();

            return other;
        }

        public MoveData SecondaryToMove()
        {
            MoveData effect = new MoveData(boosts: this.boosts, status: this.status, volatileStatus: this.volatileStatus );
            return effect;
        }
    }

    public class SelfEffect
    {
        BoostsTable boosts;
        int chance;
        string sideConditionId;
        string volatileStatusId;
        Callbacks.EventCallback onHit;

        public SelfEffect(BoostsTable boosts = null, int chance = 0, string sideConditionId = "", string volatileStatusId = "", Callbacks.EventCallback onHit = null)
        {
            this.boosts = boosts;
            this.chance = chance;
            this.sideConditionId = sideConditionId;
            this.volatileStatusId = volatileStatusId;
            this.onHit = onHit;
        }
    }

    [System.Serializable]
    public class StatsTable
    {
        public int hp;
        public int atk;
        public int def;
        public int spa;
        public int spd;
        public int spe;

        public StatsTable(int hp = 0, int atk = 0, int def = 0, int spa = 0, int spd = 0, int spe = 0)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.spa = spa;
            this.spd = spd;
            this.spe = spe;
        }

        public StatsTable ShallowCopy()
        {
            return (StatsTable) this.MemberwiseClone();
        }

        public void StatsTableIvs()
        {
            this.hp = 31;
            this.atk = 31;
            this.def = 31;
            this.spa = 31;
            this.spd = 31;
            this.spe = 31;
        }


        public void SetBattleStats(PokemonSet set)
        {
            //Math.floor(Math.floor(2 * stat + set.ivs[statName] + ) * set.level / 100 + 5);
            atk = (2 * atk + set.ivs.atk + (set.evs.atk / 4)) * set.level / 100 + 5;
            def = (2 * def + set.ivs.def + (set.evs.def / 4)) * set.level / 100 + 5;
            spa = (2 * spa + set.ivs.spa + (set.evs.spa / 4)) * set.level / 100 + 5;
            spd = (2 * spd + set.ivs.spd + (set.evs.spd / 4)) * set.level / 100 + 5;
            spe = (2 * spe + set.ivs.spe + (set.evs.spe / 4)) * set.level / 100 + 5;

            //HP
            //Math.floor(Math.floor(2 * stat + set.ivs['hp'] + Math.floor(set.evs['hp'] / 4) + 100) * set.level / 100 + 10);
            hp = (2 * hp + set.ivs.hp + (set.evs.hp / 4) + 100) * set.level / 100 + 10;

            NatureModify(set.nature);
        }

        void NatureModify(Nature nature)
        {
            atk = Mathf.FloorToInt(atk*NatureBoost(nature, "atk"));
            def = Mathf.FloorToInt(def * NatureBoost(nature, "def"));
            spa = Mathf.FloorToInt(spa * NatureBoost(nature, "spa"));
            spd = Mathf.FloorToInt(spd * NatureBoost(nature, "spd"));
            spe = Mathf.FloorToInt(spe * NatureBoost(nature, "spe"));
            //Cause why not
            hp = Mathf.FloorToInt(hp * NatureBoost(nature, "hp"));
        }

        float NatureBoost(Nature nature, string statName)
        {
            statName = getId(statName);
            return ((nature.plus == statName) ? 1.1f : (nature.minus == statName) ? 0.9f : 1f);
        }

        public int GetStatValue(string statName)
        {
            statName = getId(statName);
            switch(statName)
            {
                case "hp":
                    return hp;
                case "atk":
                    return atk;
                case "def":
                    return def;
                case "spa":
                    return spa;
                case "spd":
                    return spd;
                case "spe":
                    return spe;

            }
            return -1;
        }


    }

    public enum Type
    {
        Bug,
        Dark,
        Dragon,
        Electric,
        Fairy,
        Fighting,
        Fire,
        Flying,
        Ghost,
        Grass,
        Ground,
        Ice,
        Normal,
        Poison,
        Psychic,
        Rock,
        Steel,
        Water,
        Unknown,
        Null
    }

    public enum UnknownEffect
    {
        Ability,
        Item,
        Move,
        Template,
        Status,
        Weather,
        Null
    }

    //STUFF
    

    public class TypeData
    {

        public Dictionary<string, int> damageTaken;

        public TypeData(Dictionary<string, int> damageT)
        {
            damageTaken = damageT;
        }
    }
}
