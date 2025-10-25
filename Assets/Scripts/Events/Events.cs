using System;
using UnityEngine;

public static class Events
{
    public static Action<int> OnScoreUpdate;
    public static Action<Vector3> OnPlayerPositionChanged;
    public static Action<Vector2> OnPlayerMoveInputChanged;
    public static Action<Vector3> OnMousePositionChanged;
    public static Action<Camera> OnCameraUpdated;
    public static Action<GameState> OnStateEnter;
    public static Action<GameState> OnStateExit;
    public static Func<GameState> OnGetCurrentState;
    public static Action OnEnemyKilled;
    public static Action OnTreasureCollected;
}
