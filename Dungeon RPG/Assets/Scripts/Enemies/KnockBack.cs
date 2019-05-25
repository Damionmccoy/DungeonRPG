using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float Thrust;
    public float KnockTime;
    public float Damage;

    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other.gameObject.CompareTag("Breakable") && gameObject.CompareTag("Player"))
        {
            _other.GetComponent<Pot>().SmashPot();
        }


        if (_other.gameObject.CompareTag("Enemy") || _other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = _other.GetComponent<Rigidbody2D>();

            if(hit != null)
            {

                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * Thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (_other.gameObject.CompareTag("Enemy") && _other.isTrigger)
                {
                    _other.GetComponent<Enemy>().Knock(hit, KnockTime,Damage);
                }
                if (_other.gameObject.CompareTag("Player"))
                {
                    if(_other.GetComponent<Player>().GetPlayerState() != PlayerState.stagger)
                    {
                        _other.GetComponent<PlayerMovement>().Knock(KnockTime,Damage);
                    }
                    
                }
                
            }

        }
    }


    IEnumerator KnockCo(Rigidbody2D _enemy)
    {
        yield return new WaitForSeconds(KnockTime);
        _enemy.velocity = Vector2.zero;
        _enemy.GetComponent<Enemy>().CurrrentState = EnemyState.idle;
    }
}
