using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerMove target;
    [SerializeField] float cameraDistanceZ = -10f;


    void Start()
    {
        target = FindFirstObjectByType<PlayerMove>();
        
    }

    void Update()
    {
       transform.position=new(target.transform.position.x, target.transform.position.y, cameraDistanceZ); 
    }
}
