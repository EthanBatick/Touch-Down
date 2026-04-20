using UnityEngine;

public class BlinkLight1 : MonoBehaviour
{
    public float period;
    private float count;
    public Color col1;
    public Color col2;
    private bool switch1 = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        period = .6f;
        col1 = new Color(.9f,.1f,.1f,1);
        col2 = new Color(.9f,.65f,.1f,1);
        GetComponent<SpriteRenderer>().color = col1;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count >= period)
        {
            switch1 = !switch1;
            if (switch1){GetComponent<SpriteRenderer>().color = col1;}
            else {GetComponent<SpriteRenderer>().color = col2;}
            count = 0.0f;
        }
    }
}
