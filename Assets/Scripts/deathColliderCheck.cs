using UnityEngine;

public class RocketBreakTrigger : MonoBehaviour
{
    private RocketBreakApart rocket;

    void Start()
    {
        rocket = GetComponentInParent<RocketBreakApart>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("FireParticle"))
        {
            rocket.BreakApart();
        }
    }
}