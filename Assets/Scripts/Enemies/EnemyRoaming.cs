using UnityEngine;
using System.Collections;

public class EnemyRoaming : MonoBehaviour
{
    [SerializeField] float roamTimerDelay = 3f;
    private float roamRangeX = 1f;
    private float roamRangeY = 1f;

    private EnemyMove enemyMover; 

    private void Start()
    {
        enemyMover = GetComponent<EnemyMove>();
        if (enemyMover == null)
        {
            Debug.LogError("EnemyMover is null");
        }
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyMover.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamTimerDelay);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-roamRangeX, roamRangeX), Random.Range(-roamRangeY, roamRangeY)).normalized;
    }
}
