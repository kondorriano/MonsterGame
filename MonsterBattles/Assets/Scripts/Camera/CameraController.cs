using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, PokemonCamera
{
    public Transform character;
    public Transform target;
    public bool targeted = false;

    public float distance = 5f;
    public float heightOverCharacter = 1.5f;
    public float lookOverCharacter = 1f;
    public float lookSmooth = 10f;
    public float followSmooth = 8f;
    public float priorityTarget = 0.75f;

    public float resizeFactor = 0.5f;
    public float lookOffsetFactor = .15f;

    private Vector2 lookOffset = Vector2.zero;
    private float charFactor = 1f;
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
            lookPos = character.position + Vector3.up * lookOverCharacter * charFactor;
        }
        else
        {
            lookPos = character.position + (target.position - character.position) * priorityTarget + Vector3.up * lookOverCharacter * charFactor;
        }
        lookPos += (transform.up * lookOffset.y * 0.75f + transform.right * lookOffset.x) * lookOffsetFactor * (lookPos - transform.position).magnitude * charFactor;

        dir = transform.position - lookPos;
        dir.y = 0;
        dir.Normalize();
        Vector3 desiredPosition = character.position + dir * distance * charFactor + Vector3.up * heightOverCharacter * charFactor;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmooth / charFactor * Time.fixedDeltaTime);

        Quaternion desiredRotation = Quaternion.LookRotation(lookPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, lookSmooth / charFactor * Time.fixedDeltaTime);
	}

    public void ToggleTargeted()
    {
        targeted = !targeted;
    }

    public void SetCharacterHeight(float height)
    {
        charFactor = height * resizeFactor;
    }

    public void SetCameraOffset(Vector2 offset)
    {
        lookOffset = offset;
    }
}
