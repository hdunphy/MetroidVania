using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : SceneObjectState
{
    [SerializeField] private SpriteRenderer SpriteRenderer; //Sprite render for the switch
    [SerializeField] private Sprite OffSprite; //Sprite for off state
    [SerializeField] private Sprite OnSprite; //Sprite for on State
    [SerializeField] private Animator Animator; //Animator
    [SerializeField] private UnityEvent<bool> OnUpdateState;

    private void Start()
    {
        SceneObjectData = new SceneObjectData { guid = GUID, isOn = false };
        OnLoad();
    }

    /// <summary>
    /// Called when switch is damaged by player
    /// </summary>
    public void SwitchOn()
    {
        if (!SceneObjectData.isOn)
        { //Don't want to reopen the door. Only open if door is closed
            GetComponent<Damageable>().enabled = false;
            //Animate switch moving to on position
            Animator.enabled = true;
            OnUpdateState?.Invoke(true);

            SceneObjectData.isOn = true;
            OnUpdateState(gameObject.scene.name);
        }
    }

    /// <summary>
    /// Called by animator after animation is done
    /// </summary>
    public void SetSwitchOnState()
    {
        Animator.enabled = false;
        SpriteRenderer.sprite = OnSprite;
    }

    /// <summary>
    /// Called to turn the switch to off
    /// </summary>
    public void SetSwitchOffState()
    {
        GetComponent<Damageable>().enabled = true;
        Animator.enabled = false;
        SpriteRenderer.sprite = OffSprite;

    }

    public override void AfterLoad()
    {
        if (SceneObjectData.isOn)
        {
            SetSwitchOnState();
        }
        else
        {
            SetSwitchOffState();
        }
    }

    public override void AfterUpdate()
    {
        //Nothing in this case
    }
}
