using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    // Components.
    private Rigidbody physics;
    
    // Attributes.
    [SerializeField] private float speed;

    /// <summary>
    /// Initializing.
    /// </summary>
    private void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Movement.
    /// </summary>
    private void FixedUpdate()
    {
        // Get values.
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        
        // Write movement.
        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Increase speed for running on shift click.
        speed = Input.GetKey(KeyCode.LeftShift) ? 20 : 10;

        // Is space clicked.
        if (Input.GetKey(KeyCode.Space)) // Yes.
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
        else // No.
        {
            // Disable physics freezes.
            physics.constraints = RigidbodyConstraints.None;
            
            // Move.
            physics.AddForce(movement * speed);
        }
    }
}