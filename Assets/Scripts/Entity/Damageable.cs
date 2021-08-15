using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private float KnockbackForce;
    [SerializeField] private float StunDuration;
    [SerializeField] private float InvincibilityAfterDamageDuration;
    [SerializeField] private bool IsInvincible;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    
    [SerializeField, Tooltip("Event that triggers when ever the entity cannot move")] 
    private UnityEvent<bool> SetStun;

    [SerializeField, Tooltip("Event that triggers when Health >= 0")]
    private UnityEvent EntityDied;

    private bool canTakeDamage;

    private void Start()
    {
        canTakeDamage = true;
    }

    public void ApplyDamage(int damage, Vector3 damageDirection)
    {
        if(!IsInvincible && canTakeDamage)
        {
            Health -= damage;
            Vector2 _damageDirection = Vector3.Normalize(transform.position - damageDirection);
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(_damageDirection * KnockbackForce);

            if(Health <= 0)
            {
                EntityDied?.Invoke();
            }
            else
            {
                StartCoroutine(Stun(StunDuration));
                StartCoroutine(MakeInvincible(InvincibilityAfterDamageDuration));
            }
        }
    }

    IEnumerator Stun(float time)
    {
        SetStun?.Invoke(false);
        yield return new WaitForSeconds(time);
        SetStun?.Invoke(true);
    }
    IEnumerator MakeInvincible(float time)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(time);
        canTakeDamage = true;
    }
}
