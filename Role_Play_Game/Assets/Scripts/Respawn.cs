using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Important objects.
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    
    /// <summary>
    /// Respawn player.
    /// </summary>
    /// <param name="other">Player.</param>
    private void OnCollisionEnter(Collision other)
    {
        // Is the other collided object a player?
        if (other.transform == player) // Yes.
        {
            // Clear physics speed.
            player.GetComponent<Rigidbody>().Sleep();

            // Respawn player.
            player.position = respawnPoint.position;
            player.rotation = respawnPoint.rotation;
        }
    }
}