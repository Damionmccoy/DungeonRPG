using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pot : MonoBehaviour
{
    
    Animator potAnim;

    // Start is called before the first frame update
    void Start()
    {
        potAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SmashPot()
    {
        potAnim.SetTrigger("break");
        StartCoroutine(BreakCo());
    }

    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
