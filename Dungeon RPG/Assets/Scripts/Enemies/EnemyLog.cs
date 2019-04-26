using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLog : Enemy
{

    public Transform Target;
    public float ChaseRadius;
    public float AttackRadius;
    public Transform HomePosition;



    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector2.Distance(Target.position,transform.position) <= ChaseRadius && Vector2.Distance(Target.position,transform.position) > AttackRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, BaseMoveSpeed * Time.deltaTime);
        }
    }
}
