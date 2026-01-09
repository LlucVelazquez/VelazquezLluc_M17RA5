using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput : MonoBehaviour, InputSystem_Actions.IPlayerLocomotionMapActions
{
    public InputSystem_Actions PlayerControls { get; private set; }
    public Vector2 MovementInput { get; private set; }

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
    }
}
