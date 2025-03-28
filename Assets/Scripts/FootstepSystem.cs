using System;
using UnityEngine;
using UnityEngine.Events;

public class FootstepSystem : MonoBehaviour
{
    FirstPersonController movingState;
    bool result;

    [Range(0, 20f)]
    public float freq = 10f;

    bool isTriggered = false;
    public AudioSource walking_footstep;

    float sine;

    void Start()
    {
        movingState = FindObjectOfType<FirstPersonController>(); // Find the existing instance
    }

    void Update()
{
    if (movingState != null)
    {
        result = movingState.checkSprint();
    }

    float input_size = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

    // Adjust freq dynamically
    freq = result ? (7.5f * (5f / 3f)) : 7.5f; // Scale by sprinting speed

    sine = Mathf.Sin(Time.time * freq);

    if (input_size > 0)
    {
        StartFootsteps();
    }
}


    public void StartFootsteps()
    {
        if (sine > 0.97f && isTriggered == false)
        {
            isTriggered = true;
            walking_footstep.Play();
        }
        else if (isTriggered && sine < -0.97f)
        {
            isTriggered = false;
        }
    }
}
