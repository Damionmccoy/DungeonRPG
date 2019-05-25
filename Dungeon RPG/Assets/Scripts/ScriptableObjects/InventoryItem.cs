using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemImage;
    public int NumberHeld;
    public bool Usable;
    public bool Unique;
}
