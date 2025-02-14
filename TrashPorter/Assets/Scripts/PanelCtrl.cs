using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.UI;


public class PanelCtrl : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup; // Reference to CanvasGroup for fading
    public float fadeDuration = 0.3f; // Fade duration
    private bool isMenuOpen = false;

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip openSound; // Sound when opening
    public AudioClip closeSound; // Sound when closing

    private InputDevice controller;

    private bool isButtonPressed = false;  // To track the button press

    void Start()
    {
        // Ensure menu starts hidden
        menuCanvasGroup.alpha = 0;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;

        controller = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        UpdateController(); // Check if controller is connected

        // Check if the menu button is pressed
        if (controller.TryGetFeatureValue(CommonUsages.menuButton, out bool leftButtonPressed) && leftButtonPressed)
        {
            // Only trigger the action if it wasn't pressed before
            if (!isButtonPressed)
            {
                isButtonPressed = true;
                ToggleMenu();
            }
        }
        else
        {
            isButtonPressed = false; // Reset the button press when it's not pressed
        }
    }

    void UpdateController()
    {
        if (!controller.isValid) // Only detect if controller is not valid
        {
            var inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, inputDevices);

            if (inputDevices.Count > 0)
            {
                controller = inputDevices[0];
                // UnityEngine.Debug.LogError("Controller detected");
            }
            else
            {
                // UnityEngine.Debug.LogError("No controller detected!");
            }
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        StopAllCoroutines();
        StartCoroutine(FadeMenu(isMenuOpen ? 1 : 0));

        // Play the appropriate sound effect
        if (audioSource != null)
        {
            audioSource.PlayOneShot(isMenuOpen ? openSound : closeSound);
        }
    }

    private IEnumerator FadeMenu(float targetAlpha)
    {
        float startAlpha = menuCanvasGroup.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            menuCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        menuCanvasGroup.alpha = targetAlpha;
        menuCanvasGroup.interactable = isMenuOpen;
        menuCanvasGroup.blocksRaycasts = isMenuOpen;
    }
}
