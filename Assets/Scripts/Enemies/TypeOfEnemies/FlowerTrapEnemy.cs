using UnityEngine;
using System.Collections;


public class FlowerTrapEnemy : MonoBehaviour,IEnemy
{
    [Header("FlowerTrap Settings")]
    [SerializeField] private GameObject FlowerPrefab;
    [SerializeField] private Transform pivotFlower;
    [SerializeField] private float flowerLifetime = 10f;
    [SerializeField] private float flowereDelay = 4f;

    public void Attack()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        if (FlowerPrefab == null || pivotFlower == null )
        {
            Debug.LogError("FlowerPrefab or pivotFlower is null!");
            yield break;
        }
        GameObject bullet = Instantiate(FlowerPrefab, pivotFlower.position, Quaternion.identity);
        Destroy(bullet, flowerLifetime);
        yield return new WaitForSeconds(flowereDelay);
    }

}
