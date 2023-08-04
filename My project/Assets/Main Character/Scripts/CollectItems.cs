using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{

    public int coinCount = 0;
    public int gemCount = 0;

    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin")){
            coinCount++;
            Destroy(other.gameObject);

        }

        if(other.CompareTag("Gem")){
            gemCount++;
            Destroy(other.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
