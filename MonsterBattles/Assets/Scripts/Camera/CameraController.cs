using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform character;
    public Transform target;
    public bool targeted = false;

    public float distance = 5f;
    public float heightOverCharacter = 2f;
    public float lookOverCharacter = 1.5f;
    public float lookSmooth = 1f;
    public float followSmooth = 1f;
    public float priorityTarget = 0.75f;

    private Vector3 lookPos;
    private Vector3 dir;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetKeyDown(KeyCode.Space)) targeted = !targeted;

        if (!targeted)
        {
            lookPos = character.position + Vector3.up * lookOverCharacter;
            //dir = transform.position - lookPos;
        }
        else
        {
            lookPos = character.position + (target.position - character.position) * .75f + Vector3.up * lookOverCharacter;
            //Vector3 dir2 = transform.position - lookPos;
            //dir2.y = 0;
            //dir2.Normalize();
            //dir = dir * .25f + dir2 * .75f;
        }

        dir = transform.position - lookPos;
        dir.y = 0;
        dir.Normalize();
        Vector3 desiredPosition = character.position + dir * distance + Vector3.up * heightOverCharacter;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmooth * Time.deltaTime);

        Quaternion desiredRotation = Quaternion.LookRotation(lookPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, lookSmooth * Time.deltaTime);
	}
}
