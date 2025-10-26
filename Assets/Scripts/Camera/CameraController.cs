using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraDistanceZ = -10f;
    private Vector3 currentPlayerPosition;

    void Start()
    {
        Events.OnPlayerPositionChanged += HandlePlayerPositionChanged;
    }
    private void HandlePlayerPositionChanged(Vector3 playerPos)
    {
        currentPlayerPosition = playerPos;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
                   currentPlayerPosition.x,
                   currentPlayerPosition.y,
                   cameraDistanceZ
               );
        Events.OnCameraUpdated?.Invoke(Camera.main);
    }
    private void OnDestroy()
    {
        Events.OnPlayerPositionChanged -= HandlePlayerPositionChanged;
    }

}
