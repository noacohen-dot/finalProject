using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;


public class Sword : MonoBehaviour
{
    private Animator animator;
    InputSystem inputActions;
    private PlayerMove playerMove;
    [SerializeField] private GameObject swordPivot;
    float normalRotationX = 0f;
    float flipRotationX = 180f;
    float rotationY = 0f;


    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator is null");
        }
        playerMove = GetComponentInParent<PlayerMove>();
        if (playerMove == null)
        {
            Debug.LogError("PlayerMove is null");
        }
    }

    private void Awake()
    {
        inputActions = new();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Combat.started += OnCombatStarted;
    }
    private void Update()
    {
        RotateSword();
    }

    private void RotateSword()
    {
        if (playerMove.moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerMove.moveInput.y, playerMove.moveInput.x) * Mathf.Rad2Deg;
            float rotationZ = angle; 
            if (playerMove.moveInput.x < 0)
            {
                swordPivot.transform.localRotation = Quaternion.Euler(flipRotationX, rotationY, -rotationZ);
            }
            else
            {
                swordPivot.transform.localRotation = Quaternion.Euler(normalRotationX, rotationY, rotationZ);
            }
        }
    }

    private void OnCombatStarted(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        animator.SetTrigger("Attack");
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Combat.started -= OnCombatStarted;
    }
}
