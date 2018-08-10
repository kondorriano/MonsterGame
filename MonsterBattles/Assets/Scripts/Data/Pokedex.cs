using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokedex {

    public static Dictionary<string, TemplateData> BattlePokedex = new Dictionary<string, TemplateData>
    {
        {"bulbasaur", new TemplateData(
            num: 1,
            species: "Bulbasaur",
            types: new Globals.Type[] {Globals.Type.Grass, Globals.Type.Poison},
            maleGenderRatio: 0.875f,
            femaleGenderRatio: 0.125f,
            baseStats: new Globals.StatsTable(hp: 45, atk: 49, def: 49, spa: 65, spd: 65, spe: 45),
            ability0: "Overgrow",
            abilityH: "Chlorophyll",
            heightm: 0.7f,
            weightkg: 6.9f,
            color: "Green",
            evos: new string[]{"ivysaur"},
            eggGroups: Globals.EggGroups.Monster | Globals.EggGroups.Grass
        )},
        {"rayquaza", new TemplateData(
            num: 384,
            species: "Rayquaza",
            types: new Globals.Type[] {Globals.Type.Dragon, Globals.Type.Flying },
            gender: Globals.GenderName.N,
            baseStats: new Globals.StatsTable(hp: 105, atk: 150, def: 90, spa: 150, spd: 90, spe: 95),
            ability0: "Air Lock",
            heightm: 7,
            weightkg: 206.5f,
            color: "Green",
            eggGroups: Globals.EggGroups.Undiscovered,
            otherFormes: new string[] {"rayquazamega" }
        )},
        {"rayquazamega", new TemplateData(
            num: 384,
            species: "Rayquaza-Mega",
            baseSpecies: "Rayquaza",
            forme: "Mega",
            formeLetter: "M",
            types: new Globals.Type[] {Globals.Type.Dragon, Globals.Type.Flying },
            gender: Globals.GenderName.N,
            baseStats: new Globals.StatsTable(hp: 105, atk: 180, def: 100, spa: 180, spd: 100, spe: 115),
            ability0: "Delta Stream",
            heightm: 10.8f,
            weightkg: 392,
            color: "Green",
            eggGroups: Globals.EggGroups.Undiscovered,
            requiredMove: "dragonascent",
            isMega: true
        )},
        {"necrozma", new TemplateData(
            num: 800,
            species: "Necrozma",
            types: new Globals.Type[] {Globals.Type.Psychic },
            gender: Globals.GenderName.N,
            baseStats: new Globals.StatsTable(hp: 97, atk: 107, def: 101, spa: 127, spd: 89, spe: 79),
            ability0: "Prism Armor",
            heightm: 2.4f,
            weightkg: 230,
            color: "Black",
            eggGroups: Globals.EggGroups.Undiscovered,
            otherFormes: new string[]{"necrozmaduskmane", "necrozmadawnwings", "necrozmaultra"}
        )},
        {"necrozmadawnwings", new TemplateData(
            num: 800,
            species: "Necrozma-Dawn-Wings",
            baseSpecies: "Necrozma",
            forme: "Dawn-Wings",
            formeLetter: "D",
            types: new Globals.Type[] {Globals.Type.Psychic, Globals.Type.Ghost },
            gender: Globals.GenderName.N,
            baseStats: new Globals.StatsTable(hp: 97, atk: 113, def: 109, spa: 157, spd: 127, spe: 77),
            ability0: "Prism Armor",
            heightm: 4.2f,
            weightkg: 350,
            color: "Blue",
            eggGroups: Globals.EggGroups.Undiscovered
        )},
        {"necrozmaultra", new TemplateData(
            num: 800,
            species: "Necrozma-Ultra",
            baseSpecies: "Necrozma",
            forme: "Ultra",
            formeLetter: "U",
            types: new Globals.Type[] {Globals.Type.Psychic, Globals.Type.Dragon},
            gender: Globals.GenderName.N,
            baseStats: new Globals.StatsTable(hp: 97, atk: 167, def: 97, spa: 167, spd: 97, spe: 129),
            ability0: "Neuroforce",
            heightm: 7.5f,
            weightkg: 230,
            color: "Blue",
            eggGroups: Globals.EggGroups.Undiscovered,
            requiredItem: "ultranecroziumz",
            isUltra: true
        )}
    };

}
