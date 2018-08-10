using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexData {

    public class Tools
    {
        //IMPORTANT
        //An ID must have only lowercase alphanumeric characters
        public static string getID(string text)
        {
            if (text == null) return "";
            return ("" + text).ToLower();
        }
    }

    public class Effect
    {
        /**
		 * ID. This will be a lowercase version of the name with all the
		 * non-alphanumeric characters removed. So, for instance, "Mr. Mime"
		 * becomes "mrmime", and "Basculin-Blue-Striped" becomes
		 * "basculinbluestriped".
		 */
        public string id;
        /**
		 * Name. Currently does not support Unicode letters, so "Flabébé"
		 * is "Flabebe" and "Nidoran♀" is "Nidoran-F".
		 */
        public string name;
        /**
		 * Full name. Prefixes the name with the effect type. For instance,
		 * Leftovers would be "item: Leftovers", confusion the status
		 * condition would be "confusion", etc.
		 */
        public string fullname;
        /**
		 * Effect type.
		 */
        public Globals.EffectTypes effectType;
        /**
		 * Does it exist? For historical reasons, when you use an accessor
		 * for an effect that doesn't exist, you get a dummy effect that
		 * doesn't do anything, and this field set to false.
		 */
        public bool exists;
        /**
		 * Dex number? For a Pokemon, this is the National Dex number. For
		 * other effects, this is often an internal ID (e.g. a move
		 * number). Not all effects have numbers, this will be 0 if it
		 * doesn't. Nonstandard effects (e.g. CAP effects) will have
		 * negative numbers.
		 */
        public int num;
        /**
		 * The generation of Pokemon game this was INTRODUCED (NOT
		 * necessarily the current gen being simulated.) Not all effects
		 * track generation; this will be 0 if not known.
		 */
        public int gen;
        /**
		 * A shortened form of the description of this effect. Not all effects have this
		 */
        public string shortDesc;
        /**
		 * The full description for this effect
		 */
        public string desc;
        /**
		 * The duration of the effect.
		 */
        public int duration;
        /**
		 * Whether or not the effect is ignored by Baton Pass.
		 */
        public bool noCopy;
        /**
		 * Whether or not the effect affects fainted Pokemon.
		 */
        public bool affectsFainted;
        /**
		 * The status that the effect may cause.
		 */
        public string status;
        /**
		 * The weather that the effect may cause.
		 */
        public string weather;
        /**
		 * HP that the effect may drain.
		 * @type {number[] | undefined}
		 */
        public int[] drain;

        /*
		 * @type {AnyObject}		 
            this.flags = this.flags || {}; 
         */

         /**
		 * @type {string}
            this.sourceEffect = this.sourceEffect || '';
        */

        public virtual void Init(/*data, moreData = null, moreData2 = null*/)
        {
            id = "";
            name = "";
            fullname = "";
            effectType = Globals.EffectTypes.Effect;
            exists = true;
            num = 0;
            gen = 0;
            shortDesc = "";
            desc = "";

            /*
            Object.assign(this, data);
		    if (moreData) Object.assign(this, moreData);
		    if (moreData2) Object.assign(this, moreData2);
		    this.name = Tools.getString(this.name).trim();
		    this.fullname = Tools.getString(this.fullname) || this.name;
		    if (!this.id) this.id = toId(this.name); // Hidden Power hack
		    // @ts-ignore
		    this.effectType = Tools.getString(this.effectType) || "Effect";
            this.exists = !!(this.exists && this.id);
            */
        }

        public string toString()
        {
            return name;
        }


    }

    public class Move : Effect
    {
        public enum IgnoreImmunityTypes {
            Bug = 0,
            Dark = 1,
            Dragon = 2,
            Electric = 3,
            Fairy = 4,
            Fighting = 5,
            Fire = 6,
            Flying = 7,
            Ghost = 8,
            Grass = 9,
            Ground = 10,
            Ice = 11,
            Normal = 12,
            Poison = 13,
            Psychic = 14,
            Rock = 15,
            Steel = 16,
            Water = 17,
            None = 18,
            All = 19
        }

        public enum MoveDamageType
        {
            Basic,
            Level,
            Static
        }

        public string type; //Move type
        public string target; //Move target
        public int basePower; //Move base power

        //Move base accuracy.True denotes a move that always hits
        public int accuracy;
        public bool alwaysHits = false;

        public int critRatio; //Critical hit ratio. Defaults to 1.
        public bool willCrit; //Will this move always be a critical hit?
        public bool crit; //Is this move a critical hit?
  
        public bool ohko; //Can this move OHKO foes?

        /**
		 * Base move type. This is the move type as specified by the games,
		 * tracked because it often differs from the real move type.
		 */
        public string baseType;
        /**
		 * Secondary effects. An array because there can be more than one
		 * (for instance, Fire Fang has both a burn and a flinch
		 * secondary).
		 */
        public Globals.SecondaryEffect[] secondaries;
        /**
		 * Move priority. Higher priorities go before lower priorities,
		 * trumping the Speed stat.
		 */
        public int priority;
        /**
		 * Move category
		 * @type {'Physical' | 'Special' | 'Status'}
		 */
        public Globals.MoveCategory category;
        /**
		 * Category that changes which defense to use when calculating
		 * move damage.
		 * @type {'Physical' | 'Special' | 'Status' | undefined}
		 */
        public bool hasDefensiveCategory;
        public Globals.MoveCategory defensiveCategory;
        /**
		 * Whether or not this move uses the target's boosts
		 * @type {boolean}
		 */
        public bool useTargetOffensive;
        /**
		 * Whether or not this move uses the user's boosts
		 * @type {boolean}
		 */
        public bool useSourceDefensive;
        /**
		 * Whether or not this move ignores negative attack boosts
		 * @type {boolean}
		 */
        public bool ignoreNegativeOffensive;
        /**
		 * Whether or not this move ignores positive defense boosts
		 * @type {boolean}
		 */
        public bool ignorePositiveDefensive;
        /**
		 * Whether or not this move ignores attack boosts
		 * @type {boolean}
		 */
        public bool ignoreOffensive;
        /**
		 * Whether or not this move ignores defense boosts
		 * @type {boolean}
		 */
        public bool ignoreDefensive;
        /**
		 * Whether or not this move ignores type immunities. Defaults to
		 * true for Status moves and false for Physical/Special moves.
		 * @type {AnyObject | boolean}
		 * @readonly
		 */
        public IgnoreImmunityTypes ignoreImmunity;

        /**
		 * Base move PP.
		 * @type {number}
		 */
        public int pp;
        /**
		 * Whether or not this move can receive PP boosts.
		 * @type {boolean}
		 */
        public bool noPPBoosts;

        /**
		 * Is this move a Z-Move?
		 * @type {boolean | string | undefined}
		 */
        public bool isZ;
        /**
		 * Whether or not this move is a Z-Move that broke protect
		 * (affects damage calculation).
		 * @type {boolean}
		 */
        public bool zBrokeProtect;

        public Globals.MoveFlags flags;

        /**
		 * Whether or not the user must switch after using this move.
		 * @type {string | boolean}
		 */
        public bool selfSwitch;
        /**
		 * Move target only used by Pressure
		 * @type {string}
		 */
        public string pressureTarget;
        /**
		 * Move target used if the user is not a Ghost type
		 * @type {string}
		 */
        public string nonGhostTarget;
        /**
		 * Whether or not the move ignores abilities
		 * @type {boolean}
		 */
        public bool ignoreAbility;
        /**
		 * Move direct damage against the current target (dragon rage, seismic toss...)
		 * @type {string | number | boolean}
		 */
        public MoveDamageType moveDamageType;
        public int damage;
        /**
		 * Whether or not this move hit multiple targets
		 * @type {boolean}
		 */
        public bool spreadHit;
        /**
		 * Modifier that affects damage when multiple targets
		 * are hit
		 * @type {number | undefined}
		 */
        public int spreadModifier;
        /**
		 * Modifier that affects damage when this move is
		 * a critical hit
		 * @type {number | undefined}
		 */
        public int critModifier;
        /**
		 * Damage modifier based on the user's types
		 * @type {number}
		 */
        public int typeMod;
        /**
		 * Whether or not this move gets STAB
		 * @type {boolean}
		 */
        public bool hasSTAB;
        /**
		 * True if it can't be copied with Sketch
		 * @type {boolean}
		 */
        public bool noSketch;
        /**
		 * STAB (can be modified by other effects)
		 * @type {number | undefined}
		 */
        public int stab;

        /**
         * Some moves have damage callbacks, we will have a bunch of callbacks NOT YET IMPLEMENTED
         * "" if null
         */
        public delegate int DamageCallback(Pokemon pokemon, Pokemon target);
        public DamageCallback damageCallback; //= TemporaryCallbackStuff.CounterDamageCallback; <---Example

        public delegate int BasePowerCallback(Pokemon pokemon, Pokemon target, Move move);
        public BasePowerCallback basePowerCallback; //= TemporaryCallbackStuff.CounterDamageCallback; <---Example

        public override void Init(/*data, moreData = null, moreData2 = null*/)
        {
            base.Init(/*data, moreData = null*/);

            fullname = "move: " + name;
            effectType = Globals.EffectTypes.Move;
            critRatio = 1; //Number(this.critRatio) || 1;
            priority = 0; //Number(this.priority) || 0;
        }

    }

}
