using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetableElement))]
public class PokemonCharacter : BattleElement {

    //Script that takes care of the movement and the input of the pokemon in the game
    //It contains the class pokemon, that will take care of all the inner calculations and manages all the data

    public Pokemon pokemonData;
    public TargetableElement targetScript;


    public void Init(Battle b, PokemonSet set, Battle.Team t)
    {

        pokemonData = new Pokemon(b, set, t, this);
        targetScript = GetComponent<TargetableElement>();
        targetScript.sourceElement = this;
    }

    private void Start()
    {

    }




    // Update is called once per frame
    void Update () {

	}




    //Check Input of actions here (move, megaevo, Switch pokemon, use item FROM BAG, ultraburst? zmove?)
    void RunAction()
    {
        //Here start cooldown if needed

        //if do a move
        //set cooldown
        //RunMove(Move)

        //if do a switch
        //set cooldown
        //SwitchIn(Pokemon)

        //if mega evolve or ultra burst
        //RunMegaEvo
        //Callback.OnSetCooldown
    }

   
}
