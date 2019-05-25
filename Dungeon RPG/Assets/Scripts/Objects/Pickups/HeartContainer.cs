using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Powerup
{

    public FloatValue HeartContainers;
    public FloatValue PlayerHealth;

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.CompareTag("Player") && !_other.isTrigger)
        {
            HeartContainers.RuntimeValue += 1;
            PlayerHealth.RuntimeValue = HeartContainers.RuntimeValue * 2;
            PowerupSignal.Raise();
            Destroy(gameObject);
        }
    }
}
