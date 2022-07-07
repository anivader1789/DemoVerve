using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuitController : MonoBehaviour
{

    public  Vector3 Pv_thrust = new Vector3(0, 0, 0);
    private Vector3 pv_gravity = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 pv_velocity = new Vector3(0, 0, 0);

    private List<HeavenlyBody> pl_gravityBodies = new List<HeavenlyBody>();

    private float pf_gravity = 0;
    private float pf_distance = 0;

    public Camera Po_playerCamera;
    public float Pf_lookSpeed = 2.0f;
    public float Pf_lookXLimit = 45.0f;

    Vector3 pv_moveDirection = Vector3.zero;
    float pf_rotationX = 0;

    private Game po_game;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        po_game = GameObject.Find("Global").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        

        pf_rotationX += -Input.GetAxis("Mouse Y") * Pf_lookSpeed;
        pf_rotationX = Mathf.Clamp(pf_rotationX, -Pf_lookXLimit, Pf_lookXLimit);
        Po_playerCamera.transform.localRotation = Quaternion.Euler(pf_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Pf_lookSpeed, 0);
    }

    void FixedUpdate()
    {
        //Calculate gravity
        foreach(HeavenlyBody body in pl_gravityBodies)
        {
            pf_distance = Vector3.Distance(body.pos, transform.position);
            pf_gravity = body.gravityForce / (pf_distance * pf_distance);
            pv_gravity = pf_gravity * Vector3.Normalize(body.pos - transform.position);
            pv_velocity += pv_gravity;
        }

        //Add thrust from controller
        pv_velocity += Pv_thrust;

        // Move the controller
        transform.Translate(pv_velocity * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering "+other.gameObject.name);
        HeavenlyBody hb = other.gameObject.GetComponent<HeavenlyBody>();
        pl_gravityBodies.Add(hb);
        po_game.Po_currentOrbitBody = hb;
    }

    void OnTriggerExit(Collider other)
    {
        foreach(HeavenlyBody body in pl_gravityBodies)
        {
            if(other.gameObject.name.Equals(body.bodyName))
            {
                pl_gravityBodies.Remove(body);
                Debug.Log("Leaving " + body.bodyName);
                po_game.Po_currentOrbitBody = null;
            }
        }

        if(pl_gravityBodies.Count > 0)
        {
            po_game.Po_currentOrbitBody = pl_gravityBodies[0];
        }
    }
}
