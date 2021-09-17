using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private LineRenderer Laser; //Line render component for laser
    [SerializeField] private float AttackDuration; //Duration of attack
    [SerializeField] private float LaserDeltaMove; //How fast the laser moves
    [SerializeField] private float Damage; //Damage delt by the laser
    
    private float attackStart;
    private bool isAttacking;
    private Vector2 RayCastHitPos;
    private float lineWidth;

    // Start is called before the first frame update
    void Start()
    {
        lineWidth = Laser.startWidth;
        Laser.enabled = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            attackStart -= Time.deltaTime;
            if(attackStart <= 0)
            {
                StopAttacking();
            }
            else
            {
                Vector2 p1 = Laser.GetPosition(0);
                Vector2 p2 = Laser.GetPosition(1);
                Vector2 midpoint = new Vector2((p2.x - p1.x) / 2, (p2.y - p1.y) / 2) + (Vector2)transform.position;
                float length = Mathf.Abs(p1.y - p2.y);
                var colliders = Physics2D.OverlapBoxAll(midpoint, new Vector2(lineWidth, length), 0);

                foreach(Collider2D col in colliders)
                {
                    if(col.TryGetComponent(out Damageable damageable) && col.gameObject != gameObject)
                    {
                        damageable.ApplyDamage(Damage, midpoint);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Reset laser after attack is finished
    /// </summary>
    private void StopAttacking()
    {
        isAttacking = false;
        Laser.enabled = false;
        RayCastHitPos = Vector2.zero;
        Laser.SetPosition(1, Vector2.zero);
    }

    /// <summary>
    /// When player is in range. Fire the laser
    /// </summary>
    public void Attack()
    {
        attackStart = AttackDuration;
        isAttacking = true;
        Laser.enabled = true;
        Laser.SetPosition(1, Vector3.zero);

        var hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, GameLayers.Singleton.GroundLayer);
        if (hit)
        {
            RayCastHitPos = hit.point - (Vector2)transform.position;
            StartCoroutine(ExtendLaser());

        }
    }

    /// <summary>
    /// Coroutine to dynamically extend the laser towards the ground
    /// </summary>
    /// <returns>null</returns>
    private IEnumerator ExtendLaser()
    {
        Vector2 nextPos = Vector2.zero;

        while(Vector2.Distance(nextPos, RayCastHitPos) > .01f)
        {
            nextPos = Vector2.MoveTowards(Laser.GetPosition(1), RayCastHitPos, LaserDeltaMove);
            Laser.SetPosition(1, nextPos);

            yield return null;
        }
    }
}
