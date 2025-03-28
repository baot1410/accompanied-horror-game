using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    [Range(0.001f, 0.05f)]
    public float Amount = 0.03f;
    
    [Range(1f, 30f)]
    public float Frequency = 10.0f;

    [Range(10f, 100f)]
    public float Smooth = 10.0f;

    [Range(1f, 5f)]
    public float VerticalBobMultiplier = 1.3f;
    
    [Range(1f, 5f)]
    public float HorizontalBobMultiplier = 2.0f;
    
    [Range(1.0f, 2.0f)]
    public float RunningMultiplier = 1.5f; // Multiplier for when player is running

    Vector3 StartPos;
    private bool isWalking = false;
    private bool isRunning = false;
    private float movementSpeed = 0f;

    void Start() {
        StartPos = transform.localPosition;
    }

    void Update() {
        CheckForHeadbobTrigger();
        
        if (isWalking) {
            // Apply the head bob
            transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos + StartHeadBob(), Smooth * Time.deltaTime);
        } else {
            // Return to center when not moving
            transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, Smooth * Time.deltaTime);
        }
    }

    private void CheckForHeadbobTrigger() {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementSpeed = input.magnitude;
        isWalking = movementSpeed > 0.1f;
        isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;
    }

    private Vector3 StartHeadBob() {
        Vector3 pos = Vector3.zero;
        
        // Apply running multiplier when sprinting
        float currentMultiplier = isRunning ? RunningMultiplier : 1.0f;
        
        pos.y = Mathf.Sin(Time.time * Frequency) * Amount * VerticalBobMultiplier * movementSpeed * currentMultiplier;
        pos.x = Mathf.Cos(Time.time * Frequency / 2f) * Amount * HorizontalBobMultiplier * movementSpeed * currentMultiplier;
        
        return pos;
    }
}