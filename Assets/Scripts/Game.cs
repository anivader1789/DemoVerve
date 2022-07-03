using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameObject light = GameObject.Find("Directional Light");
        GameObject player = GameObject.Find("Player");
        StartCoroutine(sunLightUpdate(light, player));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Sun light follows player
    //This creates the necessary sun light we need in our solar system
    IEnumerator sunLightUpdate(GameObject directionalLight, GameObject player)
    {
        while(true)
        {
            directionalLight.transform.LookAt(player.transform);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
