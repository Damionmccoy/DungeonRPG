using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    public Transform[] Path;
    public int CurrentPoint;
    public Transform CurrentGoal;
    public float RoundingDistance;
    /// <summary>
    /// This is an override of the log class's check distance.
    /// </summary>
    public override void CheckDistance()
    {
        if (Vector2.Distance(Target.position, transform.position) <= ChaseRadius && Vector2.Distance(Target.position, transform.position) > AttackRadius)
        {
            if (CurrrentState == EnemyState.idle || CurrrentState == EnemyState.walk && CurrrentState != EnemyState.stagger)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, Target.position, BaseMoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - new Vector2(transform.position.x, transform.position.y));
                rb.MovePosition(temp);

                //ChangeState(EnemyState.walk);
                anim.SetBool("wakeup", true);
            }
        }
        else if (Vector2.Distance(Target.position, transform.position) >= ChaseRadius)
        {

            if (Vector2.Distance(transform.position, Path[CurrentPoint].position) > RoundingDistance)
            {

                Vector2 temp = Vector2.MoveTowards(transform.position, Path[CurrentPoint].position, BaseMoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - new Vector2(transform.position.x, transform.position.y));
                rb.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if(CurrentPoint == Path.Length - 1)
        {
            CurrentPoint = 0;
            CurrentGoal = Path[0];
        }
        else
        {
            CurrentPoint++;
            CurrentGoal = Path[CurrentPoint];
        }
    }
}
