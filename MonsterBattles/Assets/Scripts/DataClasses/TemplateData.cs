using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateData
{
    /*Required*/
    //CHANGE ABILITIES TO ARRAY??
    public string ability0;

    public Globals.StatsTable baseStats;
    public string color;
    public Globals.EggGroups eggGroups;
    public float heightm;
    public int num;
    public string species;
    public Globals.Type[] types;
    public float weightkg;
    /*Optional*/
    public string ability1;
    public string abilityH;
    public string abilityS;

    public string baseForme;
    public string baseSpecies;
    public int evoLevel;
    public string evoMove;
    public string[] evos;
    public string forme;
    public string formeLetter;
    Globals.GenderName gender;
    public float maleGenderRatio;
    public float femaleGenderRatio;
    public int maxHP;
    public string[] otherForms;
    public string[] otherFormes;
    public string prevo;
    public bool isMega;
    //To mega evolve and others?
    public string requiredMove; //Dragon ascend mega rayquaza //HAS TO BE AN ID
    public string requiredItem; //HAS TO BE AN ID
    public bool isUltra;



    public TemplateData(
        /*Required*/ string ability0, Globals.StatsTable baseStats, string color, Globals.EggGroups eggGroups, float heightm, int num, string species, Globals.Type[] types, float weightkg,
        /*Optional*/ string ability1 = "", string abilityH = "", string abilityS = "", string baseForme = "", string baseSpecies = "", int evoLevel = -1, string evoMove = "", string[] evos = null, string forme = "",
        /*Optional*/ string formeLetter = "", Globals.GenderName gender = Globals.GenderName.MorF, float maleGenderRatio = 0, float femaleGenderRatio = 0, int maxHP = -1, string[] otherForms = null, string[] otherFormes = null,
        /*Optional*/ string prevo = "", bool isMega = false, string requiredMove = "", string requiredItem = "", bool isUltra = false
        )
    {
        /*Required*/
        this.ability0 = ability0;

        this.baseStats = baseStats;
        this.color = color;
        this.eggGroups = eggGroups;
        this.heightm = heightm;
        this.num = num;
        this.species = species;
        this.types = types;
        this.weightkg = weightkg;

        /*Optional*/
        this.ability1 = ability1;
        this.abilityH = abilityH;
        this.abilityS = abilityS;

        this.baseForme = baseForme;
        this.baseSpecies = (baseSpecies == "") ? species : baseSpecies;
        this.evoLevel = evoLevel;
        this.evoMove = evoMove;
        this.evos = evos;
        this.forme = forme;
        this.formeLetter = formeLetter;
        this.gender = gender;
        this.maleGenderRatio = maleGenderRatio;
        this.femaleGenderRatio = femaleGenderRatio;
        this.maxHP = maxHP;
        this.otherForms = otherForms;
        this.otherFormes = otherFormes;
        this.prevo = prevo;
        this.isMega = isMega;
        this.requiredMove = requiredMove;
        this.requiredItem = requiredItem;
        this.isUltra = isUltra;
        //logged until here

    }

    public void LogTemplateData(string id)
    {
        string logTypes = "";
        for (int i = 0; i < types.Length; ++i)
        {
            logTypes += " " + types[i];
        }

        string logAbilities = "Its abilities are: " + ability0 + " as first" + ((ability1 != "") ? ", " + ability1 + " as second" : "") + ((abilityH != "") ? ", " + abilityH + " as hidden" : "") + ((abilityS != "") ? ", " + abilityS + " as secret" : "") + "\n";
        string logBaseStats = "Its base stats are: Hp " + baseStats.hp + ", Atk " + baseStats.atk + ", Def " + baseStats.def + ", SpA " + baseStats.spa + ", SpD " + baseStats.spd + ", Speed " + baseStats.spe + "\n";

        string logEggGroups = "Its egg groups are:";
        for (int i = 0; (1 << i) < (int)Globals.EggGroups.Length; ++i)
        {
            if ((eggGroups & ((Globals.EggGroups)(1 << i))) == 0) continue;

            logEggGroups += " " + (Globals.EggGroups)(1 << i);
        }
        logEggGroups += "\n";

        string logEvos = "Preevolution: " + ((prevo != "") ? baseForme : "none") + ", Evolutions: ";
        if (evos == null) logEvos += "none";
        else
        {
            for (int i = 0; i < evos.Length; ++i)
            {
                logEvos += evos[i] + " ";
            }
        }
        logEvos += "\n";
        string logForms = "Other Forms: ";
        if (otherForms == null) logForms += "none ";
        else
        {
            for (int i = 0; i < otherForms.Length; ++i)
            {
                logForms += otherForms[i] + " ";
            }
        }
        logForms += " Other Special Forms: ";
        if (otherFormes == null) logForms += "none ";
        else
        {
            for (int i = 0; i < otherFormes.Length; ++i)
            {
                logForms += otherFormes[i] + " ";
            }
        }
        logForms += "\n";

        Debug.Log("Pokemon #" + num + ": " + species + ", type/s:" + logTypes + ", with id " + id + "\n" +
            logAbilities +
            logBaseStats +
            logEggGroups +
            logEvos +
             "Color " + color + ", Height " + heightm + ", Weight " + weightkg + "\n" +
             "BaseForme " + ((baseForme != "") ? baseForme : "none") + ", BaseSpecies " + ((baseSpecies != "") ? baseSpecies : "none") + ", EvoLevel " + ((evoLevel != -1) ? "" + evoLevel : "none") + ", EvoMove " + ((evoMove != "") ? evoMove : "none") + "\n" +
             "Gender " + gender + ", GenderRatio {M: " + maleGenderRatio + ", F: " + femaleGenderRatio + "} Forme " + ((forme != "") ? forme : "none") + " FormeLetter " + ((formeLetter != "") ? formeLetter : "none") + " MaxHP " + ((maxHP != -1) ? "" + maxHP : "none") + "\n" +
             logForms +
             "Is " + ((isMega) ? "" : "not") + " a mega evolution. Is " + ((isUltra) ? "" : "not") + " an ultra burst form. Needs: " + ((requiredMove != "") ? requiredMove : "no move") + " and " + ((requiredItem != "") ? requiredItem : "no item") + " to mega evolve or change forms \n"
            );
    }
}
