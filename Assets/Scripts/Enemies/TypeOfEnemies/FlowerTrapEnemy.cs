using UnityEngine;
using System.Collections;


public class FlowerTrapEnemy : MonoBehaviour,IEnemy
{
    [Header("FireTrap Settings")]
    [SerializeField] private GameObject FlowerPrefab;
    [SerializeField] private Transform pivotFlower;
    [SerializeField] private float bulletLifetime = 10f;
    [SerializeField] private float fireDelay = 4f;

    private PlayerMove player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        if (player == null)
            Debug.LogError("PlayerMove is null!");

    }

    public void Attack()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        if (FlowerPrefab == null || pivotFlower == null )
        {
            Debug.LogError("fireballPrefab or pivotFireBall is null!");
            yield break;
        }
        GameObject bullet = Instantiate(FlowerPrefab, pivotFlower.position, Quaternion.identity);
        Destroy(bullet, bulletLifetime);
        yield return new WaitForSeconds(fireDelay);
    }
}
