using UnityEngine;

public class TrashZone : MonoBehaviour
{
    public string targetTag = "Grabbable"; // Tag for objects that can be trashed
    public int scoreValue = 1; // Points to add for each object

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Destroy the object
            Destroy(other.gameObject);

            // Add points to the score
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }

            // Optionally: Add feedback like sound or particles
            Debug.Log("Object trashed!");
        }
    }
}
