using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{

    public GameObject[] Dots;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        int dotToUse = Random.Range(0, Dots.Length);
        GameObject dot = Instantiate(Dots[dotToUse], transform.position, Quaternion.identity);
        dot.transform.parent = transform;
        dot.name = dot.transform.parent.name;
    }
}
