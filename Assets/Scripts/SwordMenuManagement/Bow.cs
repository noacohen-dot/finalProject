using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] ActiveWeapon activeWeapon;

    public void Attack()
    {
        Debug.Log("Bow Attack");
        activeWeapon.ToggleIsAttacking(false);
    }
}
