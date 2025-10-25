using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon {  get; private set; }
    InputSystem inputActions;
    private bool attackButtonDown, isAttacking = false;
    private float timeBetweenAttacks;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new();
    }
    private void Start()
    {
        AttackCoolDown();
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
    public void NewWeapon (MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCoolDown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCoolDown;

    }
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCoolDown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
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
            AttackCoolDown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Combat.started -= OnStartAttacking;
        inputActions.Player.Combat.canceled -= OnStopAttacking;
    }
}
