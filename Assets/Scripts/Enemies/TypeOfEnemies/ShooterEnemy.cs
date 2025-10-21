using UnityEngine;
using System.Collections;

public class ShooterEnemy : MonoBehaviour, IEnemy
{
    [Header("Shooter Enemy Settings")]
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform pivotFire;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletLifetime = 6f;
    [SerializeField] private float fireDelay = 3f;
    private bool isShooting = false;
    private Vector3 lastKnownPlayerPosition;

    private void Start()
    {
        Events.OnPlayerPositionChanged += HandlePlayerPositionChanged;
    }
    private void HandlePlayerPositionChanged(Vector3 newPosition)
    {
        lastKnownPlayerPosition = newPosition;
    }
    public void Attack()
    {
        if (!isShooting)
            StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        isShooting = true;

        if (fireBallPrefab == null || pivotFire == null)
        {
            Debug.LogError("FireBallPrefab or pivotFire is null!");
            yield break;
        }

        Vector2 direction = (lastKnownPlayerPosition - pivotFire.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        GameObject bullet = Instantiate(fireBallPrefab, pivotFire.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * bulletSpeed;
        else
            Debug.LogWarning("FireBall prefab missing Rigidbody2D!");
        Destroy(bullet, bulletLifetime);
        yield return new WaitForSeconds(fireDelay);
        isShooting = false;
    }
    private void OnDestroy()
    {
        Events.OnPlayerPositionChanged -= HandlePlayerPositionChanged;

    }

}
