/*
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
*/

// Script pour gérer l'interaction avec une porte permettant de passer au niveau suivant
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Import pour les composants UI Text

public class DoorClickHandler : MonoBehaviour
{
    // Nom de la scène suivante à charger
    [SerializeField] private string nextSceneName;
    // Délai avant l'apparition de la porte (en secondes)
    [SerializeField] private float delayTime = 10f;
    // Texte pour afficher le message de changement de niveau
    [SerializeField] private Text levelMessageText;
    // Composant audio pour jouer un son lors de l'apparition
    [SerializeField] private AudioSource doorSound;

    private Renderer doorRenderer; // Renderer de la porte (affichage)
    private Light doorLight;       // Lumière attachée à la porte

    // Initialisation des composants et masquage initial
    void Start()
    {
        doorRenderer = GetComponent<Renderer>();
        doorLight = GetComponentInChildren<Light>();
        doorRenderer.enabled = false;
        doorLight.enabled = false;
        levelMessageText.enabled = false;
        StartCoroutine(DelayDoorAppearance());
    }

    // Coroutine pour afficher la porte après un délai
    private IEnumerator DelayDoorAppearance()
    {
        yield return new WaitForSeconds(delayTime);
        doorRenderer.enabled = true;
        doorLight.enabled = true;
        levelMessageText.enabled = true;

        if (doorSound != null)
        {
            doorSound.Play();
        }
        Debug.Log("La porte est maintenant visible !");
    }

    // Détection d'entrée dans la zone de collision (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) // Vérifie si l'objet entrant est la caméra principale
        {
            Debug.Log("La caméra est passée par la porte. Changement de niveau...");
            LoadNextScene();
        }
    }

    // Vérifie si la porte a été cliquée et charge la scène suivante
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche de souris
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                LoadNextScene();
            }
        }
    }

    // Charge la scène suivante
    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Le nom de la scène suivante n'est pas défini dans l'inspecteur !");
        }
    }
}