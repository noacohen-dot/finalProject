using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;
    InputSystem inputActions;
    private bool attackButtonDown, isAttacking = false;

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Combat.started += OnStartAttacking;
        inputActions.Player.Combat.canceled += OnStopAttacking;
    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void OnStartAttacking(InputAction.CallbackContext context)
    {
        attackButtonDown = true;
    }

    private void OnStopAttacking(InputAction.CallbackContext context)
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;

            if (currentActiveWeapon is IWeapon weapon)
            {
                weapon.Attack();
            }
            else
            {
                Debug.LogError("currentActiveWeapon is null or does not implement IWeapon!");
            }
        }
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Combat.started -= OnStartAttacking;
        inputActions.Player.Combat.canceled -= OnStopAttacking;
    }
}
