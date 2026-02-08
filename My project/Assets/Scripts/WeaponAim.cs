using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    public Transform weaponTransform; 
    public Vector3 hipPosition;     
    public Vector3 aimPosition;

    public float aimSpeed = 10f;

    public Camera mainCamera;
    public float hipFOV = 60f;
    public float aimFOV = 40f;
    private PlayerLocomotionInput _playerLocomotionInput;
    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }

    private void Update()
    {
        if (_playerLocomotionInput.Aim)
        {
            Aim();
        }
        else
        {
            StopAiming();
        }
    }

    public void Aim()
    {
        weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, aimPosition, Time.deltaTime * aimSpeed);

        if (mainCamera != null)
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, aimFOV, Time.deltaTime * aimSpeed);
    }

    public void StopAiming()
    {
        weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, hipPosition, Time.deltaTime * aimSpeed);

        if (mainCamera != null)
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, hipFOV, Time.deltaTime * aimSpeed);
    }
}