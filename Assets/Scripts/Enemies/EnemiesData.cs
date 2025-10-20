using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "Scriptable Objects/EnemiesData")]
public class EnemiesData : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
}
