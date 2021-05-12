using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook2 : MonoBehaviour
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;


    public float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        //aiming sensivity
        Aim();
    }

    void Aim()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            multiplier = 0.0035f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            multiplier = 0.01f;
        }
    }
}