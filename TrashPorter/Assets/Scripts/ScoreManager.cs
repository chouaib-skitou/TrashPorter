/*using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Instance statique pour le Singleton
    public static int totalScore = 0; // Score global
    public Text scoreText; // Texte pour afficher le score

    void Awake()
    {
        // Vérifie si une autre instance existe déjà
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Si une autre instance existe, détruire ce GameObject
        }
        else
        {
            Instance = this; // Assigner cette instance comme le Singleton
            DontDestroyOnLoad(gameObject); // Ne pas détruire l'objet lors du changement de scène
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        totalScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {totalScore}"; // Mise à jour du texte du score
    }
}
*/

using UnityEngine;
using TMPro; // Référence au package TextMesh Pro pour l'affichage du texte
using UnityEngine.UI; // Référence au système d'UI de Unity

public class ScoreManager : MonoBehaviour
{
    // Instance statique pour gérer le Singleton de ScoreManager
    public static ScoreManager Instance; // Instance unique du ScoreManager
    public static int totalScore = 0; // Score global, accessible depuis toute l'application
    public Text scoreText; // Référence au composant Text (ou TMP_Text) pour afficher le score à l'écran

    // Méthode appelée lors de l'initialisation de l'objet
    void Awake()
    {
        // Vérifie si une autre instance existe déjà (Singleton pattern)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Si une autre instance existe, détruire cet objet
        }
        else
        {
            Instance = this; // Assigner cette instance comme le Singleton
            DontDestroyOnLoad(gameObject); // Garder cet objet même lors des changements de scène
        }
    }

    // Méthode appelée au démarrage du jeu
    void Start()
    {
        UpdateScoreText(); // Met à jour l'affichage du score au début
    }

    // Méthode pour ajouter des points au score global
    public void AddScore(int points)
    {
        totalScore += points; // Ajoute les points passés en paramètre au score global
        UpdateScoreText(); // Met à jour l'affichage du score après ajout
    }

    // Méthode pour mettre à jour le texte affichant le score
    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {totalScore}"; // Affiche le score global dans le composant Text
    }
}
