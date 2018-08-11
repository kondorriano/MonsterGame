using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PokemonCamera
{
    void ToggleTargeted();
    void SetCharacterHeight(float height);
}

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
    public PokemonCamera camPokemon;
    CharacterController myChar;
    public int ControllerId = 1;
    

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

        CapsuleCollider myCol = GetComponent<CapsuleCollider>();
        if (myCol)
        {
            myChar.radius = myCol.radius;
            myChar.height = myCol.height;
            myChar.center = myCol.center;

            Destroy(myCol);
        }

        if (camPokemon != null)
        {
            camPokemon.SetCharacterHeight(Mathf.Max(myChar.radius * 2, myChar.height));
        }

        if (camTrans == null)
        {
            camTrans = Camera.main.transform;
        }
    }

    private void Update()
    {
        GetInput();
    }

    void FixedUpdate ()
    {
        Run();

        myChar.Move((velocity /*+ upVelocity*/) * Time.fixedDeltaTime);
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis(string.Format("Vertical{0}", ControllerId));
        rightInput = Input.GetAxis(string.Format("Horizontal{0}", ControllerId));

        if(Input.GetButtonDown(string.Format("L{0}", ControllerId)))
        {
            camPokemon.ToggleTargeted();
        }
    }

    void Run()
    {
        Vector3 frontDir = camTrans.forward;
        frontDir.y = 0;
        frontDir.Normalize();
        Vector3 dir = camTrans.right * rightInput + frontDir * forwardInput;
        if (dir.magnitude > joystickDetection)
        {
            //transform.LookAt(transform.position + dir.normalized);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(dir, Vector3.up), 0.2f);
            velocity = dir * forwardVel * dir.magnitude;
            velocity.y = 0;
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
