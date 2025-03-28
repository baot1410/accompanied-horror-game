using UnityEngine;

public class Flashlight_Interaction : MonoBehaviour, IInteractable
{
    public GameObject pickUpText;
    public GameObject FlashLight;
    public GameObject Arm;

    void Start()
    {
        pickUpText.SetActive(false);
        FlashLight.SetActive(false);
        Arm.SetActive(false);
    }

    // This method will be called by the Interactor when the ray hits this object
    public void Interact()
    {
    //     if (FlashLight.activeSelf) {
    //         Battery.SetActive(false);
    //         // FlashLight.ReplaceBattery();
    //     } else {
    //         gameObject.SetActive(false);
    //         FlashLight.SetActive(true);
    //         Arm.SetActive(true);
    //         pickUpText.SetActive(false);
    //     }
        gameObject.SetActive(false);
        FlashLight.SetActive(true);
        Arm.SetActive(true);
        pickUpText.SetActive(false);

    }


    // Show the pickup text when player is looking at the flashlight
    public void ShowInteractPrompt(bool show)
    {
        pickUpText.SetActive(show);
    }
}

// Interface that your interactable objects will implement
public interface IInteractable
{
    void Interact();
    void ShowInteractPrompt(bool show);
}