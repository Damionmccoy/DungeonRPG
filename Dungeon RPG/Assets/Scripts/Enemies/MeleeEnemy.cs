using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{


    public override void CheckDistance()
    {
        if (Vector2.Distance(Target.position, transform.position) <= ChaseRadius && Vector2.Distance(Target.position, transform.position) > AttackRadius)
        {
            if (CurrrentState == EnemyState.idle || CurrrentState == EnemyState.walk && CurrrentState != EnemyState.stagger)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, Target.position, BaseMoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - new Vector2(transform.position.x, transform.position.y));
                rb.MovePosition(temp);

                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector2.Distance(Target.position, transform.position) <= ChaseRadius && Vector2.Distance(Target.position, transform.position) <= AttackRadius)
        {
            if ( CurrrentState == EnemyState.walk && CurrrentState != EnemyState.stagger)
                StartCoroutine(AttackCo());
        }


    }

    public IEnumerator AttackCo()
    {
        CurrrentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        CurrrentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
}
