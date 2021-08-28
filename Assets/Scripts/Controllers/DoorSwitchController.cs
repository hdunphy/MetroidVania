using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchController : MonoBehaviour
{
    [SerializeField] private DoorController Door;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite OffSprite;
    [SerializeField] private Sprite OnSprite;
    [SerializeField] private Animator Animator;

    public void OnDoorSwitched()
    {
        FindObjectOfType<Damageable>().enabled = false;
        //Animate switch moving to on position
        Animator.enabled = true;
        Door.Open();
    }

    public void SetDoorSwitchOnState()
    {
        Animator.enabled = false;
        SpriteRenderer.sprite = OnSprite;
    }
}
