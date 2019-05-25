using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinTextManager : MonoBehaviour
{

    public Inventory PlayerInventory;
    public TextMeshProUGUI CoinDisplay;


    public void UpdateCoinCount()
    {
        CoinDisplay.text = "" + PlayerInventory.NumberOfCoins;
    }

}
