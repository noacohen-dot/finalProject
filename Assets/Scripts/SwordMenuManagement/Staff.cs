using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    private Animator myAnimator;
    private PlayerMove playerMove;
    private MouseFollow mouseFollow; 
    private float flipRotationY = -180f;
    private float normalRotationY = 0f;
    private float rotationX = 0f;
    readonly int fireHash = Animator.StringToHash("Fire");


    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        if (playerMove == null)
        {
            Debug.LogError("PlayerMove is null");
        }
        myAnimator = GetComponent<Animator>();
        if (myAnimator == null)
        {
            Debug.LogError("Animator is null");
        }
    }
    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        mouseFollow = FindFirstObjectByType<MouseFollow>();
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(fireHash);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
    }
    private void RotateTowardsMouse()
    {
        if (mouseFollow == null || playerMove == null)
            return; // לא נמשיך אם אחד מהם חסר

        Vector3 mouseDir = mouseFollow.transform.position - playerMove.transform.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

        if (mouseFollow.transform.position.x < playerMove.transform.position.x)
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

}
