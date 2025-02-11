using UnityEngine;
using System.Collections;

public class PanelCtrl : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup; // Reference to CanvasGroup for fading
    public float fadeDuration = 0.3f; // Fade duration
    private bool isMenuOpen = false;

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip openSound; // Sound when opening
    public AudioClip closeSound; // Sound when closing

    void Start()
    {
        // Ensure menu starts hidden
        menuCanvasGroup.alpha = 0;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleMenu();
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
