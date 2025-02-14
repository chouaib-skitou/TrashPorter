using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private Plane dragPlane;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isDragging = true;
                dragPlane = new Plane(mainCamera.transform.forward, transform.position); // Define a drag plane
                offset = transform.position - GetMouseWorldPos();
            }
        }

        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter); // Get the point where the mouse intersects the plane
        }
        return transform.position;
    }
}
