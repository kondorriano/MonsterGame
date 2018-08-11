using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {
    public float speed = 10f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        transform.Translate(forward * Input.GetAxis("Vertical") * speed * Time.deltaTime + Camera.main.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
    }
}
