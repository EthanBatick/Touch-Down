using UnityEngine;

public class RocketBreakApart : MonoBehaviour
{
    [Header("Break Apart")]
    public float explosionForce = 8f;
    public float torqueForce = 200f;
    public bool broken = false;

    [Header("Fire Explosion")]
    public GameObject fireParticlePrefab;
    public int fireCount = 1000;
    public float fireSpawnRadius = 1.2f;
    public float fireForceMin = 2f;
    public float fireForceMax = 7f;

    public void BreakApart()
    {
        if (broken) return;
        broken = true;

        SpawnFireExplosion();

        // break all direct children apart
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform piece = transform.GetChild(i);

            // skip trigger object if needed
            if (piece.name == "TriggerZone")
                continue;

            piece.parent = null;

            Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();
            if (rb == null)
                rb = piece.gameObject.AddComponent<Rigidbody2D>();

            rb.gravityScale = 1f;
            rb.linearDamping = 0f;
            rb.angularDamping = 0.05f;

            Collider2D col = piece.GetComponent<Collider2D>();
            if (col == null)
            {
                SpriteRenderer sr = piece.GetComponent<SpriteRenderer>();
                if (sr != null)
                    col = piece.gameObject.AddComponent<BoxCollider2D>();
            }

            Vector2 dir = ((Vector2)piece.position - (Vector2)transform.position).normalized;
            if (dir == Vector2.zero)
                dir = Random.insideUnitCircle.normalized;

            rb.AddForce(dir * explosionForce, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-torqueForce, torqueForce));
        }

        //Destroy(gameObject);
    }

    void SpawnFireExplosion()
    {
        if (fireParticlePrefab == null) return;

        for (int i = 0; i < fireCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * fireSpawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            GameObject fire = Instantiate(fireParticlePrefab, spawnPos, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
            if (rb == null)
                rb = fire.AddComponent<Rigidbody2D>();

            rb.gravityScale = 0.3f;
            rb.linearDamping = Random.Range(0.5f, 2f);
            rb.angularDamping = 0.05f;

            Vector2 dir = (randomOffset.normalized + Random.insideUnitCircle * 0.4f).normalized;
            if (dir == Vector2.zero)
                dir = Random.insideUnitCircle.normalized;

            float force = Random.Range(fireForceMin, fireForceMax);
            rb.AddForce(dir * force, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-300f, 300f));
        }
    }
}