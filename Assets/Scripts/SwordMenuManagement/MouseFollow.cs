using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    private Camera mainCamera;
    private InputSystem inputActions; 
    private Vector2 mousePosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new InputSystem(); 
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.MousePosition.performed += OnMouseMoved; 
    }

    private void OnDisable()
    {
        inputActions.Player.MousePosition.performed -= OnMouseMoved;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        FaceMouse();
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void FaceMouse()
    {
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane)
        );

        Vector2 direction = (Vector2)(worldMousePos - transform.position);
        transform.right = direction;
    }
}
