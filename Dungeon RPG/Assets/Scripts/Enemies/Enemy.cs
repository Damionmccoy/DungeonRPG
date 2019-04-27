using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{

    public float BaseHealth;
    public string EnemyName;
    public int BaseAttack;
    public float BaseMoveSpeed;
    public EnemyState CurrrentState;
    public FloatValue BaseMaxHealth;


    private void Awake()
    {
        BaseHealth = BaseMaxHealth.InitialValue;
    }


    /// <summary>
    /// This is used for a knockback effect
    /// </summary>
    /// <param name="_rb">the rigidbody being effected</param>
    /// <param name="_knockTime"> how long the effect should last</param>
    /// <returns></returns>
    IEnumerator KnockCo(Rigidbody2D _rb,float _knockTime)
    {
        //_rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (_rb != null )
        {
            
            ChangeState(EnemyState.stagger);

            yield return new WaitForSeconds(_knockTime);
            _rb.velocity = Vector2.zero;
            CurrrentState = EnemyState.idle;
        }
        else
        {
            Debug.Log("Enemy KnockCo failed because of missing rigidbody");
        }
    }

    public void Knock(Rigidbody2D _rb, float _knocktime,float _damage)
    {
        StartCoroutine(KnockCo(_rb, _knocktime));
        TakeDamage(_damage);
    }

    public void ChangeState(EnemyState _newState)
    {
        if (CurrrentState != _newState)
        {
            CurrrentState = _newState;
        }
    }

    private void TakeDamage(float _damage)
    {
        BaseHealth -= _damage;
        if(BaseHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
