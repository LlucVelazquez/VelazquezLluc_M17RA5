using Unity.Cinemachine;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;
    //[SerializeField] private CinemachineCamera _playerCamera;

    public float runAcceleration = 0.25f;
    public float runSpeed = 4f;
    //public float drag = 0.1f;
    [Header("Camera Settings")]
    /*public float lookSense = 0.1f;
    public float lookLimitV = 89f;*/

    private PlayerLocomotionInput _playerLocomotionInput;
    /*private Vector2 _cameraRotation;
    private Vector2 _playerTargetRotation = Vector2.zero;*/


    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }
    private void Update()
    {
        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraForwardXZ * _playerLocomotionInput.MovementInput.y + cameraRightXZ * _playerLocomotionInput.MovementInput.x;

        Vector3 movementDelta = movementDirection * runAcceleration;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        /*Vector3 currentDrag = newVelocity.normalized * drag;
        newVelocity = (newVelocity.magnitude > drag) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);*/

        _characterController.Move(newVelocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        /*_cameraRotation.x += lookSense * _playerLocomotionInput.LookInput.x;
        _cameraRotation.y -= lookSense * _playerLocomotionInput.LookInput.y;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -lookLimitV, lookLimitV);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, _cameraRotation.x, 0f);*/
    }
}
