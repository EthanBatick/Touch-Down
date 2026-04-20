using UnityEngine;
using UnityEngine.InputSystem;

public class InputPrinter : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.wKey.wasPressedThisFrame) Debug.Log("W");
        if (Keyboard.current.aKey.wasPressedThisFrame) Debug.Log("A");
        if (Keyboard.current.sKey.wasPressedThisFrame) Debug.Log("S");
        if (Keyboard.current.dKey.wasPressedThisFrame) Debug.Log("D");
        if (Keyboard.current.spaceKey.wasPressedThisFrame) Debug.Log("Space");
    }
}