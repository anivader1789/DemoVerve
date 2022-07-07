using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuitController : MonoBehaviour
{

    public  Vector3 Pv_thrust = new Vector3(0, 0, 0);
    private Vector3 pv_gravity = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 pv_velocity = new Vector3(0, 0, 0);

    private List<HeavenlyBody> gravityBodies = new List<HeavenlyBody>();

    private float gravity = 0;
    private float distance = 0;

    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void FixedUpdate()
    {
        //Calculate gravity
        foreach(HeavenlyBody body in gravityBodies)
        {
            distance = Vector3.Distance(body.pos, transform.position);
            gravity = body.gravityForce / (distance * distance);
            pv_gravity = gravity * Vector3.Normalize(body.pos - transform.position);
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
        gravityBodies.Add(other.gameObject.GetComponent<HeavenlyBody>());
    }

    void OnTriggerExit(Collider other)
    {
        foreach(HeavenlyBody body in gravityBodies)
        {
            if(other.gameObject.name.Equals(body.bodyName))
            {
                gravityBodies.Remove(body);
                Debug.Log("Leaving " + body.bodyName);
            }
        }
    }
}
