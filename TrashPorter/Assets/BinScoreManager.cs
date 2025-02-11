using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BinScoreManager : MonoBehaviour
{
    public WasteType.WasteCategory correctWasteType; // Type correct pour cette poubelle
    public Renderer binRenderer; // Renderer de la poubelle pour changer la couleur
    public TextMeshPro textAboveBin; // Texte flottant au-dessus de la poubelle
    public int maxCapacity = 5; // Capacité maximale de la poubelle

    private Color defaultColor;
    private Color correctColor = Color.green;
    private Color wrongColor = Color.red;
    private int incorrectItemsCount = 0;
    private List<GameObject> objectsInBin = new List<GameObject>();
    private Dictionary<GameObject, Vector3> initialOffsets = new Dictionary<GameObject, Vector3>();

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        if (binRenderer == null)
        {
            binRenderer = GetComponent<Renderer>();
        }
        defaultColor = binRenderer.material.color;

        if (textAboveBin != null)
        {
            UpdateBinText();
        }
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

    void OnTriggerExit(Collider other)
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
        if (objectsInBin.Count > maxCapacity)
        {
            binRenderer.material.color = wrongColor; // Trop plein => rouge
            textAboveBin.text = "Excess Waste !";
        }
        else if (objectsInBin.Count == maxCapacity)
        {
            binRenderer.material.color = wrongColor; // Plein => rouge
            textAboveBin.text = "Full !";
        }
        else
        {
            if (incorrectItemsCount == 0)
            {
                binRenderer.material.color = correctColor;
            }
            else
            {
                binRenderer.material.color = wrongColor;
            }
            textAboveBin.text = $"{correctWasteType} Bin : ({objectsInBin.Count}/{maxCapacity})";
        }
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
        UpdateBinText();
    }

    private void UpdateBinText()
    {
        if (textAboveBin != null)
        {
            if (objectsInBin.Count > maxCapacity)
            {
                textAboveBin.text = "Excess Waste !";
            }
            else if (objectsInBin.Count == maxCapacity)
            {
                textAboveBin.text = "Full !";
            }
            else
            {
                textAboveBin.text = $"{correctWasteType} Bin : ({objectsInBin.Count}/{maxCapacity})";
            }
        }
    }
}
