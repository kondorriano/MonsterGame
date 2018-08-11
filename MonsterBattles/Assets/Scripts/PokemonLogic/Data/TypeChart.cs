using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeChart {

    //0 = normal
    //1 = effective
    //2 = not effective
    //3 = inmune
    public readonly static Dictionary<string, Globals.TypeData> BattleTypeChart = new Dictionary<string, Globals.TypeData>
    {
        {"Bug", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 2 }, {"Fire", 1 }, {"Flying", 1 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 2 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 1 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Dark", new Globals.TypeData(new Dictionary<string, int>
            {
                {"prankster", 3 },
                {"Bug", 1 }, {"Dark", 2 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 1 }, {"Fighting", 1 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 2 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 3 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Dragon", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 1 }, {"Electric", 2 }, {"Fairy", 1 }, {"Fighting", 0 }, {"Fire", 2 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 0 }, {"Ice", 1 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 2 }
            }
        )},
        {"Electric", new Globals.TypeData(new Dictionary<string, int>
            {
                {"par", 3 },
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 2 }, {"Fairy", 0 }, {"Fighting", 0 }, {"Fire", 0 }, {"Flying", 2 }, { "Ghost", 0 },
                { "Grass", 0 }, {"Ground", 1 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 2 }, {"Water", 0 }
            }
        )},
        {"Fairy", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 2 }, {"Dark", 2 }, {"Dragon", 3 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 2 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 1 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 1 }, {"Water", 0 }
            }
        )},
        {"Fighting", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 2 }, {"Dark", 2 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 1 }, {"Fighting", 0 }, {"Fire", 0 }, {"Flying", 1 }, { "Ghost", 0 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 1 }, {"Rock", 2 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Fire", new Globals.TypeData(new Dictionary<string, int>
            {
                {"brn", 3 },
                {"Bug", 2 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 2 }, {"Fighting", 0 }, {"Fire", 2 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 1 }, {"Ice", 2 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 1 }, {"Steel", 2 }, {"Water", 1 }
            }
        )},
        {"Flying", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 2 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 1 }, {"Fairy", 0 }, {"Fighting", 2 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 3 }, {"Ice", 1 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 1 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Ghost", new Globals.TypeData(new Dictionary<string, int>
            {
                {"trapped", 3 },
                {"Bug", 2 }, {"Dark", 1 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 3 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 1 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 0 }, {"Normal", 3 }, {"Poison", 2 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Grass", new Globals.TypeData(new Dictionary<string, int>
            {
                {"powder", 3 },
                {"Bug", 1 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 2 }, {"Fairy", 0 }, {"Fighting", 0 }, {"Fire", 1 }, {"Flying", 1 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 2 }, {"Ice", 1 }, {"Normal", 0 }, {"Poison", 1 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 2 }
            }
        )},
        {"Ground", new Globals.TypeData(new Dictionary<string, int>
            {
                {"sandstorm", 3 },
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 3 }, {"Fairy", 0 }, {"Fighting", 0 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 1 }, {"Ground", 0 }, {"Ice", 1 }, {"Normal", 0 }, {"Poison", 2 }, {"Psychic", 0 }, {"Rock", 2 }, {"Steel", 0 }, {"Water", 1 }
            }
        )},
        {"Ice", new Globals.TypeData(new Dictionary<string, int>
            {
                {"hail", 3 }, {"frz", 3},
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 1 }, {"Fire", 1 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 2 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 1 }, {"Steel", 1 }, {"Water", 0 }
            }
        )},
        {"Normal", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 1 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 3 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Poison", new Globals.TypeData(new Dictionary<string, int>
            {
                {"psn", 3 }, {"tox", 3},
                {"Bug", 2 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 2 }, {"Fighting", 2 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 1 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 2 }, {"Psychic", 1 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Psychic", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 1 }, {"Dark", 1 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 2 }, {"Fire", 0 }, {"Flying", 0 }, { "Ghost", 1 },
                { "Grass", 0 }, {"Ground", 0 }, {"Ice", 0 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 2 }, {"Rock", 0 }, {"Steel", 0 }, {"Water", 0 }
            }
        )},
        {"Rock", new Globals.TypeData(new Dictionary<string, int>
            {
                {"sandstorm", 3 },
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 0 }, {"Fairy", 0 }, {"Fighting", 1 }, {"Fire", 2 }, {"Flying", 2 }, { "Ghost", 0 },
                { "Grass", 1 }, {"Ground", 1 }, {"Ice", 0 }, {"Normal", 2 }, {"Poison", 2 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 1 }, {"Water", 1 }
            }
        )},
        {"Steel", new Globals.TypeData(new Dictionary<string, int>
            {
                {"psn", 3 }, {"tox", 3}, {"sandstorm", 3 },
                {"Bug", 2 }, {"Dark", 0 }, {"Dragon", 2 }, {"Electric", 0 }, {"Fairy", 2 }, {"Fighting", 1 }, {"Fire", 1 }, {"Flying", 2 }, { "Ghost", 0 },
                { "Grass", 2 }, {"Ground", 1 }, {"Ice", 2 }, {"Normal", 2 }, {"Poison", 3 }, {"Psychic", 2 }, {"Rock", 2 }, {"Steel", 2 }, {"Water", 0 }
            }
        )},
        {"", new Globals.TypeData(new Dictionary<string, int>
            {
                {"Bug", 0 }, {"Dark", 0 }, {"Dragon", 0 }, {"Electric", 1 }, {"Fairy", 0 }, {"Fighting", 0 }, {"Fire", 2 }, {"Flying", 0 }, { "Ghost", 0 },
                { "Grass", 1 }, {"Ground", 0 }, {"Ice", 2 }, {"Normal", 0 }, {"Poison", 0 }, {"Psychic", 0 }, {"Rock", 0 }, {"Steel", 2 }, {"Water", 2 }
            }
        )}
    };

    //Gravity, immunities are handled elsewhere
    public static int GetEffectiveness(Globals.Type source, Globals.Type target)
    {
        if (!BattleTypeChart.ContainsKey("" + target)) return 0;
        if (!BattleTypeChart["" + target].damageTaken.ContainsKey(""+source)) return 0;

        int typeData = BattleTypeChart["" + target].damageTaken["" + source];
        if (typeData == 1) return 1;
        if (typeData == 2) return -1;
        return 0;
    }

    public static bool HasImmunity(string source, Globals.Type[] target)
    {
        for(int i = 0; i < target.Length; ++i)
        {
            if (!BattleTypeChart.ContainsKey(""+target[i])) continue;
            if (!BattleTypeChart["" + target[i]].damageTaken.ContainsKey(source)) continue;
            if (BattleTypeChart["" + target[i]].damageTaken[source] == 3) return true;
        }
        return false;
    }
}
