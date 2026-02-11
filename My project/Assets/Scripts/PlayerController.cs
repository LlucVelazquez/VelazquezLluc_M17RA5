using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;

    [Header("Movement Stats")]
    public float runAcceleration = 0.25f;
    public float runSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    public float jumpDelay = 0.5f;
    public float turnSpeed = 10f;
    private float _verticalVelocity;
    private bool _isJumping = false;

    [Header("Camera Settings")]
    private float _lookSense = 0.1f;
    private float _lookLimitV = 89f;
    private Vector3 _cameraRotation;

    private PlayerLocomotionInput _playerLocomotionInput;

    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }

    private void Update()
    {
        ApplyGravity();
        Move();
        Jump();
    }

    private void LateUpdate()
    {
        Rotation();
    }
    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
    private void Move()
    {
        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraForwardXZ * _playerLocomotionInput.MovementInput.y + cameraRightXZ * _playerLocomotionInput.MovementInput.x;
        float currentRunSpeed = (_playerLocomotionInput.SprintToggledOn) ? sprintSpeed : runSpeed;
        Vector3 newVelocity = movementDirection * currentRunSpeed;
        newVelocity.y = _verticalVelocity;
        _characterController.Move(newVelocity * Time.deltaTime);

        if (movementDirection.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        //if (!_characterController.isGrounded) newVelocity.y += Physics.gravity.y * Time.deltaTime;
    }

    private void Rotation()
    {
        _cameraRotation.x += _playerLocomotionInput.LookInput.x * _lookSense;
        _cameraRotation.y -= _playerLocomotionInput.LookInput.y * _lookSense;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -_lookLimitV, _lookLimitV);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
    }
    private void Jump()
    {
        // Solo saltamos si se pulsa el botón, estamos en el suelo y NO estamos ya saltando
        if (_playerLocomotionInput.Jump && _characterController.isGrounded && !_isJumping)
        {
            StartCoroutine(PerformDelayedJump());
        }
    }

    private System.Collections.IEnumerator PerformDelayedJump()
    {
        _isJumping = true;
        yield return new WaitForSeconds(jumpDelay);
        _verticalVelocity = jumpForce;
        yield return new WaitForSeconds(0.1f);

        _isJumping = false;
    }
}