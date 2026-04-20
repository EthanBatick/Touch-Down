using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlume : MonoBehaviour
{
    public GameObject particle;

    [Header("Gimbal")]
    public float maxGimbal = 2.0f;
    public float transitionTime = 0.15f;

    [Header("Spawn")]
    public int maxParticlesPerFrame = 5;

    private float count;
    private float currentGimbal = 0f;
    private ControlRocket cr;

    void Start()
    {
        count = 0f;
        cr = GetComponent<ControlRocket>();
    }

    void Update()
    {
        if (cr == null) return;
        if (cr.fuelLeft <= 0) return;

        count += Time.deltaTime;

        float targetGimbal = 0f;

        if (Keyboard.current.aKey.isPressed)
            targetGimbal = -maxGimbal;
        else if (Keyboard.current.dKey.isPressed)
            targetGimbal = maxGimbal;

        float gimbalSpeed = (transitionTime <= 0f) ? 99999f : (maxGimbal / transitionTime);
        currentGimbal = Mathf.MoveTowards(currentGimbal, targetGimbal, gimbalSpeed * Time.deltaTime);

        float interval = (cr.throttleMax - cr.throttle + 1f) / 1000f;

        int spawnedThisFrame = 0;

        while (count >= interval && spawnedThisFrame < maxParticlesPerFrame)
        {
            count -= interval;
            spawnedThisFrame++;

            GameObject temp = Instantiate(particle);
            temp.transform.position =
                transform.position
                - Random.Range(0.25f, 0.35f) * transform.up
                - Random.Range(-0.003f, 0.003f) * transform.right;

            temp.transform.localScale = transform.localScale;

            Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity =
                    Random.Range(-0.5f, 0.5f) * (Vector2)transform.right
                    - (cr.throttle + 1f) * 4.5f * (Vector2)transform.up
                    + currentGimbal * (Vector2)transform.right;

                rb.angularVelocity = Random.Range(-5.5f, 5.5f);
            }

            Collider2D tempCol = temp.GetComponent<Collider2D>();
            if (tempCol != null)
            {
                foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
                {
                    Physics2D.IgnoreCollision(tempCol, col);
                }
            }
        }
    }
}