using UnityEngine;

public class KillCloud : MonoBehaviour
{
    public float ttd; // time to die (seconds)

    void Start()
    {
        ttd = 60f;
        Destroy(gameObject, ttd);
    }
}