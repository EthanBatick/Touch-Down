using UnityEngine;

public class SpawnClouds : MonoBehaviour
{
    public float speed = 5f;
    public GameObject[] cloudOptions;

    private float count = 0f;
    private float period = 0f;

    void Start()
    {
        period = GetPeriod();
    }

    void Update()
    {
        count += Time.deltaTime;

        if (count >= period)
        {
            count = 0f;
            period = GetPeriod();

            int index = Random.Range(0, cloudOptions.Length);
            GameObject chosen = cloudOptions[index];

            Rigidbody2D rb = Instantiate(chosen, transform.position, Quaternion.identity)
                .GetComponent<Rigidbody2D>();

            rb.linearVelocity = new Vector2(speed, 0f);
        }
    }

    float GetPeriod()
    {
        return Random.Range(2f, 4f);
    }
}