using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool playerInRange;
    public Signal Context;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger)
        {
            Context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger)
        {
            Context.Raise();
            playerInRange = false;
        }
    }
}
