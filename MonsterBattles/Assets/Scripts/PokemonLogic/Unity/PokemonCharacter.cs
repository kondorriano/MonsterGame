using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetableElement))]
[RequireComponent(typeof(CharacterController))]
public class PokemonCharacter : BattleElement {

    //Script that takes care of the movement and the input of the pokemon in the game
    //It contains the class pokemon, that will take care of all the inner calculations and manages all the data

    //Pokemon Data
    public Pokemon pokemonData;
    public TargetableElement targetScript;

    //Camera and CharacterController
    public Transform camTrans;
    CharacterController myChar;

    //Input
    float forwardInput, rightInput;
    public float joystickDetection = .1f;
    //Physics
    Vector3 velocity = Vector3.zero;
    public float forwardVel = 12;




    public void Init(Battle b, PokemonSet set, Battle.Team t)
    {
        pokemonData = new Pokemon(b, set, t, this);
        targetScript = GetComponent<TargetableElement>();
        targetScript.sourceElement = this;
        this.id = pokemonData.speciesId;
    }



    private void Start()
    {
        myChar = GetComponent<CharacterController>();
        camTrans = Camera.main.transform;
    }


    private void Update()
    {
        GetInput();
    }

    // Update is called once per frame
    void FixedUpdate () {
        Run();

        myChar.Move((velocity /*+ upVelocity*/) * Time.fixedDeltaTime);
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical1");
        rightInput = Input.GetAxis("Horizontal1");
    }

    void Run()
    {
        Vector3 frontDir = camTrans.forward;
        frontDir.y = 0;
        frontDir.Normalize();
        Vector3 dir = camTrans.right * rightInput + frontDir * forwardInput;
        if (dir.magnitude > joystickDetection)
        {
            transform.LookAt(transform.position + dir.normalized);
            velocity = transform.forward * forwardVel * dir.magnitude;
        }
        else velocity = Vector3.zero;
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
