using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Animator animator;

    [SerializeField] float strafing = 0f;

    int isGoundedParam, isJumpingParam, isMovingParam, isWalkingParam, moveSpeedParam, isStopParam, isStrafingParam, currentGaitParam, fallingDurationParam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        isStopParam = Animator.StringToHash("IsStopped");
        isGoundedParam = Animator.StringToHash("IsGrounded");
        isJumpingParam = Animator.StringToHash("IsJumping");
        isMovingParam = Animator.StringToHash("MovementInputHeld");
        isWalkingParam = Animator.StringToHash("IsWalking");
        moveSpeedParam = Animator.StringToHash("MoveSpeed");
        isStrafingParam = Animator.StringToHash("IsStrafing");
        currentGaitParam = Animator.StringToHash("CurrentGait");
        fallingDurationParam = Animator.StringToHash("FallingDuration");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVelocity = playerMovement.velocity;
        float moveSpeed = Mathf.Sqrt(playerVelocity.x * playerVelocity.x + playerVelocity.z * playerVelocity.z) * 0.5f;

        // Set Animator Param
        animator.SetBool(isGoundedParam, playerMovement._AniGrounded);
        animator.SetBool(isJumpingParam, playerMovement._Jumping);
        animator.SetBool(isMovingParam, playerMovement._Moving);
        animator.SetBool(isWalkingParam, playerMovement._Walk);
        animator.SetBool(isStopParam, playerMovement._Stop);
        animator.SetFloat(moveSpeedParam, moveSpeed);
        animator.SetFloat(isStrafingParam, strafing);
               
        // Different gait animation at different move speed
        if (moveSpeed <= 6f && playerMovement._Moving)
        {
            animator.SetInteger(currentGaitParam, 0);
        }
        else if (moveSpeed <= 7f && playerMovement._Moving)
        {
            animator.SetInteger(currentGaitParam, 1);
        }
        else if (moveSpeed <= 8f && playerMovement._Moving)
        {
            animator.SetInteger(currentGaitParam, 2);
        }
        else if (moveSpeed > 8f && playerMovement._Moving)
        {
            animator.SetInteger(currentGaitParam, 3);
        }
        if (!playerMovement._Moving)
        {
            animator.SetInteger(currentGaitParam, 0);
        }

        // Different fall animation depends on the Player falling time
        animator.SetFloat(fallingDurationParam, playerMovement.fallTime);


    }
}
