using System;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    // Components.
    private Rigidbody physics;
    
    // Attributes.
    [SerializeField] private float speed;
    [SerializeField] private float stamina;
    private bool isBreathless;

    /// <summary>
    /// Initializing.
    /// </summary>
    private void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Output stamina.
        Debug.Log("Stamina: " + stamina);

        // Player moved?
        if (Math.Abs(Input.GetAxis("Horizontal")) <= 0.0f && Math.Abs(Input.GetAxis("Vertical")) <= 0.0f) // No.
        {
            // Have stamina the maximum value?
            if (stamina < 10.0f) // No.
            {
                // Get more stamina.
                stamina += speed / 1000.0f;
            }
        }
        else // Yes.
        {
            // Have stamina the minimum value?
            if (stamina > 0.0f)
            {
                // Increase stamina with consideration of crouch.
                stamina -= Input.GetKey(KeyCode.Space) ? speed / 1000000.0f : speed / 1000.0f;
            }
        }

        // Can the player move or not?
        isBreathless = stamina <= 0.0f;
    }

    /// <summary>
    /// Movement.
    /// </summary>
    private void FixedUpdate()
    {
        // No stamina anymore?
        if (isBreathless) // Yes.
        {
            // Disable movement.
            return;
        }
        
        // Get values.
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        // Write movement.
        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Left shift clicked?
        speed = Input.GetKey(KeyCode.LeftShift) ? 20 : 10;

        // Is space clicked.
        if (Input.GetKey(KeyCode.Space)) // Yes. [CROUCH]
        {
            // Crouch.
            var playerTransform = transform;
            
            // Set default rotation and freeze rotation.
            playerTransform.rotation = Quaternion.identity;
            physics.constraints = RigidbodyConstraints.FreezeRotation;

            // Add crouch speed and crouch player.
            var crouchSpeed = speed / 100;
            playerTransform.position += new Vector3(moveHorizontal * crouchSpeed * Time.deltaTime, 0.0f, moveVertical * crouchSpeed * Time.deltaTime);
        }
        else // No. [WALK | RUN]
        {
            // Disable physics freezes.
            physics.constraints = RigidbodyConstraints.None;
            
            // Move.
            physics.AddForce(movement * speed);
        }
    }
}