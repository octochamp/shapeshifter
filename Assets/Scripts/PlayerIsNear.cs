using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public float proximityRadius = 5f; // Adjust this value to set the desired proximity radius
    public Animator animator;
    public GameObject playerObject; // Reference to the GameObject representing the player's position
    private bool isPlayerNear;

    void Update()
    {
        // Get the global position of the target GameObject
        Vector3 targetGlobalPosition = transform.parent == null ? transform.position : transform.parent.TransformPoint(transform.localPosition);

        // Check if the player is near the GameObject
        isPlayerNear = Physics.CheckSphere(transform.position, proximityRadius, LayerMask.GetMask("Player"));

        // Set the boolean parameter in the Animator to trigger the animation state transition
       // Debug.Log("isPlayerNear: " + isPlayerNear);
       // Debug.Log("Player Position: " + playerObject.transform.position);
       // Debug.Log("GameObject Global Position: " + targetGlobalPosition);
        animator.SetBool("IsPlayerNear", isPlayerNear);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wireframe sphere to visualize the proximity radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
    }
}