using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput : MonoBehaviour, InputSystem_Actions.IPlayerLocomotionMapActions
{
    [SerializeField] private bool _holdToSprint = true;
    public InputSystem_Actions PlayerControls { get; private set; }
    public bool Shoot { get; private set; }
    public bool Jump { get; private set; }
    public bool Aim { get; private set; } = false;
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool SprintToggledOn { get; private set; }
    private Animator animator;
    private float XDirection = 0f;
    private float YDirection = 0f;
    private float _stateAnim;

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
    private void LateUpdate()
    {
        Jump = false;
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
            _stateAnim = 0.5f;
        }
        else
        {
            animator.SetFloat("Velocity", 0f);
            _stateAnim = 0;
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
                Jump = true;
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
        if (context.performed)
        {
            SprintToggledOn = _holdToSprint || !SprintToggledOn;
            animator.SetFloat("Velocity", 1f);
        }
        else if (context.canceled)
        {
            SprintToggledOn = !_holdToSprint && SprintToggledOn;
            animator.SetFloat("Velocity", _stateAnim);
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Aim = true;
        }
        else
        {
            Aim = false;
        }
    }
}
