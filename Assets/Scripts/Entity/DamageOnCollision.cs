using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private LayerMask DamageableLayers;
    [SerializeField] private int DamageDelt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Layers are stored as bits so found this answer on the internet
        bool isInDamageableLayer = ((1 << collision.gameObject.layer) & DamageableLayers) != 0;

        if (isInDamageableLayer && collision.TryGetComponent(out Damageable damageable))
        {
            damageable.ApplyDamage(DamageDelt, transform.position);
        }
    }
}
