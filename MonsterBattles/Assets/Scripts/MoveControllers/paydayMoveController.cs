using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paydayMoveController : MonoBehaviour {
    private ActiveMove am;
    public GameObject projectilePrefab;
    public float windUpTime = 0.2f;
    public float recoveryTime = 0.4f;

    private int state = 0;
    private float timer = 0f;
    private List<GameObject> projectiles;

	// Use this for initialization
	void Start () {
        projectiles = new List<GameObject>();
        am = GetComponent<ActiveMove>();
        am.source.canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        
        if (state == 0)
        {
            if (timer >= windUpTime)
            {
                Transform T = am.source.transform;
                state++;
                GameObject proj = GameObject.Instantiate(projectilePrefab, T.position, T.rotation);
                Physics.IgnoreCollision(proj.GetComponent<Collider>(), am.source.GetComponent<CharacterController>());
                LineProjectile lp = proj.GetComponent<LineProjectile>();

                lp.direction = am.targetLocation.direction;
                projectiles.Add(proj);
            }
        }
        if (state == 1)
        {
            if (timer >= recoveryTime)
            {
                state += 1;
                am.source.canMove = true;
            }
        }
        if (state == 2)
        {
            if (projectiles.Count == 0)
            {
                Destroy(this);
            }
        }
	}

    public void ImpactedWith(BattleElement be)
    {
        Debug.Log("Collided with " + be.gameObject.name);
    }

    public void ProjectileDied(GameObject go)
    {
        projectiles.Remove(go);
        Destroy(go);
    }
}
