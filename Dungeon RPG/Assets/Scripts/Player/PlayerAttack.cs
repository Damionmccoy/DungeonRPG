using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Weapon;
    private Animator weaponAnim;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        if (Weapon != null)
        {
            weaponAnim = Weapon.GetComponent<Animator>();

            if (weaponAnim == null)
            {
                Debug.Log("Player weapon has no animator");
            }
            Weapon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && weaponAnim != null && player.GetPlayerState() != PlayerState.attack 
            && player.GetPlayerState() != PlayerState.stagger && player.GetPlayerState() != PlayerState.interact )
        {
            Weapon.SetActive(true);
            player.UpdatePlayerState(PlayerState.attack);
            weaponAnim.SetTrigger("attack");
            StartCoroutine(AttackCo());
        }
        else if(Input.GetKeyUp(KeyCode.Space) && weaponAnim != null)
        {

            if (player.GetPlayerState() != PlayerState.interact)
            {
                player.UpdatePlayerState(PlayerState.idle);
            }
        }
    }
    

    IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(.4f);
        Weapon.SetActive(false);
        weaponAnim.ResetTrigger("attack");

    }
}
