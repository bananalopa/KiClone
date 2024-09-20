using UnityEngine;

using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    readonly Vector3 flippedScale = new Vector3(-1, 1, 1);
    readonly Quaternion flippedRotation = new Quaternion(0, 0, 1, 0);

    [Header("Character")]
    [SerializeField] Animator animator = null;
    [SerializeField] Transform puppet = null;
    //[SerializeField] CharacterAudio audioPlayer = null;

    [Header("Equipment")]
    [SerializeField] Transform handAnchor = null;

    [Header("Movement")]
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float minFlipSpeed = 0.1f;
    
    private Rigidbody2D controllerRigidbody;

    private Vector2 movementInput;
    private bool jumpInput;

    private Vector2 prevVelocity;
    private bool isFlipped;
    private bool isJumping;
    private bool isFalling;
    
    private int animatorRunningSpeed;

    public bool CanMove { get; set; }

    void Start()
    {
#if UNITY_EDITOR
        if (Keyboard.current == null)
        {
            var playerSettings = new UnityEditor.SerializedObject(Resources.FindObjectsOfTypeAll<UnityEditor.PlayerSettings>()[0]);
            var newInputSystemProperty = playerSettings.FindProperty("enableNativePlatformBackendsForNewInputSystem");
            bool newInputSystemEnabled = newInputSystemProperty != null ? newInputSystemProperty.boolValue : false;

            if (newInputSystemEnabled)
            {
                var msg = "New Input System backend is enabled but it requires you to restart Unity, otherwise the player controls won't work. Do you want to restart now?";
                if (UnityEditor.EditorUtility.DisplayDialog("Warning", msg, "Yes", "No"))
                {
                    UnityEditor.EditorApplication.ExitPlaymode();
                    var dataPath = Application.dataPath;
                    var projectPath = dataPath.Substring(0, dataPath.Length - 7);
                    UnityEditor.EditorApplication.OpenProject(projectPath);
                }
            }
        }
#endif

        controllerRigidbody = GetComponent<Rigidbody2D>();
        animatorRunningSpeed = Animator.StringToHash("RunningSpeed");
        CanMove = true;
    }

    void Update()
    {
        var keyboard = Keyboard.current;

        if (!CanMove || keyboard == null)
            return;

        // Horizontal movement
        float moveHorizontal = 0.0f;

        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
            moveHorizontal = -1.0f;
        else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
            moveHorizontal = 1.0f;

        movementInput = new Vector2(moveHorizontal, 0);

        // Jumping input
        if (!isJumping && keyboard.spaceKey.wasPressedThisFrame)
            jumpInput = true;
    }

    void FixedUpdate()
    {
        UpdateVelocity();
        UpdateDirection();
        prevVelocity = controllerRigidbody.linearVelocity;
    }

    private void UpdateVelocity()
    {
        Vector2 velocity = controllerRigidbody.linearVelocity;

        // Apply acceleration directly as we'll want to clamp
        // prior to assigning back to the body.
        velocity += movementInput * acceleration * Time.fixedDeltaTime;

        // We've consumed the movement, reset it.
        movementInput = Vector2.zero;

        // Clamp horizontal speed.
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        // Assign back to the body.
        controllerRigidbody.linearVelocity = velocity;

        // Update animator running speed
        var horizontalSpeedNormalized = Mathf.Abs(velocity.x) / maxSpeed;
        animator.SetFloat(animatorRunningSpeed, horizontalSpeedNormalized);

        // Play audio
        //audioPlayer.PlaySteps(groundType, horizontalSpeedNormalized);
    }
    
    private void UpdateDirection()
    {
        // Use scale to flip character depending on direction
        if (controllerRigidbody.linearVelocity.x > minFlipSpeed && isFlipped)
        {
            isFlipped = false;
            puppet.localScale = Vector3.one;
        }
        else if (controllerRigidbody.linearVelocity.x < -minFlipSpeed && !isFlipped)
        {
            isFlipped = true;
            puppet.localScale = flippedScale;
        }
    }
}
