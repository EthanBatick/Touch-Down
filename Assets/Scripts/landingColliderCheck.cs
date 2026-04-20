using UnityEngine;
using UnityEngine.SceneManagement;

public class landingColliderCheck : MonoBehaviour
{
    [Header("Landing Limits")]
    public float maxVerticalSpeed = 1.5f;
    public float maxHorizontalSpeed = 2f;
    public float maxTiltAngle = 12f;

    [Header("Validation")]
    public float requiredStableTime = 1.0f;

    [Header("State")]
    public bool landed = false;

    private Rigidbody2D rocketRb;
    private RocketBreakApart rocketBreak;
    private ControlRocket controlRocket;

    private bool checkingLanding = false;
    private float stableTimer = 0f;

    void Start()
    {
        rocketRb = GetComponentInParent<Rigidbody2D>();
        rocketBreak = GetComponentInParent<RocketBreakApart>();
        controlRocket = GetComponentInParent<ControlRocket>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (landed || rocketRb == null) return;

        // must be landing pad
        if (!other.CompareTag("LandingPadType"))
        {
            Crash();
            return;
        }

        // start validation period
        checkingLanding = true;
        stableTimer = 0f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // left the pad → fail
        if (checkingLanding && !landed)
        {
            Crash();
        }
    }

    void Update()
    {
        if (!checkingLanding || landed || rocketRb == null) return;

        float verticalSpeed = Mathf.Abs(rocketRb.linearVelocity.y);
        float horizontalSpeed = Mathf.Abs(rocketRb.linearVelocity.x);

        float z = transform.root.eulerAngles.z;
        if (z > 180f) z -= 360f;

        float tilt = Mathf.Abs(z);

        bool valid =
            verticalSpeed <= maxVerticalSpeed &&
            horizontalSpeed <= maxHorizontalSpeed &&
            tilt <= maxTiltAngle;

        if (!valid)
        {
            Crash();
            return;
        }

        // still valid → accumulate time
        stableTimer += Time.deltaTime;

        if (stableTimer >= requiredStableTime)
        {
            SuccessfulLanding();
        }
    }

    void SuccessfulLanding()
    {
        landed = true;
        checkingLanding = false;

        print("successful landing");

        if (controlRocket != null)
        {
            controlRocket.throttle = controlRocket.throttleMin;
        }
        rocketRb.angularVelocity = 0f;

        Invoke(nameof(LoadNextScene), 1.0f);
    }

    void Crash()
    {
        if (landed) return;

        checkingLanding = false;

        print("crashed");

        if (rocketBreak != null)
            rocketBreak.BreakApart();

        Invoke(nameof(ReloadScene), 1.5f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
        else
            Debug.Log("No next scene.");
    }
}