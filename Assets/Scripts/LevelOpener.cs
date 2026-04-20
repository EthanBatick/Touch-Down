using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOpener : MonoBehaviour
{
    public GameObject rocket_tracker_spawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Instantiate(rocket_tracker_spawner);
            Destroy(gameObject);
        }
    }
}
