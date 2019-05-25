using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    public FloatValue HeartContainers;
    public FloatValue PlayerCurrentHealth;


    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < HeartContainers.RuntimeValue; i++)
        {
            if (i < Hearts.Length)
            {
                Hearts[i].gameObject.SetActive(true);
                Hearts[i].sprite = FullHeart;

            }
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = PlayerCurrentHealth.RuntimeValue / 2;

        for(int i = 0; i < HeartContainers.RuntimeValue; i++)
        {
            if(i <= tempHealth - 1)
            {
                //full heart
                Hearts[i].sprite = FullHeart;
            }
            else if(i >= tempHealth)
            {
                //empty heart
                Hearts[i].sprite = EmptyHeart;
            }
            else
            {
                //half heart
                Hearts[i].sprite = HalfHeart;
            }
        }
    }
}
