using System.Linq;
using UnityEngine;

public class DoorSwitchController : SceneObjectState
{
    [SerializeField] private DoorController Door; //The door this switch controls
    [SerializeField] private SpriteRenderer SpriteRenderer; //Sprite render for the switch
    [SerializeField] private Sprite OffSprite; //Sprite for off state
    [SerializeField] private Sprite OnSprite; //Sprite for on State
    [SerializeField] private Animator Animator; //Animator

    private void Start()
    {
        SceneObjectData = new SceneObjectData { guid = GUID, isOn = false };
        OnLoad();
    }

    /// <summary>
    /// Called when switch is damaged by player
    /// </summary>
    public void OnDoorSwitched()
    {
        if (!SceneObjectData.isOn)
        { //Don't want to reopen the door. Only open if door is closed
            GetComponent<Damageable>().enabled = false;
            //Animate switch moving to on position
            Animator.enabled = true;
            Door.Open();

            SceneObjectData.isOn = true;
            OnSave();
        }
    }

    /// <summary>
    /// Called by animator after animation is done
    /// </summary>
    public void SetDoorSwitchOnState()
    {
        Animator.enabled = false;
        SpriteRenderer.sprite = OnSprite;
    }

    /// <summary>
    /// Called to turn the switch to off
    /// </summary>
    public void SetDoorSwitchOffState()
    {
        GetComponent<Damageable>().enabled = true;
        Animator.enabled = false;
        SpriteRenderer.sprite = OffSprite;
    }

    public override void AfterLoad()
    {
        if (SceneObjectData.isOn)
        {
            SetDoorSwitchOnState();
        }
        else
        {
            SetDoorSwitchOffState();
        }
        //If isOn then door is open and should not be visible
        //Else IsNotOn then door is closed and is visible
        Door.gameObject.SetActive(!SceneObjectData.isOn); 
    }

    public override void AfterSave()
    {
        //Nothing in this case
    }
}
