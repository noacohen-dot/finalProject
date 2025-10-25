using UnityEngine;
using System.Collections;
using static EnemyController;

public class EnemyController : MonoBehaviour
{

    [Header("Enemy Settings")]
    [SerializeField] private EnemyType enemyType;
    
    private bool canAttack = true;
    private bool isVisibleToCamera = false;

    [Header("Roaming Settings")]
    [SerializeField] private float roamDirectionChangeDelay = 2f;
    [SerializeField] private float roamRangeX = 1f;
    [SerializeField] private float roamRangeY = 1f;

    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 15f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private bool stopWhileAttacking = false;
    [SerializeField] private IEnemy enemyBehavior; 
    private enum EnemyState
    {
        Roaming,
        Attacking, 
        Idle
    }
    public enum EnemyType
    {
        Passive,
        Chaser,
        FlowerTrap,
        Shooter
    }
    private Vector3 lastKnownPlayerPosition;
    private EnemyState currentState;
    private EnemyMove enemyMove;
    private Vector2 roamTarget;
    private float roamTimer = 0f;
    private Renderer myRenderer;
    private void Awake()
    {
        currentState = EnemyState.Idle;
    }

    void OnBecameVisible()
    {
        if (Camera.current == Camera.main)
            isVisibleToCamera = true;
    }

    void OnBecameInvisible()
    {
        if (Camera.current == Camera.main)
            isVisibleToCamera = false;
    }

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        if (enemyMove == null)
        {
            Debug.LogError("EnemyMove component is missing!");
        }
        myRenderer = GetComponent<Renderer>();
        if(myRenderer == null)
        {
            Debug.LogError("Renderer component is missing!");
        }
        AssignEnemyBehavior();
        roamTarget = GetRandomRoamTarget();
        Events.OnPlayerPositionChanged += HandlePlayerPositionChanged;
    }
    private void HandlePlayerPositionChanged(Vector3 newPosition)
    {

        lastKnownPlayerPosition = newPosition;
    }
    private void Update()
    {
        if (myRenderer != null)
            isVisibleToCamera = myRenderer.isVisible;
        HandleState();
    }
    private void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                enemyMove.StopMoving();
                if (isVisibleToCamera)
                    currentState = EnemyState.Roaming;
                break;
            case EnemyState.Roaming:
                HandleRoaming();
                break;

            case EnemyState.Attacking:
                HandleAttacking();
                break;
        }
    }

    private void HandleRoaming()
    {
        roamTimer += Time.deltaTime;
        enemyMove.MoveTo(roamTarget);

        if (Vector2.Distance(transform.position, lastKnownPlayerPosition) < attackRange)
        {
            currentState = EnemyState.Attacking;
        }
        if (roamTimer > roamDirectionChangeDelay)
        {
            roamTarget = GetRandomRoamTarget();
        }
    }

    private void HandleAttacking()
    {
        if (Vector2.Distance(transform.position, lastKnownPlayerPosition) > attackRange)
        {
            currentState = EnemyState.Roaming;
            return;
        }
        if (canAttack)
        {
            canAttack = false;

            enemyBehavior?.Attack(); 
            if (stopWhileAttacking)
                enemyMove.StopMoving();

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void AssignEnemyBehavior()
    {
        switch (enemyType)
        {
            case EnemyType.Passive:
                enemyBehavior = GetComponent<PassiveEnemy>();
                break;
            case EnemyType.Chaser:
                enemyBehavior = GetComponent<ChaserEnemy>();
                break;
            case EnemyType.FlowerTrap:
                enemyBehavior = GetComponent<FlowerTrapEnemy>();
                break;
            case EnemyType.Shooter:
                enemyBehavior=GetComponent<ShooterEnemy>();
                break;
            default:
                Debug.LogWarning($"No behavior found for enemy type: {enemyType}");
                break;
        }

        if (enemyBehavior == null)
        {
            Debug.LogError($"Missing IEnemy behavior on {name} for type {enemyType}");
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRandomRoamTarget()
    {
        roamTimer = 0f;
        return new Vector2(Random.Range(-roamRangeX, roamRangeX),Random.Range(-roamRangeY, roamRangeY)).normalized;
    }
    private void OnDestroy()
    {
        Events.OnPlayerPositionChanged -= HandlePlayerPositionChanged;
    }
}
