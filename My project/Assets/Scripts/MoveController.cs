using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float velocity = 5f;
    private CharacterController characterController;
    private InputSystem_Actions controller;
    private Vector2 inputMove;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        controller = new InputSystem_Actions();
        controller.Player.Move.performed += context => inputMove = context.ReadValue<Vector2>();
        controller.Player.Move.canceled += context => inputMove = Vector2.zero;
    }
    private void OnEnable()
    {
        controller.Enable();
    }
    private void OnDisable()
    {
        controller.Disable();
    }
    private void Update()
    {
        Vector3 move = transform.right * inputMove.x + transform.forward * inputMove.y;
        characterController.Move(move * velocity * Time.deltaTime);
    }
}
