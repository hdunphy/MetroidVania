using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorSwitchController : MonoBehaviour
{
    [SerializeField] private DoorController Door; //The door this switch controls
    [SerializeField] private SpriteRenderer SpriteRenderer; //Sprite render for the switch
    [SerializeField] private Sprite OffSprite; //Sprite for off state
    [SerializeField] private Sprite OnSprite; //Sprite for on State
    [SerializeField] private Animator Animator; //Animator

    private bool isOpen;
    private SceneObjectState sceneObjectState;

    private void Start()
    {
        isOpen = false;
        OnLoad();
    }

    public void OnLoad()
    {
        SceneData _scene = SaveData.current.GetScene(gameObject.scene.name);
        if (_scene.SceneObjectStates.Any(x => x.guid == Door.guid))
        {
            sceneObjectState = _scene.SceneObjectStates.First(x => x.guid == Door.guid);
            isOpen = sceneObjectState.isOn;
            if (isOpen)
            {
                SetDoorSwitchOnState();
            }
            else
            {
                SetDoorSwitchOffState();
            }
            Door.SetIsOpen(isOpen);
        }
        else
        {
            sceneObjectState = new SceneObjectState { guid = Door.guid, isOn = isOpen };
            _scene.SceneObjectStates.Add(sceneObjectState);
        }
    }

    /// <summary>
    /// Called when switch is damaged by player
    /// </summary>
    public void OnDoorSwitched()
    {
        if (!isOpen)
        { //Don't want to reopen the door. Only open if door is closed
            GetComponent<Damageable>().enabled = false;
            //Animate switch moving to on position
            Animator.enabled = true;
            Door.Open();
        }
    }

    /// <summary>
    /// Called by animator after animation is done
    /// </summary>
    public void SetDoorSwitchOnState()
    {
        isOpen = true;
        Animator.enabled = false;
        SpriteRenderer.sprite = OnSprite;
    }

    public void SetDoorSwitchOffState()
    {
        isOpen = false;
        GetComponent<Damageable>().enabled = true;
        Animator.enabled = false;
        SpriteRenderer.sprite = OffSprite;
    }
}
