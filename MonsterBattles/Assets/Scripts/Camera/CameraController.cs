﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, PokemonCamera
{
    public Transform character;
    public Transform target;
    public bool targeted = false;

    public float distance = 5f;
    public float heightOverCharacter = 2f;
    public float lookOverCharacter = 1.5f;
    public float lookSmooth = 10f;
    public float followSmooth = 8f;
    public float priorityTarget = 0.75f;

    private Vector3 lookPos;
    private Vector3 dir;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (Input.GetKeyDown(KeyCode.Space)) targeted = !targeted;

        if (!targeted)
        {
            lookPos = character.position + Vector3.up * lookOverCharacter;
            //dir = transform.position - lookPos;
        }
        else
        {
            lookPos = character.position + (target.position - character.position) * priorityTarget + Vector3.up * lookOverCharacter;
            //Vector3 dir2 = transform.position - lookPos;
            //dir2.y = 0;
            //dir2.Normalize();
            //dir = dir * .25f + dir2 * .75f;
        }

        dir = transform.position - lookPos;
        dir.y = 0;
        dir.Normalize();
        Vector3 desiredPosition = character.position + dir * distance + Vector3.up * heightOverCharacter;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmooth * Time.fixedDeltaTime);

        Quaternion desiredRotation = Quaternion.LookRotation(lookPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, lookSmooth * Time.fixedDeltaTime);
	}

    public void ToggleTargeted()
    {
        targeted = !targeted;
    }
}
