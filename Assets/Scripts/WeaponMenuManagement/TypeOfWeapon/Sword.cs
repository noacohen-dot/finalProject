using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;


public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    GameObject swordPivot;
    float normalRotationX = 0f;
    float flipRotationX = 180f;
    float rotationY = 0f;
    private Vector2 lastMoveInput = Vector2.right;
    [SerializeField] private WeaponInfo weaponInfo;
    private float flipDirectionThresholdX = 0f;

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
        Events.OnPlayerMoveInputChanged += HandlePlayerMoveInput;
    }

    private void Update()
    {
        RotateSword();
    }
    private void HandlePlayerMoveInput(Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            lastMoveInput = moveInput;
            RotateSword();
        }
    }

    private void RotateSword()
    {
        float angle = Mathf.Atan2(lastMoveInput.y, lastMoveInput.x) * Mathf.Rad2Deg;
        float rotationZ = angle;

        if (lastMoveInput.x < flipDirectionThresholdX)
            swordPivot.transform.localRotation = Quaternion.Euler(flipRotationX, rotationY, -rotationZ);
        else
            swordPivot.transform.localRotation = Quaternion.Euler(normalRotationX, rotationY, rotationZ);
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void OnDestroy()
    {
        Events.OnPlayerMoveInputChanged -= HandlePlayerMoveInput;
    }
}