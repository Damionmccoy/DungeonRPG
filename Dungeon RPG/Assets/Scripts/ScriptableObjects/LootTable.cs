using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    /// <summary>
    /// This is the power up that is given to the player 
    /// </summary>
    public Powerup ThisLoot;
    /// <summary>
    /// This is the percent chance of the object dropping
    /// </summary>
    [Range(0,100)]
    public float LootChance;

}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] Loots;

    public Powerup GetLoot()
    {

        float cumProb = 0f;
        float currentProb = Random.Range(0, 100);

        for(int i = 0; i < Loots.Length; i++)
        {
            cumProb += Loots[i].LootChance;
            if(currentProb <= cumProb)
            {
                return Loots[i].ThisLoot;
            }
        }

        return null;
    }
}
