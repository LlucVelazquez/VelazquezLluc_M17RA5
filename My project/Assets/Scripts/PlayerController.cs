using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;
    //[SerializeField] private CinemachineCamera _playerCamera;

    public float runAcceleration = 0.25f;
    public float runSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    //public float drag = 0.1f;
    [Header("Camera Settings")]
    private float _lookSense = 0.1f;
    private float _lookLimitV = 89f;
    private Vector3 _cameraRotation;

    private PlayerLocomotionInput _playerLocomotionInput;
    /*private Vector2 _cameraRotation;
    private Vector2 _playerTargetRotation = Vector2.zero;*/


    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }
    private void Update()
    {
        Move();
        Rotation();
    }
    private void Move()
    {
        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraForwardXZ * _playerLocomotionInput.MovementInput.y + cameraRightXZ * _playerLocomotionInput.MovementInput.x;

        float currentRunSpeed = (_playerLocomotionInput.SprintToggledOn) ? sprintSpeed : runSpeed;

        Vector3 newVelocity = movementDirection * currentRunSpeed;

        _characterController.Move(newVelocity * Time.deltaTime);
    }

    private void Rotation()
    {
        _cameraRotation.x += _playerLocomotionInput.LookInput.x * _lookSense;
        _cameraRotation.y -= _playerLocomotionInput.LookInput.y * _lookSense;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -_lookLimitV, _lookLimitV);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, _cameraRotation.x, 0f);
    }
}
