using UnityEngine;

public class FlashlightBob : MonoBehaviour
{
    private float horizontalBobAmount = 0.05f;  // How much the flashlight bobs horizontally
    private float verticalBobAmount = 0.03f;    // How much the flashlight bobs vertically
    private float horizontalBobSpeed = 4.0f;    // Speed of horizontal bobbing
    private float verticalBobSpeed = 2.0f;      // Speed of vertical bobbing
    
    private float idleBobAmount = 0.01f;        // Smaller bob amount when not moving
    private float moveThreshold = 0.1f;         // Minimum input to be considered moving
    private float bobTransitionSpeed = 5.0f;    // How fast to transition between idle and moving bob
    
    private Vector3 initialPosition;  // The flashlight's initial local position
    private float timer = 0f;         // Timer for bob effect
    private float currentHorizontalAmount;  // Current horizontal bob amount (for smooth transitions)
    private float currentVerticalAmount;    // Current vertical bob amount (for smooth transitions)
    
    void Start()
    {
        // Store the initial position
        initialPosition = transform.localPosition;
        
        // Initialize current amounts
        currentHorizontalAmount = idleBobAmount;
        currentVerticalAmount = idleBobAmount;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        // if player is moving using WASD (or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(horizontalInput) > moveThreshold || Mathf.Abs(verticalInput) > moveThreshold;
        
        // target bob amounts based on movement
        float targetHorizontalAmount = isMoving ? horizontalBobAmount : idleBobAmount;
        float targetVerticalAmount = isMoving ? verticalBobAmount : idleBobAmount;
        
        
        currentHorizontalAmount = Mathf.Lerp(currentHorizontalAmount, targetHorizontalAmount, Time.deltaTime * bobTransitionSpeed);
        currentVerticalAmount = Mathf.Lerp(currentVerticalAmount, targetVerticalAmount, Time.deltaTime * bobTransitionSpeed);
        
        // calculate the bob offsets
        float xOffset = Mathf.Sin(timer * horizontalBobSpeed) * currentHorizontalAmount;
        float yOffset = Mathf.Sin(timer * verticalBobSpeed) * currentVerticalAmount;
        
        Vector3 bobPosition = initialPosition + new Vector3(xOffset, yOffset, 0f);
        transform.localPosition = bobPosition;
    }
}