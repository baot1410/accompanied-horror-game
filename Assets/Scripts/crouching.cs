using UnityEngine;

public class SimpleCrouch : MonoBehaviour
{
    [Header("Crouch Settings")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float standingHeight = 1.8f;
    public float crouchHeight = 0.9f;
    public float crouchSpeed = 10f;
    
    [Header("References")]
    public Transform cameraTransform;
    public Transform viewModelTransform;
    
    [Header("View Model Settings")]
    [Range(0.0f, 1.0f)]
    public float viewModelCrouchFactor = 0.3f;
    
    [Header("Movement Settings")]
    [Range(0.1f, 1.0f)]
    public float crouchSpeedMultiplier = 0.5f;
    
    private bool isCrouched = false;
    private Vector3 originalCameraPos;
    private Vector3 originalViewModelPos;
    private FirstPersonController fpsController;
    private float originalWalkSpeed;
    private float originalSprintSpeed;
    
    void Start()
    {
        // Get references
        if (cameraTransform != null)
            originalCameraPos = cameraTransform.localPosition;
            
        if (viewModelTransform != null)
            originalViewModelPos = viewModelTransform.localPosition;
        
        // Find the controller script
        fpsController = GetComponent<FirstPersonController>();
        
        // Store the original speeds
        if (fpsController != null)
        {
            originalWalkSpeed = fpsController.walkSpeed;
            originalSprintSpeed = fpsController.sprintSpeed;
        }
    }
    
    void Update()
    {
        // Toggle crouch on key press
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouched = !isCrouched;
            
            // Apply speed changes
            if (fpsController != null)
            {
                if (isCrouched)
                {
                    // Slow walk speed and disable sprint by setting sprint speed equal to walk speed
                    fpsController.walkSpeed = originalWalkSpeed * crouchSpeedMultiplier;
                    fpsController.sprintSpeed = fpsController.walkSpeed;
                }
                else
                {
                    // Restore original speeds
                    fpsController.walkSpeed = originalWalkSpeed;
                    fpsController.sprintSpeed = originalSprintSpeed;
                }
            }
        }
        
        // Force cancel sprinting if currently crouched
        if (isCrouched && fpsController != null && fpsController.sprintSpeed > fpsController.walkSpeed)
        {
            fpsController.sprintSpeed = fpsController.walkSpeed;
        }
        
        // Calculate positions
        float heightDifference = standingHeight - crouchHeight;
        
        // Move camera
        if (cameraTransform != null)
        {
            Vector3 targetCameraPos = originalCameraPos;
            targetCameraPos.y -= isCrouched ? heightDifference : 0;
            
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                targetCameraPos,
                Time.deltaTime * crouchSpeed
            );
        }
        
        // Move view model 
        if (viewModelTransform != null)
        {
            Vector3 targetViewPos = originalViewModelPos;
            targetViewPos.y -= isCrouched ? (heightDifference * viewModelCrouchFactor) : 0;
            
            viewModelTransform.localPosition = Vector3.Lerp(
                viewModelTransform.localPosition,
                targetViewPos,
                Time.deltaTime * crouchSpeed
            );
        }
    }
    
    // Public method to check if player is crouched
    public bool IsCrouched()
    {
        return isCrouched;
    }
}