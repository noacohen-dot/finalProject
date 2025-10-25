using UnityEngine;
using System.Collections;

public class FlowerTrapEnemy : MonoBehaviour, IEnemy
{
    [Header("FlowerTrap Settings")]
    [SerializeField] private GameObject flowerPrefab;
    [SerializeField] private Transform pivotFlower;
    [SerializeField] private float flowerLifetime = 10f;
    [SerializeField] private float flowerDelay = 3f;

    private bool isAttacking = false;

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        isAttacking = true;
        if (flowerPrefab == null || pivotFlower == null)
        {
            Debug.LogError("FlowerPrefab or pivotFlower is null!");
            yield break;
        }
        GameObject flower = Instantiate(flowerPrefab, pivotFlower.position, Quaternion.identity);
        Destroy(flower, flowerLifetime);
        yield return new WaitForSeconds(flowerDelay);
        isAttacking = false;
    }
}
