using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    public float rotationX;
    public float rotationY;




    // Start is called before the first frame update
    void Start()
    {
        sensX = 1000f;
        sensY = 1000f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);

    }
}
