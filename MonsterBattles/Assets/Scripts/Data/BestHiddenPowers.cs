using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestHiddenPowers {

    public static Dictionary<string, Globals.StatsTable> HiddenPowers = new Dictionary<string, Globals.StatsTable>
    {
        {"Bug",         new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 30, spa: 31, spd: 30)},
        {"Dark",        new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 31, spa: 31, spd: 31)},
        {"Dragon",      new Globals.StatsTable(hp: 30, atk: 31, def: 31, spe: 31, spa: 31, spd: 31)},
        {"Electric",    new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 31, spa: 30, spd: 31)},
        {"Fighting",    new Globals.StatsTable(hp: 31, atk: 31, def: 30, spe: 30, spa: 30, spd: 30)},
        {"Fire",        new Globals.StatsTable(hp: 31, atk: 30, def: 31, spe: 30, spa: 30, spd: 31)},
        {"Flying",      new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 30, spa: 30, spd: 30)},
        {"Ghost",       new Globals.StatsTable(hp: 31, atk: 30, def: 31, spe: 31, spa: 31, spd: 30)},
        {"Grass",       new Globals.StatsTable(hp: 30, atk: 31, def: 31, spe: 31, spa: 30, spd: 31)},
        {"Ground",      new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 31, spa: 30, spd: 30)},
        {"Ice",         new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 30, spa: 31, spd: 31)},
        {"Poison",      new Globals.StatsTable(hp: 31, atk: 31, def: 30, spe: 31, spa: 30, spd: 30)},
        {"Psychic",     new Globals.StatsTable(hp: 30, atk: 31, def: 31, spe: 30, spa: 31, spd: 31)},
        {"Rock",        new Globals.StatsTable(hp: 31, atk: 31, def: 30, spe: 30, spa: 31, spd: 30)},
        {"Steel",       new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 31, spa: 31, spd: 30)},
        {"Water",       new Globals.StatsTable(hp: 31, atk: 31, def: 31, spe: 30, spa: 30, spd: 31)}
    };

}
