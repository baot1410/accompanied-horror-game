using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashlightSwitch : MonoBehaviour
{
    [Header("Light Settings")]
    public GameObject light;                 // The light source object
    
    [Header("Audio Settings")]
    public AudioSource lightSwitchAudio;     // Reference to the light switch audio source
    public AudioSource batteryReloadAudio;   // Reference to the battery reload audio source

    [Header("Battery Settings")]
    public float batteryLifeInSeconds = 60f; // How long the battery lasts in seconds (60 = 1 minute)
    public float lowBatteryThreshold = 20f;  // Percentage at which the light starts flickering (set to around 20)
    
    [Header("UI Elements")]
    public TextMeshProUGUI batteryText;      // Optional text to show battery percentage
    public GameObject batteryDepletedUI;     // UI object to show when battery is depleted

    private bool isLightOn = false;
    private float batteryPercentage = 100f;
    private float timeElapsed = 0f;
    private bool isBatteryDead = false;
    private float flickerTime = 0f;
    
    void Start()
    {
        // Start with the light off
        light.SetActive(false);
        isLightOn = false;
        
        // Initialize battery to full
        batteryPercentage = 100f;
        timeElapsed = 0f;
        flickerTime = 0f;
        
        // Hide battery depleted UI at start
        if (batteryDepletedUI != null)
        {
            batteryDepletedUI.SetActive(false);
        }
        
        UpdateBatteryUI();
    }

    void Update()
    {
        // Check for key press to toggle the light
        if (Input.GetKeyDown(KeyCode.Q) && !isBatteryDead)
        {
            ToggleLight();
        }
        
        // If light is on, drain the battery
        if (isLightOn && !isBatteryDead)
        {
            DrainBattery();
            
            // Make light flicker when battery is low
            if (batteryPercentage <= lowBatteryThreshold)
            {
                FlickerLight();
                
                // Debug log to help verify the flickering logic is being called
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Battery: " + batteryPercentage + "%, Flicker active");
                }
            }
        }
    }
    
    void DrainBattery()
    {
        // Calculate how much battery to drain this frame
        timeElapsed += Time.deltaTime;
        batteryPercentage = Mathf.Max(0, 100 - (timeElapsed / batteryLifeInSeconds * 100));
        
        // Update the UI
        UpdateBatteryUI();
        
        // Check if battery is fully drained
        if (batteryPercentage <= 0)
        {
            BatteryDied();
        }
    }
    
    void FlickerLight()
    {
        // Simple flickering effect - the lower the battery, the more frequent
        flickerTime += Time.deltaTime;
        
        // The lower the battery percentage, the faster the flicker
        // When at exactly lowBatteryThreshold, flicker rate is 2f
        // When at 0%, flicker rate is 0.1f
        float batteryRatio = Mathf.Clamp01(batteryPercentage / lowBatteryThreshold);
        float flickerRate = Mathf.Lerp(0.1f, 2f, batteryRatio);
        
        if (flickerTime >= flickerRate)
        {
            // Toggle the light state
            light.SetActive(!light.activeSelf);
            flickerTime = 0f;
            
            // Make sure to turn light back on after a very short time if we turned it off
            if (!light.activeSelf)
            {
                Invoke("TurnLightBackOn", 0.05f);
            }
        }
    }
    
    void TurnLightBackOn()
    {
        if (isLightOn && !isBatteryDead)
        {
            light.SetActive(true);
        }
    }
    
    void BatteryDied()
    {
        isBatteryDead = true;
        isLightOn = false;
        light.SetActive(false);
        
        // Show the battery depleted UI for a second
        if (batteryDepletedUI != null)
        {
            batteryDepletedUI.SetActive(true);
            Invoke("HideBatteryDepletedUI", 1.0f);
        }
    }
    
    void HideBatteryDepletedUI()
    {
        if (batteryDepletedUI != null)
        {
            batteryDepletedUI.SetActive(false);
        }
    }
    
    void UpdateBatteryUI()
    {
        // Update text if available
        if (batteryText != null)
        {
            batteryText.text = "Battery: " + batteryPercentage.ToString("F0") + "%";
            
            // Change text color based on battery level
            if (batteryPercentage <= 20)
            {
                batteryText.color = Color.red;
            }
            else if (batteryPercentage <= 50)
            {
                batteryText.color = Color.yellow;
            }
            else
            {
                batteryText.color = Color.green;
            }
        }
    }

    void ToggleLight()
    {
        // Invert the current state
        isLightOn = !isLightOn;
        
        // Update light state
        light.SetActive(isLightOn);
        
        // Play the switch sound
        if (lightSwitchAudio != null)
        {
            lightSwitchAudio.Play();
        }
    }
    
    // Public method to reset the battery (e.g., when player finds new batteries)
    public void ReplaceBattery()
    {
        timeElapsed = 0f;
        batteryPercentage = 100f;
        isBatteryDead = false;
        flickerTime = 0f;
        UpdateBatteryUI();

        // Hide the battery depleted UI if it's showing
        if (batteryDepletedUI != null && batteryDepletedUI.activeSelf)
        {
            batteryDepletedUI.SetActive(false);
        }

        // Play the reload sound
        if (batteryReloadAudio != null)
        {
            batteryReloadAudio.Play();
        }
    }
}