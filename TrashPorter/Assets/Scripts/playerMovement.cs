using System.Collections;
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
}


/*using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, inputDevices);

        if (inputDevices.Count > 0)
        {
            controller = inputDevices[0];
            //UnityEngine.Debug.LogError("Controller detected");
        }
        else
        {
            //UnityEngine.Debug.LogError("No controller detected!");
        }
    }

    void FixedUpdate()
    {
        AdjustHeightToGround();
    }

    void Update()
    {
        MovePlayer();
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

    *//*    void TurnPlayer()
        {
            if (controller.isValid)
            {
                Vector2 touchpadValue;
                if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchpadValue))
                {
                    if (Mathf.Abs(touchpadValue.x) > 0.7f && Mathf.Abs(lastTouchpadValue.x) < 0.3f) // Detect swipe start
                    {
                        float turnDirection = touchpadValue.x > 0 ? 1 : -1;
                        transform.Rotate(0, turnDirection * turnSpeed, 0, Space.World);
                    }
                    lastTouchpadValue = touchpadValue;
                }
            }
        }*//*

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
}
*/