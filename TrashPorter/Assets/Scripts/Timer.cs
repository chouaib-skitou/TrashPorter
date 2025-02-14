/*using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Ensure you have this for TextMeshPro

public class Timer : MonoBehaviour
{
    public Text txtTime, txtGameOver; // TextMeshProUGUI for displaying time


    public float gameDuration = 600f; // Time in seconds before pause (default: 10 minutes)
    public float pauseDuration = 10f; // Time in seconds for pause (default: 10 seconds)

    private float tps = 0;
    private bool isPaused = false;

    void Start()
    {
        
        txtTime = GameObject.Find("timerText")?.GetComponent<Text>();
        txtTime.text = "00:00";
        txtGameOver = GameObject.Find("gameOverText")?.GetComponent<Text>();
        txtGameOver.text = "Game Over !";
        txtGameOver.enabled = false;
        StartCoroutine(GameLoop());
    }

    void Update()
    {
        float remainingTime = gameDuration - tps; // Calculate remaining time
        if (remainingTime >= 0 && !isPaused )
        {
            tps += Time.deltaTime; // Increment the time passed

            
            int minutes = Mathf.FloorToInt(remainingTime / 60); // Get minutes
            int seconds = Mathf.FloorToInt(remainingTime % 60); // Get seconds

            // Debugging: log the values of minutes and seconds
            Debug.Log($"Remaining Time: {minutes} minutes and {seconds} seconds");

            // Display the time in MM:SS format with leading zeros
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(gameDuration); // Wait for the specified duration
        isPaused = true;
        Time.timeScale = 0; // Pause the game
        txtGameOver.enabled = true;
        Debug.Log($"Game paused for {pauseDuration} seconds...");
        yield return new WaitForSecondsRealtime(pauseDuration); // Wait in real-time

        Time.timeScale = 1; // Resume game
        isPaused = false;
        RestartGame();
    }

    void RestartGame()
    {
        Debug.Log("Restarting scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Assurez-vous d'avoir ce package pour TextMeshPro

public class Timer : MonoBehaviour
{
    // Références aux éléments UI pour afficher le temps et le message de fin de jeu
    public Text txtTime, txtGameOver; // Référence à TextMeshProUGUI pour afficher le temps et le message de fin de jeu

    // Paramètres du jeu : durée de la partie et pause
    public float gameDuration = 600f; // Durée du jeu en secondes (par défaut 10 minutes)
    public float pauseDuration = 10f; // Durée de la pause en secondes (par défaut 10 secondes)

    private float tps = 0; // Temps écoulé depuis le début du jeu
    private bool isPaused = false; // Indicateur pour savoir si le jeu est en pause

    // Méthode d'initialisation appelée au démarrage
    void Start()
    {
        // Récupère les références des objets UI (temps et GameOver) à partir de la scène
        txtTime = GameObject.Find("timerText")?.GetComponent<Text>(); // Récupère le composant Text de l'objet timerText
        txtTime.text = "00:00"; // Initialise le texte du timer à "00:00"
        
        txtGameOver = GameObject.Find("gameOverText")?.GetComponent<Text>(); // Récupère le composant Text de l'objet gameOverText
        txtGameOver.text = "Game Over !"; // Définit le message de fin de jeu
        txtGameOver.enabled = false; // Cache le message Game Over au début du jeu

        // Démarre la coroutine du GameLoop (la boucle principale du jeu)
        StartCoroutine(GameLoop());
    }

    // Mise à jour du timer à chaque frame
    void Update()
    {
        float remainingTime = gameDuration - tps; // Calcule le temps restant
        if (remainingTime >= 0 && !isPaused) // Continue si le temps restant est positif et si le jeu n'est pas en pause
        {
            tps += Time.deltaTime; // Incrémente le temps écoulé depuis le début du jeu

            // Calcule les minutes et les secondes restantes
            int minutes = Mathf.FloorToInt(remainingTime / 60); // Minutes restantes
            int seconds = Mathf.FloorToInt(remainingTime % 60); // Secondes restantes

            // Debug: Affiche le temps restant dans la console
            Debug.Log($"Remaining Time: {minutes} minutes and {seconds} seconds");

            // Affiche le temps restant sous le format MM:SS avec des zéros au début si nécessaire
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    // Coroutine principale du jeu qui gère la fin de la partie et la pause
    IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(gameDuration); // Attend la durée de la partie avant de faire une pause
        isPaused = true; // Marque le jeu comme étant en pause
        Time.timeScale = 0; // Met le temps du jeu à 0 pour arrêter la simulation
        txtGameOver.enabled = true; // Affiche le message "Game Over" à la fin du jeu
        Debug.Log($"Game paused for {pauseDuration} seconds..."); // Affiche un message de debug concernant la pause

        // Attend pendant la durée de la pause en temps réel (Time.timeScale = 0 signifie que le temps est arrêté pour le jeu)
        yield return new WaitForSecondsRealtime(pauseDuration);

        Time.timeScale = 1; // Reprend le jeu à une vitesse normale
        isPaused = false; // Le jeu n'est plus en pause
        RestartGame(); // Redémarre le jeu
    }

    // Méthode pour redémarrer la scène actuelle après la pause
    void RestartGame()
    {
        Debug.Log("Restarting scene..."); // Affiche un message de redémarrage dans la console
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharge la scène actuelle
    }
}




