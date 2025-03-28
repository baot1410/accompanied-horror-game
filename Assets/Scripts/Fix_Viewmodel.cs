using UnityEngine;

public class Fix_Viewmodel : MonoBehaviour
{
    private Vector3 originalScale;
    
    void Start()
    {
        originalScale = transform.localScale;
    }
    
    void LateUpdate()
    {
        // Always reset to original scale to prevent inheritance of parent scaling
        transform.localScale = originalScale;
    }
}