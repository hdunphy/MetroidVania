using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : DamageOnHit
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit(collision.collider.gameObject);
    }
}
