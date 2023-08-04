using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public bool speedPicked = false;


    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            speedPicked = true;
            Destroy(gameObject);
        }



    }


}

