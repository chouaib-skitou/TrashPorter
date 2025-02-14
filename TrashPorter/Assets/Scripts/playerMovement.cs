/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float turnSpeed = 20.0f;
    public float groundDetectionDistance = 1.5f;
    public LayerMask groundLayer;

    private Transform cameraTransform;
    private InputDevice controller;
    private Rigidbody rb;
    private Vector2 lastTouchpadValue;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        cameraTransform = Camera.main?.transform;
    }

    void Update()
    {
        UpdateController(); // Check if controller is connected
        MovePlayer();
    }

    void FixedUpdate()
    {
        AdjustHeightToGround();
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

    void MovePlayer()
    {
        if (controller.isValid)
        {
            Vector2 touchpadValue;
            bool isClicked;

            if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isClicked) && isClicked &&
                controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchpadValue))
            {
                Vector3 forward = cameraTransform.forward;
                Vector3 right = cameraTransform.right;

                forward.y = 0;
                right.y = 0;

                forward.Normalize();
                right.Normalize();

                Vector3 direction = forward * touchpadValue.y + right * touchpadValue.x;
                Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
                newPosition.y = GetGroundHeight(newPosition);
                transform.position = newPosition;
            }
        }
    }

    void AdjustHeightToGround()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = GetGroundHeight(currentPosition);
        transform.position = currentPosition;
    }

    private float GetGroundHeight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * groundDetectionDistance, Vector3.down, out hit, groundDetectionDistance * 2, groundLayer))
        {
            return hit.point.y;
        }
        return transform.position.y;
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMovement : MonoBehaviour
{
    // Variables publiques pour ajuster la vitesse de déplacement, la vitesse de rotation et la détection du sol
    public float speed = 2.0f; // Vitesse de déplacement du joueur
    public float groundDetectionDistance = 1.5f; // Distance pour détecter le sol sous le joueur
    public LayerMask groundLayer; // Couche du sol pour la détection de raycast

    private Transform cameraTransform; // Référence à la caméra principale
    private InputDevice controller; // Référence au contrôleur XR (par exemple, contrôleur VR)
    private Rigidbody rb; // Référence au Rigidbody du joueur pour appliquer la physique

    // Initialisation des composants et paramètres de départ
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupère le Rigidbody attaché au joueur
        rb.useGravity = false; // Désactive la gravité (la gravité est gérée par la détection du sol)
        rb.isKinematic = true; // Rend le Rigidbody kinematique (sans physique par défaut)

        cameraTransform = Camera.main?.transform; // Récupère la caméra principale pour diriger le mouvement
    }

    // Mise à jour des contrôles et du mouvement du joueur chaque frame
    void Update()
    {
        UpdateController(); // Vérifie si le contrôleur est bien connecté
        MovePlayer(); // Déplace le joueur selon les entrées du contrôleur
    }

    // Mise à jour de la hauteur du joueur à chaque FixedUpdate (physique)
    void FixedUpdate()
    {
        AdjustHeightToGround(); // Ajuste la hauteur du joueur pour le maintenir sur le sol
    }

    // Vérifie la validité du contrôleur XR et le récupère s'il n'est pas encore connecté
    void UpdateController()
    {
        if (!controller.isValid) // Si le contrôleur n'est pas valide (non connecté)
        {
            var inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, inputDevices); // Cherche les contrôleurs

            if (inputDevices.Count > 0)
            {
                controller = inputDevices[0]; // Assigne le premier contrôleur trouvé
                // UnityEngine.Debug.LogError("Controller detected"); // Optionnel: log de détection du contrôleur
            }
            else
            {
                // UnityEngine.Debug.LogError("No controller detected!"); // Optionnel: log d'absence de contrôleur
            }
        }
    }

    // Déplace le joueur en fonction des entrées du pavé tactile du contrôleur
    void MovePlayer()
    {
        if (controller.isValid) // Si le contrôleur est valide et connecté
        {
            Vector2 touchpadValue; // Valeur du pavé tactile
            bool isClicked; // Détecte si le pavé tactile est cliqué

            // Si le pavé tactile est cliqué et sa valeur est valide
            if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isClicked) && isClicked &&
                controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchpadValue))
            {
                // Direction de mouvement basée sur l'orientation de la caméra
                Vector3 forward = cameraTransform.forward;
                Vector3 right = cameraTransform.right;

                forward.y = 0; // Ignore le mouvement vertical
                right.y = 0; // Ignore le mouvement vertical

                forward.Normalize(); // Normalise les directions pour éviter une accélération diagonale
                right.Normalize();

                // Calcul de la direction du mouvement en fonction du pavé tactile
                Vector3 direction = forward * touchpadValue.y + right * touchpadValue.x;
                // Nouveau vecteur de position du joueur
                Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
                newPosition.y = GetGroundHeight(newPosition); // Assure que la position reste au niveau du sol
                transform.position = newPosition; // Applique la nouvelle position
            }
        }
    }

    // Ajuste la hauteur du joueur pour le maintenir sur le sol
    void AdjustHeightToGround()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = GetGroundHeight(currentPosition); // Récupère la hauteur du sol à la position actuelle
        transform.position = currentPosition; // Applique la nouvelle hauteur
    }

    // Récupère la hauteur du sol sous la position donnée via un Raycast
    private float GetGroundHeight(Vector3 position)
    {
        RaycastHit hit;
        // Lancer un raycast depuis une position au-dessus du joueur vers le bas pour détecter le sol
        if (Physics.Raycast(position + Vector3.up * groundDetectionDistance, Vector3.down, out hit, groundDetectionDistance * 2, groundLayer))
        {
            return hit.point.y; // Retourne la hauteur du sol détecté
        }
        return transform.position.y; // Si aucun sol n'est trouvé, retourne la position actuelle du joueur
    }
}
