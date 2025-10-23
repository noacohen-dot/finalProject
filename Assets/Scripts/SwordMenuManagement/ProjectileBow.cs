using UnityEngine;

public class ProjectileBow : MonoBehaviour
{
    [SerializeField] private ArrowInfo arrowInfo;

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * arrowInfo.moveSpeedProjectile, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
