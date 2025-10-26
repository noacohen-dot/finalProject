using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSword : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    private float flipRotationY = -180f;
    private float normalRotationY = 0f;
    private float rotationX = 0f;
    private Vector3 playerPosition;
    private Vector3 mouseWorldPosition;

    private void Start()
    {
       
        Events.OnPlayerPositionChanged += UpdatePlayerPosition;
        Events.OnMousePositionChanged += UpdateMousePosition;

    }
    private void UpdatePlayerPosition(Vector3 newPosition)
    {
        playerPosition = newPosition;
    }
    private void UpdateMousePosition(Vector3 newMousePos)
    {
        mouseWorldPosition = newMousePos;
    }
    private void Update()
    {
        RotateTowardsMouse();
    }

    public void Attack()
    {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
    }

    private void RotateTowardsMouse()
    {
        if (playerPosition == Vector3.zero || mouseWorldPosition == Vector3.zero)
            return;

        Vector3 mouseDir = mouseWorldPosition - playerPosition;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

        if (mouseWorldPosition.x < playerPosition.x)
        {
            transform.localRotation = Quaternion.Euler(rotationX, flipRotationY, angle);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(rotationX, normalRotationY, angle);
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void OnDestroy()
    {
        Events.OnPlayerPositionChanged -= UpdatePlayerPosition;
        Events.OnMousePositionChanged -= UpdateMousePosition;
    }
}
