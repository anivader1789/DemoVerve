using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    [HideInInspector]
    public HeavenlyBody Po_currentOrbitBody;

    private GameObject po_player;

    private float pf_orbitBodyDist = 0;
    private float pf_orbitChangeThresh = 1;
    private float pf_orbitConfidence = 0;

    public TMP_Text Pui_orbitConfTextUI;

    // Start is called before the first frame update
    void Start()
    {
        GameObject light = GameObject.Find("Directional Light");
        po_player = GameObject.Find("Player");
        StartCoroutine(sunLightUpdate(light));
        //StartCoroutine(orbitCheck());
    }


    //Sun light follows player
    //This creates the necessary sun light we need in our solar system
    IEnumerator sunLightUpdate(GameObject directionalLight)
    {
        while(true)
        {
            directionalLight.transform.LookAt(po_player.transform);
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    //Orbit feedback system
    IEnumerator orbitCheck()
    {
        while (true)
        {
            if (Po_currentOrbitBody == null)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            float newDist = Vector3.Distance(Po_currentOrbitBody.pos, po_player.transform.position);
            if(Mathf.Abs(newDist - pf_orbitBodyDist) < pf_orbitChangeThresh)
            {
                pf_orbitConfidence = Mathf.Min(1f, pf_orbitConfidence + 0.1f);
            } else
            {
                pf_orbitConfidence = Mathf.Max(0f, pf_orbitConfidence - 0.1f);
            }

            pf_orbitBodyDist = newDist;
            Pui_orbitConfTextUI.color = new Color(1f-pf_orbitConfidence, pf_orbitConfidence, 1f);

            Debug.Log(pf_orbitConfidence);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
