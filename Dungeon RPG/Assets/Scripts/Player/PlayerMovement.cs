using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public variables
    [Header("Player Health Variables")]
    public FloatValue CurrentHealth;
    public Signal PlayerHealthSignal; //This is the signal that is raised when we need to update the GUI for the players health.



    [Header("Player Stored positions")]
    public VectorValue StartingPosition;

    [Header("Player Inventory Variables")]
    public Inventory PlayerInventory; //This is a scriptable object that hold the players inventory
    public SpriteRenderer receivedItemSprite;

    [Header("Player Effects")]
    public Signal PlayerHitSignal;  // This is raised when the player is hit.

    [Header("IFrame Stuff")]
    public Color FlashColor;    //The color to flash to
    public Color RegularColor;  //The regular color of the sprite should be white and clear
    public float FlashDuration; // how long we want the individual flash to be
    public int NumOfFlashes;    //How many flashes to show
    public Collider2D TriggerCollider;
    public SpriteRenderer mySprite;

    [SerializeField, Header("Player Movement Variables")]
    private float speed; //how fast the player can move
    [HideInInspector]
    public Vector3 changePos; //Hold the input from the vertical and horizontal axis

    //Basic components needed to move and animate the player
    private Rigidbody2D playerRB;
    private Animator playerAnim;

    [SerializeField,Header("The players state machine")]
    private Player player;
    


    void Start()
    {
        //Initialize the needed variables
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        player = GetComponent<Player>();
        transform.position = StartingPosition.InitialValue;

    }

    // Update is called once per frame
    void Update()
    {
        //Is the player in an interaction we don't want the player to move while in the interact state.
        if (player.GetPlayerState() != PlayerState.interact)
        {
            //zero out the change so that only this frames movment is calculated. 
            changePos = Vector3.zero;
            //Get the input from the axis' so that we know where the player is moveing
            changePos.x = Input.GetAxisRaw("Horizontal"); // -1 left -- 1 right
            changePos.y = Input.GetAxisRaw("Vertical"); // -1 down -- 1 up


            //This is commented out unless we want to stop the player from moving while attacking so i left it in
            //if (player.GetPlayerState() != PlayerState.attack)
            //{
            // If we have a change to movement and the player is in the walk or idle state move them
            if (changePos != Vector3.zero && (player.GetPlayerState() == PlayerState.walk || player.GetPlayerState() == PlayerState.idle))
            {
                player.UpdatePlayerState(PlayerState.walk);
                MovePlayer(changePos);
            }
            else
            {
                //If the player isn't moving set the state to idle and set the proper animation.
                playerAnim.SetBool("moving", false);
                player.UpdatePlayerState(PlayerState.idle);
            }
            //}
        }
    }


    /// <summary>
    /// Moves the player around the screen using a vector3 and the movePosition function.
    /// </summary>
    /// <param name="_change">this is the vector3 used to decide how to move</param>
    void MovePlayer(Vector3 _change)
    {
        //Normalize the vector so that the player isn't moving faster when walking dialgonally
        _change.Normalize();
        // set the new position the player will move to muliplied by the speed s/he will move and the delta time between frames;
        Vector3 move = transform.position + _change * speed * Time.deltaTime;
        //move the player using move position 
        playerRB.MovePosition(move);
        //Set the animator to show the player moving
        playerAnim.SetFloat("moveX", changePos.x);
        playerAnim.SetFloat("moveY", changePos.y);
        playerAnim.SetBool("moving", true);
    }


    /// <summary>
    /// This function is called when the player takes knockback damage
    /// </summary>
    /// <param name="_knockTime">how long the player will move before its velocity is set back to zero</param>
    /// <param name="_damage">How much damage the player will recieve</param>
    public void Knock(float _knockTime,float _damage)
    {
        //decrease player health if needed
        CurrentHealth.RuntimeValue -= _damage;
        //Update the GUI health value
        PlayerHealthSignal.Raise();

        //Check to see if the player is still alive to be knocked back
        if (CurrentHealth.RuntimeValue > 0)
        { 
            //If the player is alive knock him back
            StartCoroutine(KnockCo(_knockTime));
        }
        else //This is when the player dies.
        {
            //if the player is dead disable the game object so the player isn't visable anymore. 
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// This function is used when the player is opening a chest or something similar
    /// </summary>
    public void RaiseItem()
    {
        //make sure the player hasn't already gotten this item
        if (PlayerInventory.CurrentItem != null)
        {
            //Make sure the player isn't already in the interacting state 
            if (player.GetPlayerState() != PlayerState.interact)
            {
                //Set the players animation to the pick up animation
                playerAnim.SetBool("pickup item", true);
                //change the players state to interact so s/he can't move 
                player.UpdatePlayerState(PlayerState.interact);
                //Set the recieved item sprite so the player can see what they picked up.
                receivedItemSprite.sprite = PlayerInventory.CurrentItem.ItemSprite;
            }
            else
            {
                //If the player is already in the interact state then turn the  pickup animation off 
                playerAnim.SetBool("pickup item", false);
                // Set the players state back to idle so the player can move again.
                player.UpdatePlayerState(PlayerState.idle);
                //Reset the recieved item sprite back to null so it wont accedentally get displayed again
                receivedItemSprite.sprite = null;
                //Set the current inventory item to null so its ready to be used again
                PlayerInventory.CurrentItem = null;
            }
        }
    }

    /// <summary>
    /// This Coroutine is used to knock the player back for a set time. 
    /// </summary>
    /// <param name="_knockTime">The time the player will be knocked back for</param>
    /// <returns></returns>
    IEnumerator KnockCo( float _knockTime)
    {
        //Set off the camera effect
        PlayerHitSignal.Raise();
        //Set the player to the stagger state so that s/he isn't really able to move other then to get knocked back
        player.UpdatePlayerState(PlayerState.stagger);
        //Start the corutine for the flashing and invonerability
        StartCoroutine(FlashCo());
        //Wait until the knock time has expired 
        yield return new WaitForSeconds(_knockTime);
        //Set the players velocity to zero so s/he isn't moving anymore from the knockback this avoids the player sliding like on ice
        playerRB.velocity = Vector2.zero;
        //Make sure the player state is updated so the player can move again.
        player.UpdatePlayerState(PlayerState.idle);
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        TriggerCollider.enabled = false;

        while (temp < NumOfFlashes)
        {
            mySprite.color = FlashColor;
            yield return new WaitForSeconds(FlashDuration);
            mySprite.color = RegularColor;
            yield return new WaitForSeconds(FlashDuration);
            temp++;
        }
        TriggerCollider.enabled = true;
    }
}
