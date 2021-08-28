using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchController : MonoBehaviour
{
    [SerializeField] private DoorController Door; //The door this switch controls
    [SerializeField] private SpriteRenderer SpriteRenderer; //Sprite render for the switch
    [SerializeField] private Sprite OffSprite; //Sprite for off state
    [SerializeField] private Sprite OnSprite; //Sprite for on State
    [SerializeField] private Animator Animator; //Animator

    /// <summary>
    /// Called when switch is damaged by player
    /// </summary>
    public void OnDoorSwitched()
    {
        FindObjectOfType<Damageable>().enabled = false;
        //Animate switch moving to on position
        Animator.enabled = true;
        Door.Open();
    }

    /// <summary>
    /// Called by animator after animation is done
    /// </summary>
    public void SetDoorSwitchOnState()
    {
        Animator.enabled = false;
        SpriteRenderer.sprite = OnSprite;
    }
}
