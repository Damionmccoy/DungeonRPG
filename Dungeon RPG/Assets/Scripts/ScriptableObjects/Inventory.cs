using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu,System.Serializable]
public class Inventory : ScriptableObject
{
    public Item CurrentItem;
    public List<Item> Items = new List<Item>();
    public int NumberOfKeys;
    public int NumberOfCoins;
    public float MaxMagic =  10;
    public float CurrentMagic;


    private void OnEnable()
    {
        CurrentMagic = MaxMagic;
    }

    public void ReduceMagic(float _cost)
    {
        CurrentMagic -= _cost;
    }


    public void AddItem(Item ItemToAdd)
    {
        if (ItemToAdd.IsKey)
        {
            NumberOfKeys++;
        }
        else
        {
            if (!Items.Contains(ItemToAdd))
            {
                Items.Add(ItemToAdd);
            }
        }
    }

    /// <summary>
    /// This checks the inventory to see if an item exists
    /// </summary>
    /// <param name="_item"></param>
    /// <returns>retruns true if the item is found</returns>
    public bool CheckForItem(Item _item)
    {
        if (Items.Contains(_item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
