using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinNumber : MonoBehaviour
{
    public TextMeshProUGUI coinCountText;
    public CollectItems collectItems;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = collectItems.coinCount.ToString();
    }
}
