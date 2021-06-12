using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform Body;

    float xRotation = 0f;

    //public GameObject lookat;
    //public GameObject DefaultBlock;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Body.Rotate(Vector3.up * mouseX);


        Ray ray = new Ray(transform.position, transform.forward * 5.5f);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5.5f, Color.red);
        /*
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5.5f, 1 << 0 | 1 << 8 | 1 << 12))
        {
            lookat = hit.collider.gameObject;
            //Debug.LogWarning(hit.collider.gameObject);
        }
        else
        {
            lookat = DefaultBlock;
        }
        */
    }
}
