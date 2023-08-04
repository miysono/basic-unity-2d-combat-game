using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Combat player;
    public float pickupHealthBonus = 40f;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.currentHealthPoints = player.currentHealthPoints + pickupHealthBonus;
            if(player.currentHealthPoints > player.healthPoints)
            {
                player.currentHealthPoints = player.healthPoints;
            }
            Destroy(gameObject);
        }
    }


    void Update()
    {
        transform.rotation *= Quaternion.Euler(0,0,10 * Time.deltaTime);
    }

}

