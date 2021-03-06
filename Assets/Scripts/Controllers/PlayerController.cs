using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EntityMovementHorizontal Movement;
    [SerializeField] private CharacterController2D CharacterController2D;
    [SerializeField] private Damageable Damageable;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private UnityEvent OnDeathEvent;

    private IPlayerInteractable interactableObject;

    private bool hasLoaded = false; //used when opening a scene without loading in first

    private Vector3 respawnPosition;

    private void Start()
    {
        //Connect main camera to player
        if(Camera.main.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.SetTarget(transform);
        }

        CharacterController2D.UpdateAbilityList += CharacterController2D_UpdateAbilityList;

        if (!hasLoaded)
        {
            Vector3 startPos = GameSceneController.Singleton != null ? GameSceneController.Singleton._startPosition : transform.position;
            OnLoad(startPos);
        }

        respawnPosition = transform.position;
    }

    private void OnDestroy()
    {
        CharacterController2D.UpdateAbilityList -= CharacterController2D_UpdateAbilityList;
    }

    private void CharacterController2D_UpdateAbilityList()
    {
        SaveData.current.PlayerHeldAbilityIds = CharacterController2D.GetAbilityList();
    }

    public void SetRespawnPoint(Vector3 _respawnPosition)
    {
        respawnPosition = _respawnPosition;
    }

    /// <summary>
    /// Function called by UnityEvent to trigger this character's death
    ///     Will trigger any clean up, animations, and events that need to occur after this character dies
    /// </summary>
    public void OnDeath()
    {
        if(Damageable.currentHealth <= 0)
        {
            Debug.Log("Character is Dead");

            Movement.SetCanMove(false);
            Damageable.enabled = false;
            OnDeathEvent?.Invoke();
        }
        else
        {
            transform.position = respawnPosition;
        }
    }

    /// <summary>
    /// Function character death so some things are not instantanious like removing the object
    ///     Gets called by Death Animation Event
    /// </summary>
    public void CharacterDeath()
    {
        GameSceneController.Singleton.LoadLastSave(this);
    }

    public void SetHUDHealth()
    {
        HUDController.Singleton.SetHealthBarPercent(Damageable.GetHealthPercent);
    }

    /// <summary>
    /// Leaving a room into another scene
    ///     Need to stop all movement
    ///     Block inputs so no abilities
    /// </summary>
    public void LeaveRoom()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        Movement.enabled = false; //Disable movement so player cannot move until scene is done loading
    }

    /// <summary>
    /// Move player to new room
    /// </summary>
    /// <param name="loadPosition">The Vector3 for where the player should move to in new room</param>
    public void EnterRoom(Vector3 loadPosition)
    {
        transform.position = loadPosition; //Move player to position
        Camera.main.transform.position = new Vector3(loadPosition.x, loadPosition.y, Camera.main.transform.position.z); //Move camera to position
        Movement.enabled = true; //re-enable player movement
    }

    /// <summary>
    /// When a saved game is loaded, call this function to restore player to last loaded state
    /// </summary>
    /// <param name="loadPosition">position player will load in at</param>
    public void OnLoad(Vector3 loadPosition)
    {
        hasLoaded = true;
        m_Rigidbody2D.velocity = Vector2.zero;
        List<Ability> startingAbilities = PlayerAbilityManager.Singleton.GetAbilitiesByIds(SaveData.current.PlayerHeldAbilityIds);
        CharacterController2D.SetCharacterAbilities(startingAbilities);
        HUDController.Singleton.SetCurrentPercent(Damageable.GetHealthPercent);
        EnterRoom(loadPosition);
    }

    public void AddAbility(Ability ability)
    {
        CharacterController2D.AddAbility(ability);
    }

    public void Heal(float healthAmount)
    {
        Damageable.Heal(healthAmount);
    }

    public void SetFullHealth()
    {
        Damageable.SetFullHealth();
        SetHUDHealth();
    }

    public void OnPlayerInteraction()
    {
        interactableObject?.Interact(this);
    }

    public void OnPlayerInteractionComplete()
    {
        //Change player state to not interacting
    }

    /// <summary>
    /// Set the trigger object for when player pressed interactable button
    /// </summary>
    /// <param name="_triggerObject">Object to trigger on interaction</param>
    public void SetTriggerObject(IPlayerInteractable _triggerObject)
    {
        interactableObject = _triggerObject;
    }
}
