using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineProjectile : MonoBehaviour {
    public Transform father;

    public float speed = 5f;
    public float turnSpeed = 720f;
    public Vector3 direction = Vector3.zero;
    public Vector3 spinAxis = Vector3.up;
    public bool isSpinning = true;
    public float timeToLive = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            father.GetComponent<paydayMoveController>().ProjectileDied(gameObject);
            Destroy(this);
        }

        transform.RotateAroundLocal(spinAxis, turnSpeed * Time.fixedDeltaTime);
        transform.position += direction * speed * Time.fixedDeltaTime;
	}

    void OnCollisionEnter(Collision collision)
    {
        BattleElement be = collision.gameObject.GetComponent<BattleElement>();
        if (be != null) father.GetComponent<paydayMoveController>().ImpactedWith(be);
    }
}
