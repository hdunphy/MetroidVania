using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float TotalHealth; //total health of the entity
    [SerializeField] private float KnockbackForce; //Amount of knockback entity takes when damaged
    [SerializeField] private float StunDuration; //Duration of stun entity takes when damaged
    [SerializeField] private float InvincibilityAfterDamageDuration; //Duration of invincibility entity takes before can take additional damage
    [SerializeField] private bool IsInvincible; //Mostly for debugging, if true entity cannot take damage
    [SerializeField] private Rigidbody2D m_Rigidbody2D; //RigidBody2D attached to this game object

    [SerializeField, Tooltip("Event that triggers when ever the entity cannot move")]
    private UnityEvent<bool> SetStun;

    [SerializeField, Tooltip("Event that triggers when Health >= 0")]
    private UnityEvent EntityDied;

    [SerializeField, Tooltip("Event that triggers when Apply Damage is called")]
    private UnityEvent TakeDamage;

    public float currentHealth { get; private set; } //Current health of the entity

    public float GetHealthPercent => currentHealth / TotalHealth;
    private bool canTakeDamage; //Check if entity can take damage this frame

    private void Start()
    {
        canTakeDamage = true;
        currentHealth = TotalHealth;
    }

    /// <summary>
    /// Apply damage to this entity with knockback
    /// </summary>
    /// <param name="damage">amount of damage taken</param>
    /// <param name="damagePosition">the position the damage is coming from</param>
    public void ApplyDamage(float damage, Vector3 damagePosition)
    {
        if (!IsInvincible && canTakeDamage)
        { //If the entity is not invincible and can take damage
            currentHealth -= damage;
            TakeDamage?.Invoke();

            Vector2 _damageDirection = Vector3.Normalize(transform.position - damagePosition); //Find the direction the damge came from

            if (m_Rigidbody2D != null)
            {
                m_Rigidbody2D.velocity = Vector2.zero; //stop entity movement
                m_Rigidbody2D.AddForce(_damageDirection * KnockbackForce); //Add knockback force
            }

            if (currentHealth <= 0)
            { //Check if the entity is dead. If so trigger Died UnityEvent
                KillEntity();
            }
            else
            { //Entity is still alive, Stun the entity and make temprorily invincible
                StartCoroutine(Stun(StunDuration));
                StartCoroutine(MakeInvincible(InvincibilityAfterDamageDuration));
            }
        }
    }

    public void Heal(float addedHealth)
    {
        currentHealth = currentHealth += addedHealth > TotalHealth ? TotalHealth : currentHealth;
    }

    public void SetFullHealth()
    {
        currentHealth = TotalHealth;
    }

    /// <summary>
    /// Kill Entity
    /// </summary>
    public void KillEntity()
    {
        EntityDied?.Invoke();
    }

    /// <summary>
    /// Coroutine to Stun the entity for x amount of time
    /// </summary>
    /// <param name="time">Time that the stun will last</param>
    /// <returns></returns>
    IEnumerator Stun(float time)
    {
        SetStun?.Invoke(false);
        yield return new WaitForSeconds(time);
        SetStun?.Invoke(true);
    }

    /// <summary>
    /// Coroutine to make the entity unhittable for duration
    /// </summary>
    /// <param name="time">duration of entity being temporarily invincible</param>
    /// <returns></returns>
    IEnumerator MakeInvincible(float time)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(time);
        canTakeDamage = true;
    }
}
