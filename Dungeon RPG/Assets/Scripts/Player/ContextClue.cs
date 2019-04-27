using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject CharacterContextClue;
    public bool ContextActive = false;


    public void SetContextActive()
    {
        ContextActive = !ContextActive;

        CharacterContextClue.SetActive(ContextActive);
       
    }

}
