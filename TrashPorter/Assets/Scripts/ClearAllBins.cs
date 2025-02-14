/* using UnityEngine;
using System.Collections;

public class ClearAllBins : MonoBehaviour
{
    public BinScoreManager[] bins;
    public Transform handle;
    public float binMoveDistance = 2f;
    public float binMoveDuration = 0.5f;

    private bool isActivated = false;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = handle.rotation;
    }

    private void OnMouseDown()
    {
        if (!isActivated)
        {
            isActivated = true; 
            StartCoroutine(RaiseAndLowerLever());
            StartCoroutine(MoveBinsAndClear());
        }
    }

    private IEnumerator RaiseAndLowerLever()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Quaternion startRotation = handle.rotation;
        Quaternion targetRotation = Quaternion.Euler(-45, 0, 0) * initialRotation;

        while (elapsed < duration)
        {
            handle.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        handle.rotation = targetRotation;
        yield return new WaitForSeconds(0.5f);

        elapsed = 0f;
        while (elapsed < duration)
        {
            handle.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        handle.rotation = initialRotation;
        isActivated = false;
    }

    private IEnumerator MoveBinsAndClear()
    {
        foreach (BinScoreManager bin in bins)
        {
            StartCoroutine(bin.MoveBackwardAndClear(binMoveDistance, binMoveDuration));
        }

        yield return new WaitForSeconds(binMoveDuration + 1f);
        Debug.Log("Toutes les poubelles ont �t� vid�es et replac�es !");
    }
}
*/

// Script pour vider toutes les poubelles simultanément avec descriptions des fonctions
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ClearAllBins : MonoBehaviour
{
    // Tableau des poubelles à gérer
    public BinScoreManager[] bins;
    //public List<GameObject> bins;
    // Poignée d'activation
    public Transform handle;
    // Paramètres de déplacement des poubelles
    public float binMoveDistance = 2f;
    public float binMoveDuration = 0.5f;

    private bool isActivated = false; // Indique si la commande est en cours d'utilisation
    private Quaternion initialRotation; // Rotation initiale de la poignée

    // Initialise la rotation de la poignée
    public void Start()
    {
        initialRotation = handle.rotation;
    }

    // Détecte un clic sur l'objet pour lancer les animations
    public void OnMouseDown()
    {
            isActivated = true;
            StartCoroutine(RaiseAndLowerLever()); // Animation de la poignée
            StartCoroutine(MoveBinsAndClear());   // Déplacement et vidage des poubelles
    }

    // Anime la poignée : rotation vers le bas puis retour à la position initiale
    public IEnumerator RaiseAndLowerLever()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Quaternion startRotation = handle.rotation;
        Quaternion targetRotation = Quaternion.Euler(-45, 0, 0) * initialRotation;

        // Rotation descendante
        while (elapsed < duration)
        {
            handle.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        handle.rotation = targetRotation;
        yield return new WaitForSeconds(0.5f);

        // Rotation ascendante (retour)
        elapsed = 0f;
        while (elapsed < duration)
        {
            handle.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        handle.rotation = initialRotation;
        isActivated = false;
    }

    // Anime et vide chaque poubelle via BinScoreManager
    public IEnumerator MoveBinsAndClear()
    {
        foreach (BinScoreManager bin in bins)

        {
                StartCoroutine(bin.MoveBackwardAndClear(binMoveDistance, binMoveDuration));
        }

        yield return new WaitForSeconds(binMoveDuration + 1f);
        Debug.Log("Toutes les poubelles ont été vidées et replacées !");
    }
}
