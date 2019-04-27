using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLog : Enemy
{

    public Transform Target;
    public float ChaseRadius;
    public float AttackRadius;
    public Transform HomePosition;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        CurrrentState = EnemyState.idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector2.Distance(Target.position,transform.position) <= ChaseRadius && Vector2.Distance(Target.position,transform.position) > AttackRadius)
        {
            if (CurrrentState == EnemyState.idle || CurrrentState == EnemyState.walk && CurrrentState != EnemyState.stagger) 
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, Target.position, BaseMoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - new Vector2(transform.position.x, transform.position.y));
                rb.MovePosition(temp);
                
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeup", true);
            }
        }
        else if(Vector2.Distance(Target.position, transform.position) >= ChaseRadius)
        {
            anim.SetBool("wakeup", false);
        }
    }


    private void SetAnimFloat(Vector2 _moveVector)
    {
        anim.SetFloat("moveX", _moveVector.x);
        anim.SetFloat("moveY", _moveVector.y);
    }

    private void ChangeAnimation(Vector2 _direction)
    {
        if(Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
        {
            Debug.Log(_direction);

            if(_direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(_direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if(Mathf.Abs(_direction.x) < Mathf.Abs(_direction.y))
        {
            if (_direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (_direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }

    }


}
