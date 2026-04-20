using System.Security.Cryptography;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    private float ttd;
    private float ttd_hold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ttd = Random.Range(.6f,1.2f);
        ttd_hold = ttd;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.6f,1.0f), Random.Range(0.0f,0.4f), Random.Range(0.0f,0.4f));
        transform.localScale = new Vector3(1,1,1)*Random.Range(0.02f,0.07f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ttd <= 0)
        {
            Destroy(gameObject);
        }
        ttd -= Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                    gameObject.GetComponent<SpriteRenderer>().color.g,
                                                                    gameObject.GetComponent<SpriteRenderer>().color.b,
                                                                    ttd/ttd_hold);
    }
}
