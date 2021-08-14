﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;

	[SerializeField] private LayerMask DamageableLayerMask;
	[SerializeField] private float DamageAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if ( !hasHit)
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == DamageableLayerMask)
		{
			if (collision.gameObject.TryGetComponent(out Damageable _damageable))
			{
				_damageable.ApplyDamage(DamageAmount, transform.position);
				Destroy(gameObject);
			}
		}
		//if (collision.gameObject.tag == "Enemy")
		//{
		//	collision.gameObject.SendMessage("ApplyDamage", Mathf.Sign(direction.x) * 2f);
		//	Destroy(gameObject);
		//}
		//else if (collision.gameObject.tag != "Player")
		//{
		//	Destroy(gameObject);
		//}
	}
}
