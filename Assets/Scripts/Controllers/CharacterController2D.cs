using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(EntityMovement))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private bool HasAirControl;  //True if player can control movement in the air

    [SerializeField] private Transform GroundCheck; //Get position of the players feet for checking if in contact with the ground
    private LayerMask GroundLayer; // which Object layers should be checked for a 'Ground' object

    //Implement if we want wall sliding
    //[SerializeField] private Transform WallCheck;
    //[SerializeField] private LayerMask WallLayer;

    [SerializeField] private List<Ability> StartingAbilities; //List of abilities character can start with

    [Header("Events")] //Triggerable events that can be assigned from the inspector
    [SerializeField] private UnityEvent OnLandEvent; //When the player transitions to the ground state, trigger this event

    private EntityMovement Movement; //Component for handling movement abilities

    private CharacterState CurrentCharacterState; //Current character state

    private AbilityController AbilityController; //Keep track of all character  abilities
    private Dictionary<CharacterState, ICharacterState> CharacterStateMachine; //Keep track of current character state

    //Constants
    private const float k_GroundRadiusCheck = 0.2f; //Radius for checking if character  is touching the ground
    //private const float k_WallRadiusCheck = 0.2f; //Radius for checking if character  is touching the wall

    private void Start()
    {
        GroundLayer = GameLayers.Singleton.GroundLayer;

        Movement = GetComponent<EntityMovement>(); //store movement for character states. Needs this component on the same game object

        AbilityController = new AbilityController(StartingAbilities, gameObject); //Ability controller which stores all character abilities

        //Build character state machine
        CharacterStateMachine = new Dictionary<CharacterState, ICharacterState>()
        {
            { CharacterState.Gounded, new GroundedState(AbilityController, Movement) }, //State for when character's feet are colliding with ground obejct
            { CharacterState.Airborn, new AirbornState(AbilityController, HasAirControl, Movement) } //state for when player is in the air
        };
    }

    private void Update()
    {
        CharacterStateMachine[CurrentCharacterState].Update(); //Call update method on Character state machine

        AbilityController.Update(Time.deltaTime); //Call update method on Ability controller
    }

    //Fixed update is used for physics calculations
    private void FixedUpdate()
    { 
        CharacterState previousCharacterState = CurrentCharacterState; //store state from last fixed update

        if (Physics2D.OverlapCircle(GroundCheck.position, k_GroundRadiusCheck, GroundLayer))
        { // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            CurrentCharacterState = CharacterState.Gounded;
        }
        /* Not needed yet
        else if (Physics2D.OverlapCircle(WallCheck.position, k_WallRadiusCheck, WallLayer))
        { // The player is wall sliding if a circlecast to the wall check position hits anything designated as wall
            CurrentCharacterState = CharacterState.WallSliding;
        }
        */
        else
        { //Else the player is air born
            CurrentCharacterState = CharacterState.Airborn;
        }

        //Entering a new state
        if (previousCharacterState != CurrentCharacterState)
        {
            CharacterStateMachine[CurrentCharacterState].EnterState();
        }
    }

    /// <summary>
    /// Used to set the Character's movement direction
    /// </summary>
    /// <param name="moveVector"> Takes a 2d Vector, but only needs the x component for horizontal movment</param>
    public void SetMove(Vector2 moveVector)
    {
        Movement.SetMoveDirection(moveVector.x);
    }

    /// <summary>
    /// Trigger a charcters ability stored in the ability controller
    /// </summary>
    /// <param name="abilityType">The type of ability to trigger</param>
    /// <param name="isButtonPressed">If the ability should be turned on or off</param>
    public void TriggerAbility(AbilityEnum abilityType, bool isButtonPressed)
    {
        AbilityController.TriggerAbility(abilityType, isButtonPressed);
    }

    /// <summary>
    /// Add a new ability to the ability controller
    /// </summary>
    /// <param name="_ability">The ability object to be added to the ability controller</param>
    public void AddAbility(Ability _ability)
    {
        AbilityController.AddAbility(_ability, gameObject);
    }

    /// <summary>
    /// Function called by UnityEvent to trigger this character's death
    ///     Will trigger any clean up, animations, and events that need to occur after this character dies
    /// </summary>
    public void OnDeath()
    {
        Debug.Log("Character is Dead");

        StartCoroutine(CharacterDeath());
    }

    /// <summary>
    /// Coroutine for character death so some things are not instantanious like removing the object
    /// </summary>
    /// <returns></returns>
    IEnumerator CharacterDeath()
    {
        Movement.SetCanMove(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
