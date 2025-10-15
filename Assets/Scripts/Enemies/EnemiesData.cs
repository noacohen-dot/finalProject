using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "Scriptable Objects/EnemiesData")]
public class EnemiesData : ScriptableObject
{
    [SerializeField] private int score;
    public int Score
    {
        get => score;       
        set => score = value; 
    }
}
