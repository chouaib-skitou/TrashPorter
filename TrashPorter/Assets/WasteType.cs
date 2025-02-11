using UnityEngine;

public class WasteType : MonoBehaviour
{
    // Enum pour définir les différents types de déchets
    public enum WasteCategory
    {
        Metal,
        Paper,
        Plastic,
        Glass,
        NonRecyclable, // Pour les objets non recyclables
        Hazardous      // Pour les objets dangereux
    }

    // Variable publique pour sélectionner le type dans l'inspecteur
    public WasteCategory wasteType;
}
