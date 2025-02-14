using UnityEngine;
//using Unity.XR.PXR;

public class KeyBoardMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;   // Vitesse de déplacement
    public float rotateSpeed = 50f;  // Vitesse de rotation
    public Transform headTransform;  // Assigné à la Main Camera (tête du joueur)

    private void Update()
    {
        // Déplacement avant/arrière
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1;    // Avancer
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1; // Reculer
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1; // Gauche
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1; // Droite

        // Direction basée sur le regard du joueur
        Vector3 moveDirection = headTransform.right * moveX + headTransform.forward * moveY;
        moveDirection.y = 0; // Garde le mouvement au sol

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotation avec les flèches gauche/droite
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
