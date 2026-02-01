using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput : MonoBehaviour, InputSystem_Actions.IPlayerLocomotionMapActions
{
    public InputSystem_Actions PlayerControls { get; private set; }
    public bool Shoot { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    private Animator animator;
    private float XDirection = 0f;
    private float YDirection = 0f;
    private float Velocity = 0f;

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
        XDirection = context.ReadValue<Vector2>().x;
        YDirection = context.ReadValue<Vector2>().y;
        //Velocity = (XDirection + YDirection) / 2;
        print(MovementInput);
        if (XDirection != 0f || YDirection != 0f)
        {
            animator.SetFloat("Velocity", 0.5f);
        }
        else
        {
            animator.SetFloat("Velocity", 0f);
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("isDancing", true);
        }
        else
        {
            animator.SetBool("isDancing", false) ;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            animator.SetBool("Jump", true) ;
        }
        else
        {
            animator.SetBool("Jump", false ) ;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("click");
            Shoot = true;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
