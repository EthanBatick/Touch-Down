using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightGridFin : MonoBehaviour
{
    public float scalex;
    public float scaley;
    public float scalez;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
        scalez = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            transform.localScale = new Vector3(scalex, scaley*3f, scalez);
        }
        else
        {
            transform.localScale = new Vector3(scalex, scaley, scalez);
        }
    }
}
