using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{
    public GameObject Projectile;
    public float FireDelay;
    private float fireDelaySeconds;
    private bool canFire = true;



    //private override void Start()
    //{
    //    base.Start()
    //    fireDelaySeconds = FireDelay;
    //}

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;

        if(fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = FireDelay;
        }
    }



    public override void CheckDistance()
    {
        if (Vector2.Distance(Target.position, transform.position) <= ChaseRadius && Vector2.Distance(Target.position, transform.position) > AttackRadius)
        {
            if (CurrrentState == EnemyState.idle || CurrrentState == EnemyState.walk && CurrrentState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector2 tempVector = Target.position - transform.position;
                    GameObject tempProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                    tempProjectile.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                }
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeup", true);
            }
        }
        else if (Vector2.Distance(Target.position, transform.position) >= ChaseRadius)
        {
            anim.SetBool("wakeup", false);
        }
    }
}
