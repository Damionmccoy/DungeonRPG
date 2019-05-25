using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{

    public Inventory PlayerInventory;
    

    // Start is called before the first frame update
    void Start()
    {
        PowerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player") && !_other.isTrigger)
        { 
            PlayerInventory.NumberOfCoins += 1;
            PowerupSignal.Raise();
            Destroy(gameObject);
        }
    }
}
