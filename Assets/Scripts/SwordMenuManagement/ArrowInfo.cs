using UnityEngine;

[CreateAssetMenu(fileName = "ArrowInfo", menuName = "Scriptable Objects/New Arrow")]
public class ArrowInfo : ScriptableObject
{
    public GameObject arrowPrefab;
    public float moveSpeedProjectile;
}
