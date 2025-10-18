using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] ActiveWeapon activeWeapon;

    public void Attack()
    {
        Debug.Log("Staff Attack");
        activeWeapon.ToggleIsAttacking(false);

    }
}
