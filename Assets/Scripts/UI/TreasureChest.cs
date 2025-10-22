using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private GameObject collectEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Events.OnScoreUpdate?.Invoke(scoreValue);
            if (collectEffect != null)
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject,0.3f);
        }
    }
}
