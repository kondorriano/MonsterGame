using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PokemonCamera
{
    void ToggleTargeted();
    void SetCharacterHeight(float height);
    void SetCameraOffset(Vector2 axis);
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
    Vector2 velPlane = Vector2.zero;
    float velGravity = 0;
    public float velMin = 4;
    public float velMax = 30;
    public const float velMonsterMax = 10000;
    public const float velGravityConst = 9.0f;

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

        float axisX = Input.GetAxis(string.Format("RHorizontal{0}", ControllerId));
        float axisY = Input.GetAxis(string.Format("RVertical{0}", ControllerId));
        Vector2 camAxis = new Vector2(axisX, axisY);

        camPokemon.SetCameraOffset(camAxis);

        if (Input.GetButtonDown(string.Format("L{0}", ControllerId)))
        {
            camPokemon.ToggleTargeted();
        }
    }

    void Run()
    {
        Vector2 frontDir = new Vector2(camTrans.forward.x, camTrans.forward.z);
        frontDir.Normalize();

        Vector2 rightDir = new Vector2(camTrans.right.x, camTrans.right.z);
        rightDir.Normalize();

        Vector2 dir = rightDir * rightInput + frontDir * forwardInput;

        if (dir.magnitude > 1)
            dir.Normalize();

        if (dir.magnitude > joystickDetection)
        {
            //transform.LookAt(transform.position + dir.normalized);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y), Vector3.up), 0.15f);

            float speedFactor = (pokemonData.speed / velMonsterMax);
            speedFactor = 1.0f - speedFactor;
            speedFactor = 1.0f - (speedFactor * speedFactor);

            float currVel = velMin + speedFactor * velMax;

            velPlane += dir * currVel * dir.magnitude * Time.fixedDeltaTime * 5;
            
            if (velPlane.magnitude > currVel)
            {
                velPlane = velPlane.normalized * currVel;
            }
        }
        else
        {
            if (myChar.isGrounded)
            {
                velPlane = velPlane * 0.8f;
                if (velPlane.magnitude < 0.1f)
                {
                    velPlane = Vector2.zero;
                }
            }
        }

        if (myChar.isGrounded)
        {
            velGravity = 0;
        }
        else
        {
            velGravity -= velGravityConst;
        }

        velocity = new Vector3(velPlane.x, velGravity, velPlane.y);
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
