using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarCounter : MonoBehaviour
{

    public Combat combat;
    public TextMeshProUGUI healthBarCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarCounter.text = combat.currentHealthPoints.ToString() + " / " + combat.healthPoints.ToString();
        
    }
}
