using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PickSwords : MonoBehaviour
{
    public float swordDamage;
    public bool keyPressed = false;
    bool triggerEntered = false;

    public GameObject uiStats;
    public GameObject uiIcon;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            triggerEntered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            triggerEntered = false;
        }
    }



    void Start(){

        uiStats.SetActive(false);
        uiIcon.SetActive(false);

    }

    void Update()
    {


        if(triggerEntered)
        {
            uiStats.SetActive(true);
        }else if(!triggerEntered) uiStats.SetActive(false);

        if(triggerEntered && Input.GetKeyDown(KeyCode.F))
        {
            //CHANGE ICON IN UI WITH CURRENT WEAPONS
            uiStats.SetActive(false);
            uiIcon.SetActive(true);
           
            //ADD WEAPON DAMAGE TO SCRIPT DAMAGE

            Destroy(gameObject);
        }
        
        

    }
}
