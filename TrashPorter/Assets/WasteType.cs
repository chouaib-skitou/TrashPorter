using UnityEngine;

public class WasteType : MonoBehaviour
{
    // Enum pour d�finir les diff�rents types de d�chets
    public enum WasteCategory
    {
        Metal,
        Paper,
        Plastic,
        Glass,
        NonRecyclable, // Pour les objets non recyclables
        Hazardous      // Pour les objets dangereux
    }

    // Variable publique pour s�lectionner le type dans l'inspecteur
    public WasteCategory wasteType;
}
