using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageOnHit : MonoBehaviour
{
    /*Component that makes entity apply damage when other object collides with it*/

    [SerializeField, Tooltip("Layers that can take damage from this object")] protected LayerMask DamageableLayers;
    [SerializeField, Tooltip("Amount of Damage delt on collison")] protected float DamageDelt;

    protected void OnHit(GameObject other)
    {
        bool isInDamageableLayer = GameLayers.Singleton.IsLayerInLayerMask(other.layer, DamageableLayers);
        //if other object is damageable layer and has the damageable componenet than apply damage
        if (isInDamageableLayer && other.TryGetComponent(out Damageable damageable))
        {
            damageable.ApplyDamage(DamageDelt, transform.position);
        }
    }
}
