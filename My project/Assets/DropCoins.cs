using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    Rigidbody2D itemBody;
    public float dropForce = 10f;
    void Start()
    {
        itemBody = GetComponent<Rigidbody2D>();
        itemBody.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - 1 > 0)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        else GetComponent<Collider2D>().enabled = false;
    }
}
