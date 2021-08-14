using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable/Abilities/Dash")]
public class Dash : Ability
{
    [SerializeField] private float DashForce;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Dashing in activate");
        Transform transform = parent.transform;
        Rigidbody2D m_Rigidbody2D = parent.GetComponent<Rigidbody2D>();

        m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * DashForce, 0);

        HasUse = false;
    }

    public override void BeginCooldown(GameObject parent)
    {
        //Transform transform = parent.transform;
        //Rigidbody2D m_Rigidbody2D = parent.GetComponent<Rigidbody2D>();

        //m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * DashForce, 0);
    }
}
