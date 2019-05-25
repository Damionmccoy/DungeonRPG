using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{

    public Slider MagicSlider;
    public Inventory PlayerInventory;

    // Start is called before the first frame update
    void Start()
    {
        MagicSlider.maxValue = PlayerInventory.MaxMagic;
        MagicSlider.value = PlayerInventory.MaxMagic;
        PlayerInventory.CurrentMagic = PlayerInventory.MaxMagic;
    }

    /// <summary>
    /// This is used to increase the magic slider value
    /// </summary>
    /// <param name="_value">how much to increase the slider</param>
    public void AddMagic()
    {
        MagicSlider.value = PlayerInventory.CurrentMagic;
        if (MagicSlider.value > MagicSlider.maxValue)
        {
            MagicSlider.value = MagicSlider.maxValue;
            PlayerInventory.CurrentMagic = PlayerInventory.MaxMagic;
        }
    }

    /// <summary>
    /// This is used to decrease the magic slider value
    /// </summary>
    /// <param name="_value">how much to decrease the slider</param>
    public void DecreseMagic()
    {
        MagicSlider.value = PlayerInventory.CurrentMagic;
        if (MagicSlider.value < 0)
        {
            MagicSlider.value = 0;
            PlayerInventory.CurrentMagic = 0;
        }
    }
    

    



}
