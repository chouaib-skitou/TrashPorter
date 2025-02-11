using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement
    public float stepDistance = 1f; // Distance d'un pas
    public int steps = 5; // Nombre de pas
    public float rotationSpeed = 700f; // Vitesse de rotation

    private Rigidbody rb;
    private int stepsRemaining; // Pas restants � effectuer
    private bool isMoving = false; // Indique si le personnage est en train de bouger
    private Vector3 moveDirection; // Direction du d�placement

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // D�clenche le mouvement avec une touche (par exemple Espace)
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            stepsRemaining = steps; // Initialise le compteur de pas
            moveDirection = transform.forward; // Direction du mouvement (en avant)
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving && stepsRemaining > 0)
        {
            // Avance d'un pas
            rb.MovePosition(rb.position + moveDirection * stepDistance);
            stepsRemaining--;

            // Arr�te le mouvement une fois les pas termin�s
            if (stepsRemaining == 0)
            {
                isMoving = false;
            }
        }

        // Rotation avec la souris
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.fixedDeltaTime, 0);
    }
}
