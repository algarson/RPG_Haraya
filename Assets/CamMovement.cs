using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float panSpeed;
    public float zoomSpeed;

    void Update()
    {
        PanCamera();
        ZoomCamera();
    }

    void PanCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 panDirection = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(panDirection * panSpeed * Time.deltaTime);
    }

    void ZoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = transform.position.z + scrollInput * zoomSpeed;

        transform.position = new Vector3(transform.position.x, transform.position.y, newZoom);
    }
}
