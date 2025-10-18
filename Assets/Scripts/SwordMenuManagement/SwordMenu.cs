using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordMenu : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    InputSystem inputActions;

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
    }
    private void OnDisable()
    {
        inputActions.SwordsMenu.Disable();
        inputActions.SwordsMenu.Keyboard.performed -= ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }
}



