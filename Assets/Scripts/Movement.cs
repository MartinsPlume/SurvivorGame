using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float min = -50f;
    public float max = 50f;
    public bool SideCannon = false;
    float random=0f;
    float randomSpeed = 0f;

    private void Start()
    {
        random=UnityEngine.Random.Range(0, 5);
        randomSpeed= UnityEngine.Random.Range(5, 10);

    }

    // Update is called once per frame
    void Update()
    {
        random -= Time.deltaTime;
        if (random <= 0) { 
        if (!SideCannon)
        {
        transform.position = new Vector3(Mathf.PingPong(Time.time* randomSpeed, max - min) + min, transform.position.y, transform.position.z);
        }
        else { 
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * randomSpeed, max - min) + min);
        }
        }
    }
}
