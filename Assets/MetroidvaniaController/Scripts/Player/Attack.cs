using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackCheck;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;
	public float projectileCooldown;

	public GameObject cam;
	[SerializeField] private LayerMask EntityLayer;

	private bool isAttacking, isFiring;
	private float nextProjectileTime;

    private void Start()
    {
		isAttacking = isFiring = false; //initialize as false
		nextProjectileTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (isAttacking && canAttack)
		{
			canAttack = false;
			animator.SetBool("IsAttacking", true);
			StartCoroutine(AttackCooldown());
		}

		if (isFiring && Time.time > nextProjectileTime)
		{
			nextProjectileTime = Time.time + projectileCooldown;
			GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f), Quaternion.identity) as GameObject; 
			Vector2 direction = new Vector2(transform.localScale.x, 0);
			throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
			throwableWeapon.name = "ThrowableWeapon";
		}
	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.25f);
		canAttack = true;
	}

	public void DoDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);

		var _collider = Physics2D.OverlapCircle(attackCheck.position, 0.9f, EntityLayer);
		if(_collider && _collider.TryGetComponent(out Damageable damageable))
        {
			damageable.ApplyDamage(dmgValue, attackCheck.position);

			if(_collider.TryGetComponent(out CameraFollow cam))
            {
				cam.ShakeCamera();
            }
        }
		//Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f, EntityLayer);
		//for (int i = 0; i < collidersEnemies.Length; i++)
		//{
		//	if (collidersEnemies[i].gameObject.tag == "Enemy")
		//	{
		//		if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
		//		{
		//			dmgValue = -dmgValue;
		//		}
		//		collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
		//		cam.GetComponent<CameraFollow>().ShakeCamera();
		//	}
		//}
	}

	public void SetIsAttacking(bool _isAttacking) { isAttacking = _isAttacking; }
	public void SetIsFiring(bool _isFiring) { isFiring = _isFiring; }
}
