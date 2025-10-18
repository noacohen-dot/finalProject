using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordMenu : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    [SerializeField] ActiveWeapon activeWeapon;

    InputSystem inputActions;

    private void Start()
    {
        ToggleActiveHighlight(0);
    }
    private void Awake()
    {
        inputActions = new();
    }


    private void OnEnable()
    {
        inputActions.SwordsMenu.Enable();
        inputActions.SwordsMenu.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {
        if (activeWeapon == null)
        {
            Debug.LogError("ActiveWeapon reference is missing!");
            return;
        }

        if (activeWeapon.CurrentActiveWeapon != null)
        {
            Destroy(activeWeapon.CurrentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            activeWeapon.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum)
            .GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(
            weaponToSpawn,
            activeWeapon.transform.position,
            Quaternion.identity
        );

        newWeapon.transform.parent = activeWeapon.transform;

        activeWeapon.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    private void OnDisable()
    {
        inputActions.SwordsMenu.Disable();
        inputActions.SwordsMenu.Keyboard.performed -= ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

}



