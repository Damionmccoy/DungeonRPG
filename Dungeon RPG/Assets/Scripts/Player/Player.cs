using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerState curState;


    // Start is called before the first frame update
    void Start()
    {
        curState = PlayerState.idle;
    }





    /// <summary>
    /// Change the player state 
    /// </summary>
    /// <param name="_nextState">This is the state you want to change too</param>
    public void UpdatePlayerState(PlayerState _nextState)
    {
        curState = _nextState;
    }
    /// <summary>
    /// Gets the player current state
    /// </summary>
    /// <returns>players current state</returns>
    public PlayerState GetPlayerState()
    {
        return curState;
    }
}
