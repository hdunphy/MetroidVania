using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupLoader : SceneObjectState
{
    [SerializeField] private AbilityPickup AbilityPickup;

    //Need this class as a container for ability pickup so when game is loading, it can find the object
    private void Start()
    {
        SceneObjectData = new SceneObjectData { guid = GUID, isOn = true }; //isOn is if the object has not been pickedup yet
        OnLoad();
    }

    /// <summary>
    /// Called by unity event from AbilityPickup when the ability has been picked up by the player
    /// </summary>
    public void OnPickupEvent()
    {
        SceneObjectData.isOn = false; //Object is picked up so no longer on
        OnUpdateState(gameObject.scene.name);
    }

    public override void AfterLoad()
    {
        UpdateAbilityPickup();
    }

    public override void AfterUpdate()
    {
        UpdateAbilityPickup();
    }

    /// <summary>
    /// Sets the abilityPickup object to active or not depening on SceneObjectData.IsOn
    ///     If object is picked up IsOn is false.
    ///     If object is not picked up IsOn is true.
    /// </summary>
    private void UpdateAbilityPickup()
    {
        AbilityPickup.gameObject.SetActive(SceneObjectData.isOn); 
    }
}
