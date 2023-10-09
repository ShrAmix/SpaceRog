using UnityEngine;

public class BackgroundData : MonoBehaviour
{
    public static BackgroundData Instance;

    public float startX1 = 24f;
    public float startX2 = 24f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
