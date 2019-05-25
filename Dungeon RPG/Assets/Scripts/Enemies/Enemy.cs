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
    [Header("Enemy Base",order =0)]
    [Header("Base Values",order =1)]
    public float RuntimeHealth;
    public string EnemyName;
    public int BaseAttack;
    public float BaseMoveSpeed;

    [Header("Current State",order =1)]
    public EnemyState CurrrentState;
    [Header("Defualt States",order = 0)]
    public FloatValue BaseMaxHealth;

    [Header("Death Effects and signals")]
    public GameObject DeathEffectObject;
    public Signal roomSignal;
    private Vector2 HomePosition;
    public LootTable EnemyLoot;


    private void Awake()
    {
        RuntimeHealth = BaseMaxHealth.InitialValue;
        HomePosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = HomePosition;
        RuntimeHealth = BaseMaxHealth.InitialValue;
        CurrrentState = EnemyState.idle;
    }

    private void DeathEffect()
    {
        if(DeathEffectObject != null)
        {
            GameObject effect = Instantiate(DeathEffectObject, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
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
        RuntimeHealth -= _damage;

        if(RuntimeHealth <= 0) //happens if the player is dead
        {
            DeathEffect();
            DropLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            gameObject.SetActive(false);
        }
    }

    private void DropLoot()
    {
        if(EnemyLoot != null)
        {
            Powerup current = EnemyLoot.GetLoot();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
