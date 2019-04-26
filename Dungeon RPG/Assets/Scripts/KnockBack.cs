using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float Thrust;
    public float KnockTime;
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = _other.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
                
                StartCoroutine(KnockCo(enemy));
            }
            else
            {
                Debug.Log("no enemy");
            }
        }
    }

    IEnumerator KnockCo(Rigidbody2D _enemy)
    {
        _enemy.isKinematic = false;
        Vector2 difference = _enemy.transform.position - transform.position;
        difference = difference.normalized * Thrust;
        _enemy.AddForce(difference, ForceMode2D.Impulse);
        Debug.Log("before co");
        yield return new WaitForSeconds(KnockTime);
        Debug.Log("after co");
        _enemy.velocity = Vector2.zero;
        _enemy.isKinematic = true;
    }
}
