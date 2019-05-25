using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Player Magic Variables")]
    //public FloatValue CurrentMagic;
    public Signal ReduceMagic; //This is the signal that is raised when we need to update the GUI for the players magic.

    [Header("Player Melee Weapon Variables")]
    public GameObject Weapon;
    private Animator weaponAnim;

    [Header("Projectile Variables")]
    public GameObject Projectile;
    public Item Bow;

    [Header("Player Variables")]
    public Player player;
    public Inventory PlayerInventory;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>(); // Get the player data

        anim = GetComponent<Animator>(); // get the players animator 

        if (Weapon != null) //Check if the player has a weapon
        {
            weaponAnim = Weapon.GetComponent<Animator>(); //Get the weapons animator 

            if (weaponAnim == null)//Make sure we found the animaotry
            {
                Debug.Log("Player weapon has no animator");
            }
            else
            { //Set the  weapon to inactive so its only visable when the player is attacking. 
                Weapon.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is hitting the attack button while holding a weapon and not already in the attack state, the stagger state or the interact state. 
        if (Input.GetButtonDown("Attack") && weaponAnim != null && player.GetPlayerState() != PlayerState.attack 
            && player.GetPlayerState() != PlayerState.stagger && player.GetPlayerState() != PlayerState.interact )
        {
            //set the weapon active
            Weapon.SetActive(true);
            //place the player into the attack state
            player.UpdatePlayerState(PlayerState.attack);
            //activate the weapons animation
            weaponAnim.SetTrigger("attack");
            //Start the attack corroutine
            StartCoroutine(AttackCo());
        }
        //This is the same as the last if statement but its looking for the player second attack or ranged attack
        else if(Input.GetButtonDown("Attack2") && weaponAnim != null && player.GetPlayerState() != PlayerState.attack
            && player.GetPlayerState() != PlayerState.stagger && player.GetPlayerState() != PlayerState.interact)
        {
            //Check to see if the player has the bow then if it does fire it
            if (PlayerInventory.CheckForItem(Bow))
            {
                StartCoroutine(Attack2Co());
            }
        }
       
    }
    

    IEnumerator AttackCo()
    {
        //Wait for the animation to finish
        yield return new WaitForSeconds(.4f);
        //Set the weapon inactive so it is no longer visable
        Weapon.SetActive(false);
        //Reset the weapon animation
        weaponAnim.ResetTrigger("attack");
        //if the player isn't in the interact state
        if (player.GetPlayerState() != PlayerState.interact)
        {
            //set the player state to idle
            player.UpdatePlayerState(PlayerState.idle);
        }

    }

    IEnumerator Attack2Co()
    {
       //set the player to the attack state
        player.UpdatePlayerState(PlayerState.attack);
        //wait one frame
        yield return null;
        //shoot an arrow
        MakeArrow();
        //yield return new WaitForSeconds(.4f);
        //Weapon.SetActive(false);
        //weaponAnim.ResetTrigger("attack");


        //if the player isn't in the interact state
        if (player.GetPlayerState() != PlayerState.interact)
        {
            //set the player state to idle
            player.UpdatePlayerState(PlayerState.idle);
        }
    }


    /// <summary>
    /// Calculates the direction the arrow should be rotated
    /// </summary>
    /// <returns>returns a new rotation based on the players input</returns>
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(anim.GetFloat("moveY"),anim.GetFloat("moveX"))* Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    /// <summary>
    /// Instantiates a new arrow to shoot at an enemy
    /// </summary>
    private void MakeArrow()
    {
        if (PlayerInventory.CurrentMagic > 0)
        {
            Vector2 temp = new Vector2(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
            Arrow arrow = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            PlayerInventory.ReduceMagic(arrow.MagicCost);
            ReduceMagic.Raise();
        }


    }
}
