using UnityEngine;
using UnityEngine.InputSystem;

public class ControlRocket : MonoBehaviour
{
    public Rigidbody2D self_rigid;
    public Canvas self_ui;

    [Header("Throttle")]
    public float throttle = 0f;          // 0 to 1
    public float throttleSpeed = 1f;   // how fast throttle changes
    public float throttleMin = .33f;
    public float throttleMax = 1f;

    [Header("Thrust")]
    public float maxThrust = 15f;        // actual max force

    [Header("Rotation")]
    public float rotationInput = 0f;
    public float rotationSpeed = 0.1f;
    public float fuelLeft = 0.3f; //0-1
    void Start()
    {
        Canvas can1 = Instantiate(self_ui);
        can1.GetComponentInChildren<GUI_Throttle>().rocket12 = gameObject;
        can1.GetComponentInChildren<GUI_Fuel>().rocket12 = gameObject;
    }
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
            throttle += throttleSpeed * Time.deltaTime;

        if (Keyboard.current.sKey.isPressed)
            throttle -= throttleSpeed * Time.deltaTime;

        throttle = Mathf.Clamp(throttle, throttleMin, throttleMax);

        if (Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed)
            rotationInput = 1f;
        else if (Keyboard.current.dKey.isPressed && !Keyboard.current.aKey.isPressed)
            rotationInput = -1f;
        else
            rotationInput = 0f;
        fuelLeft -= (Time.deltaTime/10)*throttle;
    }

    void FixedUpdate()
    {
        if (fuelLeft > 0)
        {
            self_rigid.AddTorque(rotationInput * rotationSpeed);
            self_rigid.AddForce(transform.up * (throttle * maxThrust));
        }
    }
}