using UnityEngine;
using UnityEngine.InputSystem;

public class RocketLegController : MonoBehaviour
{
    public Transform legRightPivot;
    public Transform legLeftPivot;

    public bool extended = false;
    public float deploySpeed = 180f;

    public float rightRetractedAngle = 0f;
    public float rightExtendedAngle = -115f;

    public float leftRetractedAngle = 0f;
    public float leftExtendedAngle = 115f;

    void Start()
    {
        extended = false;
    }
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            extended = !extended;
        }

        float rightTarget = extended ? rightExtendedAngle : rightRetractedAngle;
        float leftTarget = extended ? leftExtendedAngle : leftRetractedAngle;

        if (legRightPivot != null)
        {
            float current = legRightPivot.localEulerAngles.z;
            if (current > 180f) current -= 360f;

            float next = Mathf.MoveTowards(current, rightTarget, deploySpeed * Time.deltaTime);
            legRightPivot.localRotation = Quaternion.Euler(0f, 0f, next);
        }

        if (legLeftPivot != null)
        {
            float current = legLeftPivot.localEulerAngles.z;
            if (current > 180f) current -= 360f;

            float next = Mathf.MoveTowards(current, leftTarget, deploySpeed * Time.deltaTime);
            legLeftPivot.localRotation = Quaternion.Euler(0f, 0f, next);
        }
    }
}