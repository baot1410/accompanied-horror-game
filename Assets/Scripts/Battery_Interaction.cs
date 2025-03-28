using UnityEngine;

public class Battery_Interaction : MonoBehaviour, IInteractable
{
    public GameObject pickUpText;
    public GameObject Battery;

    void Start()
    {
        pickUpText.SetActive(false);
        Battery.SetActive(true);
    }

    public void Interact()
    {
        // Find the flashlight script in the scene
        FlashlightSwitch flashlight = FindObjectOfType<FlashlightSwitch>();
        
        // Refill the battery
        if (flashlight != null)
        {
            flashlight.ReplaceBattery();
        }
        
        // Make the battery disappear
        Battery.SetActive(false);
    }

    // Show the pickup text when player is looking at the battery
    public void ShowInteractPrompt(bool show)
    {
        pickUpText.SetActive(show);
    }
}
