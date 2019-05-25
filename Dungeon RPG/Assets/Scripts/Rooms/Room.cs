using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Variables")]
    public Enemy[] Enemies;
    public Pot[] Pots;
    public GameObject VirtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.CompareTag("Player") && !_other.isTrigger)
        {
            //Activate All enemies
            for(int i = 0; i < Enemies.Length; i++)
            {
                ChangeActivation(Enemies[i],true);
            }

            //Activate All  pots
            for (int i = 0; i < Pots.Length; i++)
            {
                ChangeActivation(Pots[i], true);
            }
            VirtualCamera.SetActive(true); //activate the room camera
        }
    }

    public virtual void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger)
        {
            //Deactivate All enemies and pots
            for (int i = 0; i < Enemies.Length; i++)
            {
                ChangeActivation(Enemies[i], false);
            }

            //Deactivate All  pots
            for (int i = 0; i < Pots.Length; i++)
            {
                ChangeActivation(Pots[i], false);
            }
            VirtualCamera.SetActive(false); //Deactivate the room camera
        }
    }

    private void OnDisable()
    {
        VirtualCamera.SetActive(false);
    }

    public void ChangeActivation(Component _component, bool _activation)
    {
        //Changes the activation of a game object could be moved to a helper class later for use all over the game
        _component.gameObject.SetActive(_activation);
    }
}
