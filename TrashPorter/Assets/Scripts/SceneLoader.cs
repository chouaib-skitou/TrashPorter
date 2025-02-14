/*using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SecondScene"); // Loads the scene by name
    }
}*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Fonction pour charger la scène suivante
    public void LoadNextScene()
    {
        // Charge la scène nommée "SecondScene"
        SceneManager.LoadScene("SecondScene"); 
        // Il est important de s'assurer que "SecondScene" est bien le nom exact de la scène dans la gestion de scènes de Unity
    }
}
