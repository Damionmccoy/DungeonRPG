using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D playerRB;
    private Animator playerAnim;
    private Vector3 changePos;
    private Player player;
    public FloatValue CurrentHealth;
    public Signal PlayerHealthSignal;
    public VectorValue StartingPosition;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        player = GetComponent<Player>();
        transform.position = StartingPosition.InitialValue;

    }

    // Update is called once per frame
    void Update()
    {
        changePos = Vector3.zero;
        changePos.x = Input.GetAxisRaw("Horizontal");
        changePos.y = Input.GetAxisRaw("Vertical");


        //if (player.GetPlayerState() != PlayerState.attack)
        //{
            if (changePos != Vector3.zero && (player.GetPlayerState() == PlayerState.walk || player.GetPlayerState() == PlayerState.idle))
            {
                player.UpdatePlayerState(PlayerState.walk);
                MovePlayer(changePos);
            }
            else
            {
                playerAnim.SetBool("moving", false);
                player.UpdatePlayerState(PlayerState.idle);
            }
        }
    //}


    /// <summary>
    /// Moves the player around the screen using a vector3 and the movePosition function.
    /// </summary>
    /// <param name="_change">this is the vector3 used to decide how to move</param>
    void MovePlayer(Vector3 _change)
    {
        _change.Normalize();

        Vector3 move = transform.position + _change * speed * Time.deltaTime;
        
        playerRB.MovePosition(move);
        playerAnim.SetFloat("moveX", changePos.x);
        playerAnim.SetFloat("moveY", changePos.y);
        playerAnim.SetBool("moving", true);
    }

    public void Knock(float _knockTime,float _damage)
    {
        CurrentHealth.RuntimeValue -= _damage;
        PlayerHealthSignal.Raise();
        if (CurrentHealth.RuntimeValue > 0)
        { 
            StartCoroutine(KnockCo(_knockTime));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator KnockCo( float _knockTime)
    {
        player.UpdatePlayerState(PlayerState.stagger);
        yield return new WaitForSeconds(_knockTime);
        playerRB.velocity = Vector2.zero;
        player.UpdatePlayerState(PlayerState.idle);
    }
}
