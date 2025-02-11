using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Import this for UI Text

public class DoorClickHandler : MonoBehaviour
{
    [SerializeField] private string nextSceneName;  // Next scene name
    [SerializeField] private float delayTime = 10f; // Delay before the door appears (in seconds)
    [SerializeField] private Text levelMessageText; // Reference to the UI Text
    [SerializeField] private AudioSource doorSound;  // Reference to AudioSource

    private Renderer doorRenderer;
    private Light doorLight;

    void Start()
    {
        doorRenderer = GetComponent<Renderer>();
        doorLight = GetComponentInChildren<Light>();  // Make sure you have a light child attached to the door
        doorRenderer.enabled = false;  // Hide the door initially
        doorLight.enabled = false;     // Light is off initially
        levelMessageText.enabled = false;  // Hide the level message initially
        StartCoroutine(DelayDoorAppearance());  // Start the coroutine to show the door after a delay
    }

    private IEnumerator DelayDoorAppearance()
    {
        // Wait for the delay time before showing the door and light
        yield return new WaitForSeconds(delayTime);
        
        doorRenderer.enabled = true;  // Show the door
        doorLight.enabled = true;     // Turn on the light around the door
        levelMessageText.enabled = true;  // Show the "Next Level" message

        if (doorSound != null)
        {
            doorSound.Play();
        }
        Debug.Log("The door is now visible!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))  // Check if the object colliding with the door is the camera (MainCamera)
        {
            Debug.Log("Player (Camera) has entered the door! Moving to next level...");
            LoadNextScene();
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click or trackpad tap
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // If the door is clicked
                {
                    LoadNextScene();
                }
            }
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set in the Inspector!");
        }
    }
}
