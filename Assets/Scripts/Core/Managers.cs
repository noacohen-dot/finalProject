using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
