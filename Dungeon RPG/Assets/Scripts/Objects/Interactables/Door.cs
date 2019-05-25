using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    public DoorType ThisDoorType;
    public bool IsOpen = false;
    public Inventory PlayerInventory;
    public SpriteRenderer DoorSprite;
    public BoxCollider2D PhysicsCollider;
    [SerializeField]
    private Sprite doorOpenSprite;
    [SerializeField]
    private Sprite doorClosedSprite;




    private void Start()
    {

        DoorSprite.enabled = true;
        if (!IsOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        //switch to a door open sprite 
        DoorSprite.enabled = false;
        //set open to true
        IsOpen = true;
        //turn off the doors box collider
        PhysicsCollider.enabled = false;
    }

    public void Close()
    {
        //switch to a door open sprite 
        DoorSprite.enabled = true;
        //set open to true
        IsOpen = false;
        //turn off the doors box collider
        PhysicsCollider.enabled = true;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (playerInRange && ThisDoorType == DoorType.key)
            {
                //does the player have a key
                if(PlayerInventory.NumberOfKeys > 0)
                {
                    //remove a key
                    PlayerInventory.NumberOfKeys--;
                    //if sothen call the open method
                    Open();
                }


            }
        }
    }
}
