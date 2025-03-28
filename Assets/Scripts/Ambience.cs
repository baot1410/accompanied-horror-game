using UnityEngine;

public class Ambience : MonoBehaviour
{
    public Collider area;
    public GameObject player;
    public AudioSource audioSource;
    
    void Start()
    {
            
        if (area != null && !area.isTrigger)
        {
            area.isTrigger = true;
        }
    }
    
    void Update()
    {
        if (area != null && player != null)
        {
            Vector3 closest_point = area.ClosestPoint(player.transform.position);
            transform.position = closest_point;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == player && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}