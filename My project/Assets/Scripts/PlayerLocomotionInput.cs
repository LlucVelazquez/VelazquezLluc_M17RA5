using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput : MonoBehaviour, InputSystem_Actions.IPlayerLocomotionMapActions
{
    public InputSystem_Actions PlayerControls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    private Animator animator;
    private float XDirection = 0f;
    private float YDirection = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerControls = new InputSystem_Actions();
        PlayerControls.Enable();

        PlayerControls.PlayerLocomotionMap.Enable();
        PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
    }
    private void OnDisable()
    {
        PlayerControls.PlayerLocomotionMap.Disable();
        PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
    }

    public void OnNewaction(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        print(MovementInput);
        XDirection = context.ReadValue<Vector2>().x;
        YDirection = context.ReadValue<Vector2>().y;
        if (XDirection != 0f || YDirection != 0f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

    }
}
