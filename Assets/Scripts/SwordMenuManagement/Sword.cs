using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;


public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    InputSystem inputActions;
    private PlayerMove playerMove;
    GameObject swordPivot;
    float normalRotationX = 0f;
    float flipRotationX = 180f;
    float rotationY = 0f;
    private Vector2 lastMoveInput = Vector2.right;
    [SerializeField] private WeaponInfo weaponInfo;

    void Start()
    {
        if (swordPivot == null)
        {
            swordPivot = GameObject.FindWithTag("SwordPivot");
            if (swordPivot == null)
            {
                Debug.LogError("SwordPivot not found in scene!");
            }
        }
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
            lastMoveInput = playerMove.moveInput;
        }
        float angle = Mathf.Atan2(lastMoveInput.y, lastMoveInput.x) * Mathf.Rad2Deg;
        float rotationZ = angle;

        if (lastMoveInput.x < 0)
        {
            swordPivot.transform.localRotation = Quaternion.Euler(flipRotationX, rotationY, -rotationZ);
        }
        else
        {
            swordPivot.transform.localRotation = Quaternion.Euler(normalRotationX, rotationY, rotationZ);
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Sword Attack");
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
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
