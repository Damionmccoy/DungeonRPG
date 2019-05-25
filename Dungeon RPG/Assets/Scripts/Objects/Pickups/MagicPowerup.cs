using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerup : Powerup
{

    public Inventory PlayerInventory;
    public float MagicValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player") && !_other.isTrigger)
        {
            PlayerInventory.CurrentMagic += MagicValue;
            PowerupSignal.Raise();
            Destroy(gameObject);
        }
    }
}
