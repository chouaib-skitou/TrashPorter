using UnityEngine;

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
}
