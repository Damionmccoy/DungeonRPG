using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //public float DamageAmount;
    public float MoveSpeed;
    public Vector2 DirectionToMove;
    public float LifeTime;
    private float lifetimeSeconds;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetimeSeconds = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;

        if (lifetimeSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch( Vector2 _target)
    {
        rb.velocity = _target * MoveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        DestroyGameObject();
    }

    /// <summary>
    /// This is where the project is distroyed use this function for any effects needed
    /// </summary>
    public virtual void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
