using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : Interactable
{
    [SerializeField]
    GameObject DialogBox;
    [SerializeField]
    TextMeshProUGUI txtDisplayBox;
    [SerializeField]
    string txt2Display;

    //public Signal ContextOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (DialogBox.activeInHierarchy)
            {
                txtDisplayBox.text = string.Empty;
                DialogBox.SetActive(false);
            }
            else
            {
                DialogBox.SetActive(true);
                txtDisplayBox.text = txt2Display;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger)
        {
            Context.Raise();
            playerInRange = false;
            txtDisplayBox.text = string.Empty;
            DialogBox.SetActive(false);
        }
    }
}
