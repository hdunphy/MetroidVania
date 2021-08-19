using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    /*Component that makes entity apply damage when other object collides with it*/

    [SerializeField, Tooltip("Layers that can take damage from this object")] private LayerMask DamageableLayers;
    [SerializeField, Tooltip("Amount of Damage delt on collison")] protected float DamageDelt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isInDamageableLayer = GetIsInDamageableLayer(collision);
        //if other object is damageable layer and has the damageable componenet than apply damage
        if (isInDamageableLayer && collision.TryGetComponent(out Damageable damageable))
        {
            damageable.ApplyDamage(DamageDelt, transform.position);
        }
    }

    /// <summary>
    /// Determins if the other object colliding with this is in the damageable layer
    /// </summary>
    /// <param name="collision">Other object in collision with this collider</param>
    /// <returns>true if is in damageable layer, False if is not in damageable layer</returns>
    protected bool GetIsInDamageableLayer(Collider2D collision)
    {
        //Layers are stored as bits so found this answer on the internet
        bool isInDamageableLayer = ((1 << collision.gameObject.layer) & DamageableLayers) != 0;

        return isInDamageableLayer;
    }
}
