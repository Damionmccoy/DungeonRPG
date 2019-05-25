using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : DungeonRoom
{

    [Header("Enemy Room Verialbes")]
    public Door[] Doors;
    




    public override void OnTriggerEnter2D(Collider2D _other)
    {
        //when the player enters the room
        if (_other.CompareTag("Player") && !_other.isTrigger)
        {
            //Activate All enemies
            for (int i = 0; i < Enemies.Length; i++)
            {
                ChangeActivation(Enemies[i], true);
            }

            //Activate All  pots
            for (int i = 0; i < Pots.Length; i++)
            {
                ChangeActivation(Pots[i], true);
            }
            
            VirtualCamera.SetActive(true); //activate the room camera
            CloseDoors();
        }

        
    }


    public override void OnTriggerExit2D(Collider2D _other)
    {
        //When the player exits the room
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
            VirtualCamera.SetActive(false); //deactivate the room camera
        }
    }


    /// <summary>
    /// This method checks all the enemies if all the enemies are dead it will open the doors.
    /// </summary>
    public void CheckEnemies()
    {
        for(int i = 0; i <Enemies.Length; i++) //search through each enemy 
        {
            if (Enemies[i].gameObject.activeInHierarchy && i < Enemies.Length - 1) // if even one enemy is active don't open the doors
            {
                return; //if active return before opening the doors
            }          
        }
        OpenDoors(); //If all the enemies are inactive then they are dead so open the doors. 
    }

    /// <summary>
    /// Closes all the doors in the room
    /// </summary>
    public void CloseDoors()
    {
        for(int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Close();
        }
    }

    /// <summary>
    /// Opens all the doors in the room.
    /// </summary>
    public void OpenDoors()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Open();
        }
    }
}
