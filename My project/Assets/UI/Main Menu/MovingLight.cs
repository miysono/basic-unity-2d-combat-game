using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    
    public float lightSpeed = 15f;
    float lastChangedDirection = 0f;
    float timePerDirection = 8f;
    float tick = 1;

    bool goingLeft;
    bool goingRight;


    // Update is called once per frame
    void Update()
    {
        if(Time.time - timePerDirection > lastChangedDirection) //3-3 > 0 //9-6>3
        {
            transform.position = transform.position + new Vector3(-lightSpeed,0,0) * Time.deltaTime;
            if(Time.time - (timePerDirection * tick) > timePerDirection) //6-3>3 changes //9-6>3
                {
                    lastChangedDirection = Time.time; //lastchanged = 6 //lastchanged =9
                    tick=tick+2; //tick = 2

                }

        } 
        else 
        {
            transform.position = transform.position + new Vector3(lightSpeed,0,0) * Time.deltaTime;
        }
    }
}
