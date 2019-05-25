using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [Header("Arrow Values")]
    public float Speed;
    public bool IsRanged;
    public float LifeTime;
    private float lifeTimeCounter;
    public float Range;
    public float MagicCost;

    [Header("Arrow Components")]
    public Rigidbody2D RB;



    void Start()
    {
        lifeTimeCounter = LifeTime;
    }

    public void Setup(Vector2 _velocity, Vector3 _direction)
    {
        RB.velocity = _velocity.normalized * Speed;
        transform.rotation = Quaternion.Euler(_direction);
    }

    public void Update()
    {
        lifeTimeCounter -= Time.deltaTime;
        if(lifeTimeCounter <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        

    }
}
