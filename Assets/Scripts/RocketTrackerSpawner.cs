using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class SpawnConfig
{
    public float spawnDelay = 2f;
    public float initialVelocityXMin = -1f;
    public float initialVelocityXMax = 1f;
    public float rotationMin = -10f;
    public float rotationMax = 10f;
    public float fuelAmount = 0.8f;
    public float lifetime = 1.0f;
    public float spawnerX = 0f;
}

public class RocketTrackerSpawner : MonoBehaviour
{
    public GameObject rocket;

    [Header("Configs (one per level / scene)")]
    public List<SpawnConfig> configs = new List<SpawnConfig>();

    private float count = 0f;
    private float ttd;
    private float ttd_orig;
    private bool already = false;

    private SpawnConfig current;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (configs == null || configs.Count == 0)
        {
            Debug.LogError("RocketTrackerSpawner: No configs set on prefab.");
            return;
        }

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int configIndex = Mathf.Clamp(sceneIndex, 0, configs.Count - 1);
        current = configs[configIndex];

        Vector3 pos = transform.position;
        pos.x = current.spawnerX;
        transform.position = pos;

        if (rb != null)
        {
            Vector2 vel = rb.linearVelocity;
            vel.x = Random.Range(current.initialVelocityXMin, current.initialVelocityXMax);
            rb.linearVelocity = vel;
        }

        ttd = current.lifetime;
        ttd_orig = current.lifetime;

        Debug.Log("RocketTrackerSpawner using config " + configIndex + " in scene " + sceneIndex + " with spawnerX = " + current.spawnerX);
    }

    void Update()
    {
        if (current == null) return;

        count += Time.deltaTime;

        if (count >= current.spawnDelay && !already)
        {
            GameObject temp = Instantiate(rocket);

            temp.transform.position = transform.position + new Vector3(0f, 1.5f, 0f);

            temp.transform.Rotate(
                new Vector3(0f, 0f, Random.Range(current.rotationMin, current.rotationMax))
            );

            ControlRocket cr = temp.GetComponent<ControlRocket>();
            if (cr != null)
            {
                cr.fuelLeft = current.fuelAmount;
            }

            already = true;
        }

        if (already)
        {
            ttd -= Time.deltaTime;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color old = sr.color;
                sr.color = new Color(old.r, old.g, old.b, ttd / ttd_orig);
            }

            if (ttd <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}