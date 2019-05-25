using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{

    public Collider2D Boundry;


    public override void CheckDistance()
    {
        if (Vector2.Distance(Target.position, transform.position) <= ChaseRadius 
            && Vector2.Distance(Target.position, transform.position) > AttackRadius
            && Boundry.bounds.Contains(Target.position))
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
        else if (Vector2.Distance(Target.position, transform.position) >= ChaseRadius || !Boundry.bounds.Contains(Target.position))
        {
            anim.SetBool("wakeup", false);
        }
    }
}
