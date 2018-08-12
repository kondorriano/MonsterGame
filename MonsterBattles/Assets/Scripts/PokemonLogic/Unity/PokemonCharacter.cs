using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PokemonCamera
{
    void ToggleTargeted();
    void SetCharacterHeight(float height);
    void SetCameraOffset(Vector2 axis);
    Transform GetTargetedPokemon();
}

[RequireComponent(typeof(TargetableElement))]
[RequireComponent(typeof(CharacterController))]
public class PokemonCharacter : BattleElement {

    //Script that takes care of the movement and the input of the pokemon in the game
    //It contains the class pokemon, that will take care of all the inner calculations and manages all the data

    //Pokemon Data
    public Pokemon pokemonData;
    public TargetableElement targetScript;
    public GameObject currentActiveMove;

    //Camera and CharacterController
    public Transform camTrans;
    public PokemonCamera camPokemon;
    CharacterController myChar;
    Animator myAnim;
    public int ControllerId = 1;

    public bool canMove = true;

    //Input
    float forwardInput, rightInput;
    float jumpPressed = 0.0f;
    public float joystickDetection = .1f;

    //Physics
    Vector3 velocity = Vector3.zero;
    Vector2 velPlane = Vector2.zero;
    float velVertical = 0;
    public float velMin = 4;
    public float velMax = 30;
    public const float velMonsterMax = 10000;
    public const float velGravityConst = 25.0f;

    public const float velJumpBase = 7;
    public const float jumpPressTime = 0.1f;

    public void Init(Battle b, PokemonSet set, Battle.Team t)
    {
        pokemonData = new Pokemon(b, set, t, this);
        targetScript = GetComponent<TargetableElement>();
        targetScript.sourceElement = this;
        this.id = pokemonData.speciesId;
        currentActiveMove = null;
    }

    private void Start()
    {
        myChar = GetComponent<CharacterController>();
        myAnim = transform.GetChild(0).GetComponent<Animator>();

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
        pokemonData.CoolDownManagement();
    }

    void FixedUpdate ()
    {
        if (canMove) Run();
        Animations();
        myChar.Move((velocity /*+ upVelocity*/) * Time.fixedDeltaTime);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10 * ControllerId, 100, 20), velPlane.magnitude.ToString());
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

        if (Input.GetButtonDown(string.Format("R{0}", ControllerId)))
        {
            jumpPressed = jumpPressTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int id = 0;
            for (id = 0; id < pokemonData.moveSlots.Length; id++)
            {
                if (pokemonData.moveSlots[id].id == "payday") break;
            }
            if (id < pokemonData.moveSlots.Length) RunAction(Globals.ActionType.Move, id);
        }
    }

    void Animations()
    {
        Vector3 velo = velocity;
        velo.y = 0;
        bool isRunning = velocity.magnitude > 0;
        myAnim.SetBool("isRunning", isRunning);
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

            if (myChar.isGrounded)
            {
                float speedFactor = (pokemonData.speed / velMonsterMax);
                speedFactor = 1.0f - speedFactor;
                speedFactor = 1.0f - (speedFactor * speedFactor);

                float currVel = velMin + speedFactor * velMax;

                if (jumpPressed > 0.0f)
                {
                    velPlane += dir * currVel * dir.magnitude * Time.fixedDeltaTime * 50;
                }
                else
                {
                    velPlane += dir * currVel * dir.magnitude * Time.fixedDeltaTime * 5;
                }

                if (velPlane.magnitude > currVel)
                {
                    velPlane = velPlane.normalized * currVel;
                }
            }
        }

        if (myChar.isGrounded && velPlane.magnitude != 0 && 
            (dir.magnitude <= joystickDetection || Vector2.Dot(velPlane, dir) < 0.2))
        {
            Vector2 velPlaneDir = velPlane.normalized * -1;
            velPlane += velPlaneDir * Time.fixedDeltaTime * 50;
            if (Vector2.Dot(velPlaneDir, velPlane) >= 0)
            {
                velPlane = Vector2.zero;
            }
        }

        if (myChar.isGrounded)
        {
            velVertical = 0;

            if (jumpPressed > 0.0f)
            {
                velVertical = velJumpBase;
                jumpPressed = 0.0f;
            }
        }
        else
        {
            velVertical -= velGravityConst * Time.fixedDeltaTime;
        }

        jumpPressed -= Time.fixedDeltaTime;
        velocity = new Vector3(velPlane.x, velVertical, velPlane.y);
    }

    //Check Input of actions here (move, megaevo, Switch pokemon, use item FROM BAG, ultraburst? zmove?)
    void RunAction(Globals.ActionType action, int moveSlotId, bool zMove = false)
    {
        //Here start cooldown if needed



        //Move
        if (action == Globals.ActionType.Move)
        {
            //set cooldown
            pokemonData.StartActionCoolDown();
            Transform myTarget = camPokemon.GetTargetedPokemon();
            Pokemon.TargetLocation targetto;
            if (myTarget == null)
            {
                targetto = new Pokemon.TargetLocation(transform.forward);
            } else
            {
                targetto = new Pokemon.TargetLocation(Vector3.zero, myTarget);

            }

            pokemonData.RunMove(moveSlotId, 0, zMove, false, targetto);
        }


        //Switch
        else if(action == Globals.ActionType.Switch)
        {
            //set cooldown
            pokemonData.StartActionCoolDown();
            //SwitchIn(Pokemon)
        }


        //MegaEvolution or Ultra burst
        else if (action == Globals.ActionType.MegaEvolution || action == Globals.ActionType.UltraBurst)
        {
            //RunMegaEvo
            //Callback.OnSetCooldown
        }
    }

    public void SetActiveMove(GameObject move = null)
    {
        this.currentActiveMove = move;
    }
}
