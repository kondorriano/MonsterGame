using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSet {

    public string speciesId;
    public string name;
    public int level;
    public Globals.GenderName gender;
    public int happiness;
    public string pokeball;
    public bool shiny;
    public string abilityId;
    public string itemId;
    public string[] movesId;
    public Globals.StatsTable evs;
    public Globals.StatsTable ivs;
    public Globals.Nature nature;
    public int[] ppBoosts; //0, 1, 2 or 3

    public PokemonSet(string speciesId = "", string name = "", int level = 100, Globals.GenderName gender = Globals.GenderName.F, int happiness = 0, string pokeball = "", bool shiny = false, string abilityId = "", string itemId = "",  string[] movesId = null, Globals.StatsTable evs = null, Globals.StatsTable ivs = null, Globals.Nature nature = null, int[] ppBoosts = null)
    {
        this.speciesId = speciesId;
        this.name = name;
        this.level = level;
        this.gender = gender;
        this.happiness = happiness;
        this.pokeball = pokeball;
        this.shiny = shiny;
        this.abilityId = abilityId;
        this.itemId = itemId;
        this.movesId = movesId;
        if(evs == null)
        {
            evs = new Globals.StatsTable();
        }
        this.evs = evs;
        if(ivs == null)
        {
            ivs = new Globals.StatsTable();
            ivs.StatsTableIvs();
        }
        this.ivs = ivs;
        if(nature == null)
        {
            nature = new Globals.Nature(name: "basic");
        }
        this.nature = nature;

        if (movesId != null && (ppBoosts == null || ppBoosts.Length < movesId.Length))
        {
            this.ppBoosts = new int[movesId.Length];
            for (int i = 0; i < movesId.Length; ++i)
            {
                if (ppBoosts == null || ppBoosts.Length <= i) this.ppBoosts[i] = 0;
                else this.ppBoosts[i] = ppBoosts[i];
            }
        } else this.ppBoosts = ppBoosts;
    }
}
