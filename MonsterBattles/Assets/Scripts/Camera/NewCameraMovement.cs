using UnityEngine;
using System.Collections;

public class NewCameraMovement : MonoBehaviour
{
    public Transform target;

    

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 2f, 0); //offset al que mira la camara (Player pos mas offset)
        public float distanceFromTarget = -4;
        public float lookSmooth = 100;
        public bool smoothFollow = true;
        public float smooth = .05f;

        [HideInInspector]
        public float newDistance = -4; //set by zoom input
        [HideInInspector]
        public float adjustmentDistance = -4;
    }

    [System.Serializable]
    public class OtherPlayerSettings
    {
        public Transform otherTarget;
        public Vector3 otherPosOffset = new Vector3(0, 2f, 0); //offset al que mira la camara (other pos mas offset)
        [HideInInspector]
        public bool lookOther = false;
        public float lostTime = 0.3f;
        [HideInInspector]
        public float lookCounter = 0;

    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "R3";
        public string ORBIT_HORIZONTAL = "HorRight";
        public string ORBIT_VERTICAL = "VerRight";
    }

    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
        public bool drawOtherCollisionLines = true;
    }

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();
    public OtherPlayerSettings other = new OtherPlayerSettings();
    public DebugSettings debug = new DebugSettings();
    public CollisionHandler collision = new CollisionHandler();

    Vector3 lookPos = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 adjustedDirection = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    float vOrbitInput, hOrbitInput, hOrbitSnapInput;

    void Start()
    {
        vOrbitInput = hOrbitInput = hOrbitSnapInput = 0;
        MoveToTarget();

        collision.Initialize(Camera.main);
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(direction, transform.rotation, ref collision.desiredCameraClipPoints);
    }

    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        if (Input.GetButtonDown("Fire3"))
        {
            if (!other.lookOther && !collision.CollisionDetectedAtAllClipPoints(collision.adjustedCameraClipPoints,
                other.otherTarget.position + other.otherPosOffset)) other.lookOther = true;
            else other.lookOther = false;
        }
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
        //player input orbit
        OrbitTarget();

        
        
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(direction, transform.rotation, ref collision.desiredCameraClipPoints);
        collision.CheckColliding(lookPos);//using raycasts here
        position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(lookPos);

        if (other.lookOther)
        {
            if (collision.CollisionDetectedAtAllClipPoints(collision.adjustedCameraClipPoints,
                other.otherTarget.position + other.otherPosOffset)) other.lookCounter += Time.deltaTime;
            else other.lookCounter = 0;

            if(other.lookCounter >= other.lostTime) other.lookOther = false;
        }

        //draw debug line
        for (int i = 0; i < 5; i++)
        {
            if (debug.drawDesiredCollisionLines)
            {
                Debug.DrawLine(lookPos, collision.desiredCameraClipPoints[i], Color.blue);
            }

            if (debug.drawAdjustedCollisionLines)
            {
                Debug.DrawLine(lookPos, collision.adjustedCameraClipPoints[i], Color.green);
            }

            if (debug.drawOtherCollisionLines)
            {
                Debug.DrawLine(other.otherTarget.position + other.otherPosOffset, collision.desiredCameraClipPoints[i], Color.red);
            }
        }
    }

    void MoveToTarget()
    {
        lookPos = target.position + position.targetPosOffset;//+ Vector3.up * position.targetPosOffset.y + Vector3.forward * position.targetPosOffset.z + transform.TransformDirection(Vector3.right * position.targetPosOffset.x);
        Vector3 dir = transform.position-lookPos;
        dir.y = 0;
        dir.Normalize();
        if(other.lookOther)
        {
            Vector3 lookPos2 = target.position + (other.otherTarget.position - target.position) * .5f + position.targetPosOffset;
            Vector3 dir2 = transform.position - lookPos2;
            dir2.y = 0;
            dir2.Normalize();
            dir = dir * .25f + dir2 * .75f;
        }
        direction = -dir * position.distanceFromTarget;// Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -dir * position.distanceFromTarget;
        direction += lookPos;

        if (collision.colliding)
        {
            adjustedDirection = dir * position.adjustmentDistance;//;Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * dir * position.adjustmentDistance;
            adjustedDirection += lookPos;
            if (position.smoothFollow)
            {
                //use smooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDirection, ref camVel, position.smooth);
            }
            else transform.position = adjustedDirection;
        }
        else
        {
            if (position.smoothFollow)
            {
                //use smooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, direction, ref camVel, position.smooth);
            }
            else transform.position = direction;
        }
        
    }

    void LookAtTarget()
    {
        if (!other.lookOther)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookPos - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
        } else
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position+(other.otherTarget.position-target.position)*.2f+position.targetPosOffset - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
        }
    }

    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = -180;
        }

        orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

        orbit.xRotation = Mathf.Clamp(orbit.xRotation, orbit.minXRotation, orbit.maxXRotation);
    }

    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;
        public float collisionSizeSpace = 3.41f;

        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;
        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 camPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera) return;
            //clear contents of intoarray
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / collisionSizeSpace) * z;
            float y = x / camera.aspect;

            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + camPosition; //added and rotated the point relative to camera
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + camPosition;
            //bot left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + camPosition;
            //bot right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + camPosition;
            //cam pos
            intoArray[4] = camPosition - camera.transform.forward;
        }

        public bool CollisionDetectedAtAllClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if (!Physics.Raycast(ray, distance, collisionLayer))
                {
                    return false;
                }
            }

            return true;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if (Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }

            return false;
        }

        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;
            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (distance == -1) distance = hit.distance;
                    else if (hit.distance < distance) distance = hit.distance;
                }
            }

            if (distance == -1) return 0;
            else return distance;
        }

        public void CheckColliding(Vector3 targetPosition)
        {
            colliding = CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition);
        }

    }
}
