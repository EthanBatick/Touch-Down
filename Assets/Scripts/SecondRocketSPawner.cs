using UnityEngine;

public class SpawnSecondBoosterOnFirstSpawn : MonoBehaviour
{
    [Header("What to watch for")]
    public GameObject rocketPrefab;

    [Header("Second booster settings")]
    public float xOffset = 6.25f;
    public bool spawnToRight = true;

    [Header("Optional")]
    public bool copyFuel = true;

    private GameObject firstRocketFound;
    private bool alreadySpawnedSecond = false;

    void Update()
    {
        if (alreadySpawnedSecond) return;
        if (rocketPrefab == null) return;

        GameObject foundRocket = FindFirstMatchingRocket();
        if (foundRocket == null) return;

        firstRocketFound = foundRocket;
        SpawnSecondRocket(firstRocketFound);
        alreadySpawnedSecond = true;
    }

    GameObject FindFirstMatchingRocket()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if (obj == null) continue;

            // Match by prefab name roughly
            string objName = obj.name.Replace("(Clone)", "").Trim();
            string prefabName = rocketPrefab.name.Trim();

            if (objName == prefabName)
            {
                return obj;
            }
        }

        return null;
    }

    void SpawnSecondRocket(GameObject original)
    {
        Vector3 offset = new Vector3(spawnToRight ? xOffset : -xOffset, 0f, 0f);

        GameObject second = Instantiate(
            rocketPrefab,
            original.transform.position + offset,
            original.transform.rotation
        );

        CopyPhysics(original, second);
        CopyRocketValues(original, second);
    }

    void CopyPhysics(GameObject original, GameObject second)
    {
        Rigidbody2D rbOriginal = original.GetComponent<Rigidbody2D>();
        Rigidbody2D rbSecond = second.GetComponent<Rigidbody2D>();

        if (rbOriginal != null && rbSecond != null)
        {
            rbSecond.linearVelocity = rbOriginal.linearVelocity;
            rbSecond.angularVelocity = rbOriginal.angularVelocity;
        }
    }

    void CopyRocketValues(GameObject original, GameObject second)
    {
        if (!copyFuel) return;

        ControlRocket originalControl = original.GetComponent<ControlRocket>();
        ControlRocket secondControl = second.GetComponent<ControlRocket>();

        if (originalControl != null && secondControl != null)
        {
            secondControl.fuelLeft = originalControl.fuelLeft;
        }
    }
}