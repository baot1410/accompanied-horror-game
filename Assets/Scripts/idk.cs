// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SocialPlatforms.Impl;

// public class FirstPersonController : MonoBehaviour
// {
//     public GameObject player;
//     public GameObject monster;

//     public GameObject win_text_object;
//     public GameObject lose_text_object;
//     public GameObject play_again_object;

//     public float walk_speed = 5f;
//     public float run_speed = 10f;
//     public float crouch_speed = 3f;
//     public Transform camera_transform;
//     public float mouse_sensitivity = 1.6f;
    
//     public float slope_limit = 45f;
//     public float step_offset = 0.1f;

//     private CharacterController controller;
//     private Vector3 player_velocity;
//     private Crouching crouching_script;

//     void Start()
//     {
//         win_text_object.SetActive(false);
//         lose_text_object.SetActive(false);
//         play_again_object.SetActive(false);

//         controller = GetComponent<CharacterController>();
//         crouching_script = GetComponent<Crouching>();
        
//         crouch_speed = walk_speed / 2.0f;
        
//         controller.stepOffset = step_offset;
//         controller.slopeLimit = slope_limit;

//         Cursor.lockState = CursorLockMode.Locked;
//     }

//     void Reset()
//     {
//         win_text_object.SetActive(false);
//         lose_text_object.SetActive(false);
//         play_again_object.SetActive(false);

//         walk_speed = 5f;
//         run_speed = 10f;
//         crouch_speed = 3f;

//         controller.enabled = false;
//         player.transform.position = new Vector3(-33.83f, 1.185f, -27.583f);
//         player_velocity = Vector3.zero;
//         controller.enabled = true;
//     }

//     void Update()
//     {
//         float move_speed = walk_speed;
        
//         if (crouching_script != null)
//         {
//             bool is_crouching = crouching_script.IsCrouching();
            
//             if (is_crouching)
//             {
//                 move_speed = crouch_speed;
//             }
//             else if (Input.GetKey(KeyCode.LeftShift))
//             {
//                 move_speed = run_speed;
//             }
//         }
        
//         float horizontal = Input.GetAxis("Horizontal");
//         float vertical = Input.GetAxis("Vertical");
        
//         Vector3 move = transform.right * horizontal + transform.forward * vertical;
//         controller.Move(move * move_speed * Time.deltaTime);

//         player_velocity.y += Physics.gravity.y * Time.deltaTime;
//         controller.Move(player_velocity * Time.deltaTime);

//         if (Input.GetButtonDown("Jump") && play_again_object.activeSelf)
//         {
//             Reset();
//         }

//         float mouse_x = Input.GetAxis("Mouse X") * mouse_sensitivity;
//         float mouse_y = Input.GetAxis("Mouse Y") * mouse_sensitivity;
//         transform.Rotate(Vector3.up * mouse_x);

//         Vector3 current_rotation = camera_transform.rotation.eulerAngles;
//         float desired_rotation_x = current_rotation.x - mouse_y;
//         if (desired_rotation_x > 180)
//             desired_rotation_x -= 360;
//         desired_rotation_x = Mathf.Clamp(desired_rotation_x, -90f, 90f);
//         camera_transform.rotation = Quaternion.Euler(desired_rotation_x, current_rotation.y, current_rotation.z);
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("WinBox"))
//         {
//             win_text_object.SetActive(true);
//             play_again_object.SetActive(true);
//         }
//         else if (other.gameObject.CompareTag("Monster"))
//         {
//             lose_text_object.SetActive(true);
//             play_again_object.SetActive(true);
//             walk_speed = 0f;
//             run_speed = 0f;
//             crouch_speed = 0f;
//         }
//     }
// }






