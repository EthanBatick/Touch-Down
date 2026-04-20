using UnityEngine;
using UnityEngine.InputSystem;

public class StartWavesAndBarge : MonoBehaviour
{
    // Start is called once be
    // fore the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D barge;
    public Rigidbody2D waves;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            barge.linearVelocityX = 1f;
            waves.linearVelocityX = -3f;
        }
    }
}
