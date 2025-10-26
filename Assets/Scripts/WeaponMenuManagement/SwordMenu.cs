using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordMenu : MonoBehaviour
{
    private int defaultSlotIndex = 0;
    private int highlightChildIndex = 0;
    private int keyboardInputOffset = 1;
    private int activeSlotIndexNum = 0;
    InputSystem inputActions;
    int activeWeaponPosition = 0;

    private void Start()
    {
        ToggleActiveHighlight(defaultSlotIndex);
        ChangeActiveWeapon();
    }
    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.SwordsMenu.Enable();
        inputActions.SwordsMenu.Keyboard.performed += OnKeyboardPerformed;
    }

    private void OnKeyboardPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - keyboardInputOffset);
    }
    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(highlightChildIndex).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(highlightChildIndex).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).
        GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(activeWeaponPosition, activeWeaponPosition, activeWeaponPosition);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        newWeapon.SetActive(true);

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    private void OnDisable()
    {
        inputActions.SwordsMenu.Disable();
        inputActions.SwordsMenu.Keyboard.performed -= OnKeyboardPerformed;
    }

}



