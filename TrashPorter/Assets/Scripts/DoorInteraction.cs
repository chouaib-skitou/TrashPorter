using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    void OnMouseDown()
    {
        // This method is called when the user clicks on the door
        Debug.Log("Door clicked! Switching to Level 2...");
        SceneManager.LoadScene("SecondLevel"); // Replace "Level2" with the name of your scene
    }
}
