using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    SpaceSuitController suitController;

    public float thurstAmount;
    Vector3 thrustVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        suitController = this.gameObject.GetComponent<SpaceSuitController>();
    }

    // Update is called once per frame
    void Update()
    {
        thrustVec = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") + transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal");
        suitController.Pv_thrust = thurstAmount * thrustVec;
    }
}
