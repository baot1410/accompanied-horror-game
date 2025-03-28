using UnityEngine;

public class StopFollowingOnCollision : MonoBehaviour
{
    public AIMovement monster;  // Reference to the monster's AI or follow script
    public string playerTag = "Player";  // Tag for the player

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the player
        if (other.CompareTag(playerTag))
        {
            // Stop the monster from following the player
            AIMovement monsterFollow = monster.GetComponent<AIMovement>();
            if (monsterFollow != null)
            {
                monsterFollow.StopFollowing();
            }
        }
    }
}
