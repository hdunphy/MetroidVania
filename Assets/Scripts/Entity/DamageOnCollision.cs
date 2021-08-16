using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    /*Component that makes entity apply damage when other object collides with it*/

    [SerializeField, Tooltip("Layers that can take damage from this object")] private LayerMask DamageableLayers;
    [SerializeField, Tooltip("Amount of Damage delt on collison")] private float DamageDelt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Layers are stored as bits so found this answer on the internet
        bool isInDamageableLayer = ((1 << collision.gameObject.layer) & DamageableLayers) != 0;

        //if other object is damageable layer and has the damageable componenet than apply damage
        if (isInDamageableLayer && collision.TryGetComponent(out Damageable damageable))
        {
            damageable.ApplyDamage(DamageDelt, transform.position);
        }
    }
}
