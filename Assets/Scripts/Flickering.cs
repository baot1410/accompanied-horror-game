using UnityEngine;

public class Flickering : MonoBehaviour
{
    public Light lightOb;
    
    public AudioSource lightSound;
    
    public float minTime;
    public float maxTime;
    public float timer;
    
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }
    
    void Update()
    {
        LightsFlickering();
    }
    
    void LightsFlickering()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
            
        if(timer <= 0)
        {
            lightOb.enabled = !lightOb.enabled;
            timer = Random.Range(minTime, maxTime);
            lightSound.Play();
        }
    }
}
