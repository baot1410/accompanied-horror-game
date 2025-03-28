using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 1f;
    
    private IInteractable lastInteractable;
    
    // void Start()
    // {
    //     // Initialize if needed
    // }
    
    void Update()
    {
        // Create a ray from the InteractorSource position forward
        Ray crosshair_ray = new Ray(InteractorSource.position, InteractorSource.forward);
        
        // Variable to store raycast hit information
        RaycastHit hitInfo;
        
        // Draw the ray in the scene view for debugging
        Debug.DrawRay(crosshair_ray.origin,crosshair_ray.direction * InteractRange, Color.green);
        
        // Check if we're looking at an interactable object
        if (Physics.Raycast(crosshair_ray, out hitInfo, InteractRange))
        {
            // Try to get an IInteractable component from the hit object
            IInteractable interactObj = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            
            // If we found an interactable object
            if (interactObj != null)
            {
                // show the interact prompt
                interactObj.ShowInteractPrompt(true);
                
                // Store reference to this interactable
                lastInteractable = interactObj;
                
                // If F key is pressed, interact with the object
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactObj.Interact();

                }
            }
            else
            {
                // Hide prompt on previous interactable if we're not looking at it anymore
                if (lastInteractable != null)
                {
                    lastInteractable.ShowInteractPrompt(false);
                    lastInteractable = null;
                }
            }
        }
        else
        {
            // Hide prompt on previous interactable if we're not looking at it anymore
            if (lastInteractable != null)
            {
                lastInteractable.ShowInteractPrompt(false);
                lastInteractable = null;
            }
        }
    }
}