using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//Keeps track of character states and enables/disables abilities
[RequireComponent(typeof(EntityMovement))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private bool HasAirControl;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;

    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask WallLayer;

    [SerializeField] private List<Ability> StartingAbilities; //List of abilities character can start with

    [Header("Events")]
    [SerializeField] private UnityEvent OnLandEvent;

    private EntityMovement Movement;

    private CharacterState CurrentCharacterState; //Current character state

    private AbilityController AbilityController; //Keep track of all character  abilities
    private Dictionary<CharacterState, ICharacterState> CharacterStateMachine; //Keep track of current character  state

    //Constants
    private const float k_GroundRadiusCheck = 0.2f; //Radius for checking if character  is touching the ground
    private const float k_WallRadiusCheck = 0.2f; //Radius for checking if character  is touching the wall

    private void Start()
    {
        Movement = GetComponent<EntityMovement>(); //store movement for character states

        AbilityController = new AbilityController(StartingAbilities, gameObject);

        //Build character state machine
        CharacterStateMachine = new Dictionary<CharacterState, ICharacterState>()
        {
            { CharacterState.Gounded, new GroundedState(AbilityController, Movement) },
            { CharacterState.Airborn, new AirbornState(AbilityController, HasAirControl, Movement) }
        };
    }

    private void Update()
    {
        CharacterStateMachine[CurrentCharacterState].Update();

        //Not sure this is best way
        AbilityController.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        CharacterState previousCharacterState = CurrentCharacterState;

        if (Physics2D.OverlapCircle(GroundCheck.position, k_GroundRadiusCheck, GroundLayer))
        { // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            CurrentCharacterState = CharacterState.Gounded;
        }
        else if (Physics2D.OverlapCircle(WallCheck.position, k_WallRadiusCheck, WallLayer))
        { // The player is wall sliding if a circlecast to the wall check position hits anything designated as wall
            CurrentCharacterState = CharacterState.WallSliding;
        }
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

    public void SetMove(Vector2 moveVector)
    {
        Movement.SetMoveDirection(moveVector.x);
    }

    public void TriggerAbility(AbilityEnum abilityType, bool isButtonPressed)
    {
        AbilityController.TriggerAbility(abilityType, isButtonPressed);
    }

    public void AddAbility(Ability _ability)
    {
        AbilityController.AddAbility(_ability, gameObject);
    }

    public void OnDeath()
    {
        Debug.Log("Character is Dead");

        StartCoroutine(CharacterDeath());
    }

    IEnumerator CharacterDeath()
    {
        Movement.SetCanMove(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
