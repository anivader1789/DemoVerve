using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenlyBody : MonoBehaviour
{
    [HideInInspector]
    public float gravityForce;

    [HideInInspector]
    public Vector3 pos;

    [HideInInspector]
    public string bodyName;

    void Start()
    {
        pos = transform.position;
        bodyName = gameObject.name;
        gravityForce = transform.localScale.x * 10;
    }
}
