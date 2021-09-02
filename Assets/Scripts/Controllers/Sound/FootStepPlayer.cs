using UnityEngine;

public class FootStepPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip FootStepClip;
    [SerializeField] private AudioSource FootStepSource;
    [SerializeField] private float SecondsBetweenSteps;

    private bool IsGrounded;
    private bool IsMoving;
    private float LastTimePlayed;

    private void Start()
    {
        IsGrounded = true;
        IsMoving = false;
    }

    private void Update()
    {
        if (IsGrounded && IsMoving)
        {
            if (Time.time > LastTimePlayed + SecondsBetweenSteps)
            {
                LastTimePlayed = Time.time;
                FootStepSource.PlayOneShot(FootStepClip);
            }
        }
    }

    /// <summary>
    /// Play the footstep audio clip if the entity is grounded and moving
    /// </summary>
    private void CheckPlayFootsteps()
    {
        //if(IsGrounded && IsMoving)
        //{
        //    FootStepSource.Play();
        //    //AudioSource.PlayClipAtPoint(FootStepClip, transform.position);
        //}
        //else
        //{
        //    FootStepSource.Stop();
        //}
    }

    /// <summary>
    /// If the parameter does not equal the IsMoving property than update it and check if we can play the footstep clip
    /// </summary>
    /// <param name="_isMoving">true if entity is moving, false if not moving</param>
    public void SetIsMoving(bool _isMoving)
    {
        if(_isMoving != IsMoving)
        {
            IsMoving = _isMoving;
            CheckPlayFootsteps();
        }
    }


    /// <summary>
    /// If the parameter does not equal the IsGrounded property than update it and check if we can play the footstep clip
    /// </summary>
    /// <param name="_isGrounded">true if entity is grounded, false if airborne</param>
    public void SetIsGrounded(bool _isGrounded)
    {
        if (_isGrounded != IsGrounded)
        {
            IsGrounded = _isGrounded;
            CheckPlayFootsteps();
        }
    }

    /// <summary>
    /// Sometimes easier to use event is airborne. Call set is grounded but take the inverse of the isAirborne parameter
    /// </summary>
    /// <param name="_isAirborne">true if entity is in the air, false if the entity is grounded</param>
    public void SetIsAirborn(bool _isAirborne) { SetIsGrounded(!_isAirborne); }
}
