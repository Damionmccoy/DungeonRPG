using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool Active;
    public BoolValue StoredValue;
    public Sprite ActiveSprite;
    public Sprite InactiveSprite;
    public Door ThisDoor;
    private SpriteRenderer CurrentSprite;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSprite = GetComponent<SpriteRenderer>();
        Active = StoredValue.RuntimeValue;
        ChangeDoorState(Active);

        
    }

    public void ActivateSwitch()
    {
        Active = true;
        StoredValue.RuntimeValue = Active;
        ThisDoor.Open();
        CurrentSprite.sprite = ActiveSprite;
    }

    public void DeactivateSwitch()
    {
        Active = false;
        StoredValue.RuntimeValue = Active;
        ThisDoor.Close();
        CurrentSprite.sprite = InactiveSprite;
    }

    public void ChangeDoorState(bool _active)
    {
        if (Active)
        {
            ActivateSwitch();
        }
        else
        {
            DeactivateSwitch();
        }
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.CompareTag("Player") && !_other.isTrigger)
        {
            ActivateSwitch();
        }
    }
}
