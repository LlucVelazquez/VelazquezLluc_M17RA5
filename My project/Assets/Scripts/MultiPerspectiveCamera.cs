using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MultiPerspectiveCamera : MonoBehaviour
{
    public bool tPerson = true;
    [Header("Objetivos de cámara")]
    public Transform tpTarget;
    public Transform fpTarget;

    [Header("Visibilidad de Jugador")]
    public bool disablePlayerMesh = true;
    [Space(2)]
    public GameObject playerMesh;
    // Para la bota que molesta
    public GameObject playerMesh2;
    [Space(5)]
    private Vector2 angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    private new Camera camera;
    private Vector2 nearPlaneSize;
    private Transform follow;
    private float defaultDistance;
    private float newDistance;

    [Header("Ajustes de Cámara")]
    public float maxDistace = 7f;
    public float minDistance = 2f;
    [Space(6)]
    public int zoomVelocity = 300;
    public float zoomSmoth = 0.1f;
    public Vector2 sensitivity = new Vector2(0.5f, 0.5f);

    [Header("Entradas (Input System)")]
    public InputAction lookAction;
    public InputAction zoomAction;
    public InputAction switchPerspectiveAction;

    [Tooltip("Tecla por defecto si no se asigna un Input Asset")]
    public Key defaultSwitchKey = Key.Q;

    private void Awake()
    {
        if (lookAction == null || lookAction.bindings.Count == 0)
        {
            lookAction = new InputAction("Look", binding: "<Mouse>/delta");
            lookAction.AddCompositeBinding("Dpad")
                .With("Up", "<Gamepad>/rightStick/up")
                .With("Down", "<Gamepad>/rightStick/down")
                .With("Left", "<Gamepad>/rightStick/left")
                .With("Right", "<Gamepad>/rightStick/right");
        }

        if (zoomAction == null || zoomAction.bindings.Count == 0)
            zoomAction = new InputAction("Zoom", binding: "<Mouse>/scroll");

        if (switchPerspectiveAction == null || switchPerspectiveAction.bindings.Count == 0)
            switchPerspectiveAction = new InputAction("Switch", binding: $"<Keyboard>/{defaultSwitchKey}");
    }

    private void OnEnable()
    {
        lookAction.Enable();
        zoomAction.Enable();
        switchPerspectiveAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
        zoomAction.Disable();
        switchPerspectiveAction.Disable();
    }

    void Start()
    {
        ChangePerspective(tPerson);

        defaultDistance = (maxDistace + minDistance) / 2;
        newDistance = defaultDistance;

        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();

        CalculateNearPlaneSize();
    }

    void ChangePerspective(bool ThirdPerson)
    {
        if (ThirdPerson)
        {
            follow = tpTarget;
            if (disablePlayerMesh && playerMesh != null)
            {
                playerMesh.SetActive(true);
                playerMesh2.SetActive(true);
            }
            tPerson = true;
        }
        else
        {
            follow = fpTarget;
            if (disablePlayerMesh && playerMesh != null)
            {
                playerMesh.SetActive(false);
                playerMesh2.SetActive(false);
            }
            tPerson = false;
        }
    }

    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;
        nearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.4f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[]
        {
            center - right + up,
            center + right + up,
            center - right - up,
            center + right - up
        };
    }

    void Update()
    {
        // --- 1. Lectura del movimiento (Mouse Look) ---
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float hor = lookInput.x;
        if (hor != 0)
        {
            angle.x += hor * Mathf.Deg2Rad * sensitivity.x;
        }

        float ver = lookInput.y;
        if (ver != 0)
        {
            angle.y += ver * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }

        // --- 2. Lógica de Zoom ---
        if (tPerson)
        {
            float scrollValue = zoomAction.ReadValue<Vector2>().y;

            float scrollDelta = Mathf.Clamp(scrollValue, -1f, 1f);

            if (scrollDelta > 0)
            {
                newDistance -= 0.1f * (Time.deltaTime * zoomVelocity);
            }
            else if (scrollDelta < 0)
            {
                newDistance += 0.1f * (Time.deltaTime * zoomVelocity);
            }
            newDistance = Mathf.Clamp(newDistance, minDistance, maxDistace);
            defaultDistance = Mathf.Lerp(defaultDistance, newDistance, zoomSmoth);
        }
        else
        {
            defaultDistance = 0.1f;
        }

        // --- 3. Cambio de Perspectiva ---
        if (switchPerspectiveAction.triggered)
        {
            ChangePerspective(!tPerson);
        }
    }

    void LateUpdate()
    {
        if (follow == null) return;

        Vector3 direction = new Vector3(
            Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
            -Mathf.Sin(angle.y),
            -Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
            );

        RaycastHit hit;
        float distance = defaultDistance;
        Vector3[] points = GetCameraCollisionPoints(direction);

        // Comprobación de colisiones
        foreach (Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out hit, defaultDistance))
            {
                // Aseguramos que no choque con el propio jugador si tiene colisionadores
                if (hit.transform != playerMesh.transform && hit.transform != transform)
                {
                    distance = Mathf.Min((hit.point - follow.position).magnitude, distance);
                }
            }
        }

        transform.position = follow.position + direction * distance;
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
    }
}