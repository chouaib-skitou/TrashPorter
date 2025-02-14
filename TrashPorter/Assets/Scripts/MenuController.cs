/*using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("Quit button pressed. Exiting application...");
        Application.Quit();

        // Note: Application.Quit() will only work in a built application.
        // In the Unity Editor, you can simulate this behavior using the following:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}*/


// Script de contrôle du menu principal avec fonction de quitter l'application
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

using System.Collections.Generic;
using System.Collections;

public class MenuController : MonoBehaviour
{
    // Fonction pour quitter l'application
    public void QuitGame()
    {
        Debug.Log("Quitter : Fermeture de l'application...");
        Application.Quit();
    }
}