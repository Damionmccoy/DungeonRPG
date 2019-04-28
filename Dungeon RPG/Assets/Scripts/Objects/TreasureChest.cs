using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : Interactable
{
    public Inventory PlayerInventory;
    public Item Contents;
    public bool IsOpen;
    public Signal RaiseItem;
    public GameObject DialogBox;
    public TextMeshProUGUI DialogText;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!IsOpen)
            {
                //open the chest
                OpenChest();
            }
            else
            {
                //chest is already open
                ChestOpened();
            }
        }
    }

    public void OpenChest()
    {
        anim.SetBool("open", true);
        //open the dialog box
        DialogBox.SetActive(true);
        //display the discription
        DialogText.text = Contents.ItemDescription;
        //add the item to the players inventory
        PlayerInventory.AddItem(Contents);
        //set the item to the current item
        PlayerInventory.CurrentItem = Contents;
        //Raise the signal
        RaiseItem.Raise();
        //set the chest to open
        IsOpen = true;
        //raise the context clue
        Context.Raise();
    }

    public void ChestOpened()
    {

            //turn the dialog window off and clear the text field
            DialogText.text = string.Empty;
            DialogBox.SetActive(false);
            //set the current item to empty
            //PlayerInventory.CurrentItem = null;
            //raise the signal to the player to stop animating
            RaiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger && !IsOpen)
        {
            Context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger && !IsOpen)
        {
            Context.Raise();
            playerInRange = false;
        }
    }
}
