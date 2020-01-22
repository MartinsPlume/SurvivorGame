using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRound : MonoBehaviour
{
    [Header("Cannon Stats")]
    public float reloadTime = 10f; // samainot šo var uztaisīt, ka šauj ļoti bieži :D
    public GameObject shotRound; // lādiņš


    [Header("References")]
    public GameObject explosionEffect;

    [Header("Code")]
    float countdown=0f;
    int loadingMistake = 0;
    private GameObject instantiatedObj;


    private void Start()
    {
        // Lai katrs lielgabals šauj dažādi --> Sākumā katram savs aiztures laiks
        countdown = UnityEngine.Random.Range(5, 10);
    }

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        { 
        OpenFire();
            // "Pārlādē lielgabalu" var kas tiek dažādots, var sanākt ātrāk vai lēnāk
            loadingMistake = UnityEngine.Random.Range(-5, 5);
        countdown = reloadTime + loadingMistake;
        }

    }

    // Lielgabala izšaušana, nospēlē skaņu, izveido sprādziena objektu, izveido lodi, izdzēš sprādziena objektu
    private void OpenFire()
    {
        FindObjectOfType<AudioManager>().Play("Waffe");
        instantiatedObj = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject currentRound = Instantiate(shotRound, transform.position, transform.rotation);
        Destroy(instantiatedObj,4);
    }
}
