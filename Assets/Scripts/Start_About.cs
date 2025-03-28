using UnityEngine;
using TMPro;
using System.Collections;

public class TimedAboutScreen : MonoBehaviour
{
    public TextMeshProUGUI aboutText;
    
    public GameObject aboutContainer;

    public AudioSource page_flip;
    
    // Time to wait before showing the about screen (in seconds)
    public float delayBeforeShowing = 2.0f;
    
    private bool isShowingAbout = false;

    void Start()
    {
       
        
        // Hide the about screen initially
        aboutContainer.SetActive(false);
        aboutText.gameObject.SetActive(false);
        
        // Start the timer to show the about screen
        StartCoroutine(ShowAboutAfterDelay());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isShowingAbout)
        {
            HideAbout();
        }
    }

    private IEnumerator ShowAboutAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeShowing);
        
        ShowAbout();
    }

    public void ShowAbout()
    {
        aboutContainer.SetActive(true);
        aboutText.gameObject.SetActive(true);
        page_flip.Play();
        isShowingAbout = true;
        //Debug.Log("About screen shown");
    }

    public void HideAbout()
    {
        aboutContainer.SetActive(false);
        aboutText.gameObject.SetActive(false);
        isShowingAbout = false;
    }
}