using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform shotInitialPosition;

    private Vector2 direction; //will need this if we can shoot up
    public Transform ShotInitialPosition { get => shotInitialPosition; }

    private void Start()
    {
        direction = Vector2.right;
    }

    public Vector2 GetDirection()
    {
        return new Vector2(transform.localScale.x * direction.x, direction.y);
    }
}
