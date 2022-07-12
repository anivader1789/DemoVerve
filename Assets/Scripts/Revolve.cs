using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float rotateTimeGap;
    public float revolveTimeGap;
    public float revolveSpeed;

    public Transform revolveAroundObject;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rotate());
        if(revolveAroundObject != null)
        {
            StartCoroutine(revolve());
        }
        
    }

    IEnumerator rotate()
    {
        while(true)
        {
            yield return new WaitForSeconds(rotateTimeGap);
            transform.Rotate(
                rotateX * Time.deltaTime,
                rotateY * Time.deltaTime,
                rotateZ * Time.deltaTime
            );
        }
    }

    IEnumerator revolve()
    {
        while(true)
        {
            yield return new WaitForSeconds(revolveTimeGap);
            transform.RotateAround(revolveAroundObject.position, Vector3.up, revolveSpeed * Time.deltaTime);
        }
    }
}
