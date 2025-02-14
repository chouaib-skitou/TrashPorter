/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BinScoreManager : MonoBehaviour
{
    public WasteType.WasteCategory correctWasteType;
    public Renderer binRenderer;
    public TextMeshPro textAboveBin;
    public int maxCapacity = 5;

    private Color defaultColor;
    private Color correctColor = Color.white;
    private Color wrongColor = Color.red;
    private int incorrectItemsCount = 0;
    private List<GameObject> objectsInBin = new List<GameObject>();
    private Dictionary<GameObject, Vector3> initialOffsets = new Dictionary<GameObject, Vector3>();

    private Vector3 initialPosition;

    private int previousScore = 0;
    private int currentScore = 0;


    void Start()
    {
        initialPosition = transform.position;

        if (binRenderer == null)
        {
            binRenderer = GetComponent<Renderer>();
        }
        defaultColor = binRenderer.material.color;

        UpdateBinState();
    }

    void OnTriggerEnter(Collider other)
    {
        WasteType waste = other.GetComponent<WasteType>();
        if (waste != null)
        {
            objectsInBin.Add(other.gameObject);
            initialOffsets[other.gameObject] = other.transform.position - transform.position;

            UpdateBinState();
        }
    }

    //void OnTriggerExit(Collider other)
    void OnTriggerEnter(GameObject other)
    {
        WasteType waste = other.GetComponent<WasteType>();
        if (waste != null)
        {
            objectsInBin.Remove(other.gameObject);
            initialOffsets.Remove(other.gameObject);

            UpdateBinState();
        }
    }

    private void UpdateBinState()
    {
        incorrectItemsCount = 0;
        textAboveBin.text = "";
        foreach (GameObject obj in objectsInBin)
        {
            if (obj.GetComponent<WasteType>().wasteType != correctWasteType)
            {
                incorrectItemsCount++;
            }
        }

        if (objectsInBin.Count > maxCapacity)
        {
            binRenderer.material.color = wrongColor;
            textAboveBin.text = "Excess Waste!";
        }
        else if (objectsInBin.Count == maxCapacity)
        {
            binRenderer.material.color = wrongColor;
            textAboveBin.text = "Full!";
        }
        else
        {
            if (incorrectItemsCount == 0)
            {
                binRenderer.material.color = correctColor;
                // Si tous les objets sont corrects, ajoute au score global
            }
            else
            {
                binRenderer.material.color = wrongColor;
                textAboveBin.text = $"Objects that are not of type {correctWasteType} found!";
            }
        }
        currentScore = objectsInBin.Count - incorrectItemsCount;

        ScoreManager.Instance.AddScore(currentScore-previousScore); // Utilisation du Singleton

        textAboveBin.text += $" \n {correctWasteType} Bin : ({currentScore}/{maxCapacity})";
        previousScore = objectsInBin.Count - incorrectItemsCount;

    }

    public IEnumerator MoveBackwardAndClear(float distance, float duration)
    {
        Vector3 targetPosition = initialPosition - transform.forward * distance;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            foreach (var obj in objectsInBin)
            {
                if (obj != null)
                {
                    obj.transform.position = transform.position + initialOffsets[obj];
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        yield return new WaitForSeconds(0.5f);

        ClearBin();

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(targetPosition, initialPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
    }

    public void ClearBin()
    {
        foreach (GameObject obj in objectsInBin)
        {
            Destroy(obj);
        }
        objectsInBin.Clear();
        initialOffsets.Clear();
        incorrectItemsCount = 0;
        binRenderer.material.color = defaultColor;
        Debug.Log($"La poubelle {gameObject.name} a été vidée.");
        UpdateBinState();
    }
}
*/

// Gestionnaire de score par poubelle dans Unity avec descriptions des fonctions
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BinScoreManager : MonoBehaviour
{
    // Paramètres publics
    public WasteType.WasteCategory correctWasteType; // Type correct d'objet accepté
    public Renderer binRenderer;                     // Renderer pour changer la couleur de la poubelle
    public TextMeshPro textAboveBin;                // Texte indicatif au-dessus de la poubelle
    public int maxCapacity = 5;                     // Capacité maximale de la poubelle

    // Variables privées
    private Color defaultColor;
    private Color correctColor = Color.white;
    private Color wrongColor = Color.red;
    private int incorrectItemsCount = 0;
    private List<GameObject> objectsInBin = new List<GameObject>();
    private Dictionary<GameObject, Vector3> initialOffsets = new Dictionary<GameObject, Vector3>();
    private Vector3 initialPosition;
    private int previousScore = 0;
    private int currentScore = 0;

    // Initialise les paramètres et l'affichage de la poubelle
    void Start()
    {
        initialPosition = transform.position;
        binRenderer ??= GetComponent<Renderer>();
        defaultColor = binRenderer.material.color;
        UpdateBinState();
    }

    // Détecte l'entrée d'un objet dans la poubelle et met à jour son état
    void OnTriggerEnter(Collider other)
    {
        WasteType waste = other.GetComponent<WasteType>();
        if (waste != null)
        {
            objectsInBin.Add(other.gameObject);
            initialOffsets[other.gameObject] = other.transform.position - transform.position;
            UpdateBinState();
        }
    }

    // Met à jour l'état de la poubelle : couleurs, score et messages
    private void UpdateBinState()
    {
        incorrectItemsCount = 0;
        textAboveBin.text = "";

        // Compte les objets incorrects
        foreach (GameObject obj in objectsInBin)
        {
            if (obj.GetComponent<WasteType>().wasteType != correctWasteType)
            {
                incorrectItemsCount++;
            }
        }

        // Change la couleur et affiche l'état selon le remplissage
        if (objectsInBin.Count > maxCapacity)
        {
            binRenderer.material.color = wrongColor;
            textAboveBin.text = "Excess Waste!";
        }
        else if (objectsInBin.Count == maxCapacity)
        {
            binRenderer.material.color = wrongColor;
            textAboveBin.text = "Full!";
        }
        else
        {
            binRenderer.material.color = incorrectItemsCount == 0 ? correctColor : wrongColor;
            if (incorrectItemsCount > 0)
            {
                textAboveBin.text = $"Objects that are not of type {correctWasteType} found!";
            }
        }

        // Met à jour le score via le ScoreManager Singleton
        currentScore = objectsInBin.Count - incorrectItemsCount;
        ScoreManager.Instance.AddScore(currentScore - previousScore);
        textAboveBin.text += $"\n {correctWasteType} Bin : ({currentScore}/{maxCapacity})";
        previousScore = currentScore;
    }

    // Anime la poubelle (recul et retour) puis la vide
    public IEnumerator MoveBackwardAndClear(float distance, float duration)
    {
        Vector3 targetPosition = initialPosition - transform.forward * distance;
        float elapsedTime = 0f;

        // Déplacement arrière progressif
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            foreach (var obj in objectsInBin)
            {
                if (obj != null)
                {
                    obj.transform.position = transform.position + initialOffsets[obj];
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        yield return new WaitForSeconds(0.5f);

        // Vide la poubelle et retourne à sa position
        ClearBin();
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(targetPosition, initialPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = initialPosition;
    }

    // Vide la poubelle : détruit tous les objets et réinitialise l'état
    public void ClearBin()
    {
        foreach (GameObject obj in objectsInBin)
        {
            Destroy(obj);
        }
        objectsInBin.Clear();
        initialOffsets.Clear();
        incorrectItemsCount = 0;
        binRenderer.material.color = defaultColor;
        Debug.Log($"La poubelle {gameObject.name} a été vidée.");
        UpdateBinState();
    }
}