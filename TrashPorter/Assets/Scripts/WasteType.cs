/*using UnityEngine;

public class WasteType : MonoBehaviour
{
    // Enum pour d�finir les diff�rents types de d�chets
    public enum WasteCategory
    {
        Metal,
        Paper,
        Plastic,
        Glass
        
    }

    // Variable publique pour s�lectionner le type dans l'inspecteur
    public WasteCategory wasteType;
}
*/
using UnityEngine;

public class WasteType : MonoBehaviour
{
    // Enum pour définir les différentes catégories de déchets
    // Cet enum permet de classer les types de déchets pour l'objet.
    public enum WasteCategory
    {
        Metal,   // Représente les déchets métalliques
        Paper,   // Représente les déchets en papier
        Plastic, // Représente les déchets plastiques
        Glass    // Représente les déchets en verre
    }

    // Variable publique pour sélectionner le type de déchet dans l'inspecteur
    // La variable wasteType permet d'assigner une catégorie de déchet à cet objet
    // directement dans l'Inspecteur, en utilisant les options de l'enum (Metal, Paper, Plastic, Glass).
    public WasteCategory wasteType;
}
