using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemNumber : MonoBehaviour
{

    public CollectItems collectItems;
    public TextMeshProUGUI gemCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gemCountText.text = collectItems.gemCount.ToString();
    }
}
