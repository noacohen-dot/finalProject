using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    private Camera mainCamera;
    private InputSystem inputActions; 
    private Vector2 mousePosition;

    private void Start()
    {
        Events.OnCameraUpdated += HandleCameraUpdated;
    }
    private void Awake()
    {
        inputActions = new InputSystem(); 
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.MousePosition.performed += OnMouseMoved; 
    }

    private void HandleCameraUpdated(Camera cam)
    {
        mainCamera = cam;
    }

    private void Update()
    {
        if (mainCamera == null) return;
        FaceMouse();
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void FaceMouse()
    {
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(
             new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        Vector2 direction = (Vector2)(worldMousePos - transform.position);
        transform.right = direction;

        Events.OnMousePositionChanged?.Invoke(worldMousePos);
    }
    private void OnDestroy()
    {
        Events.OnCameraUpdated -= HandleCameraUpdated;
    }
    private void OnDisable()
    {
        inputActions.Player.MousePosition.performed -= OnMouseMoved;
        inputActions.Player.Disable();
    }
}
