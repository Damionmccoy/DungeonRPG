using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{

    public FloatValue PlayerHealth;
    public FloatValue HeartContainers;
    public float AmountToIncrease; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Player") && !_other.isTrigger)
        {
            PlayerHealth.RuntimeValue += AmountToIncrease;
            if(PlayerHealth.InitialValue > HeartContainers.RuntimeValue * 2f)
            {
                PlayerHealth.InitialValue = HeartContainers.RuntimeValue * 2f;
            }

            PowerupSignal.Raise();
            Destroy(gameObject);
        }   
    }
}
