using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamageOnTrigger : DamageOnHit
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isInDamageableLayer = GameLayers.Singleton.IsLayerInLayerMask(collision.gameObject.layer, DamageableLayers);

        if (isInDamageableLayer && collision.TryGetComponent(out Damageable damageable))
        {
            Debug.Log($"{collision.name} fell into hazard");
            damageable.ApplyDamage(DamageDelt, transform.position);

            if (damageable.currentHealth > 0)
            { //if health is <= 0 than apply damage calls KillEntity()
                damageable.KillEntity();
            }
        }
    }
}
