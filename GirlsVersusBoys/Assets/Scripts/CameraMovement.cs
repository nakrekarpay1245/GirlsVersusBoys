using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    public Camera camera;

    [SerializeField]
    private Transform moveCameraTransform;

    [SerializeField]
    private Transform commandCameraTransform;

    [SerializeField]
    private float rotateSpeed = 5;

    private float mouseX;
    private float yRotation;

    private float mouseY;
    private float xRotation;

    private bool canMove;
    private Vector3 rotation;

    public static CameraMovement instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseX = Input.GetAxis("Mouse X") * rotateSpeed * 2 * Time.deltaTime;
            yRotation += mouseX;

            mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, 30, 75);

            moveCameraTransform.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            commandCameraTransform.transform.localRotation = Quaternion.Euler(90, yRotation, 0);
        }
    }

    public void Command()
    {
        canMove = false;

        transform.DORotateQuaternion(commandCameraTransform.rotation, 0.15f);
    }

    public void Move()
    {
        canMove = true;

        transform.DORotateQuaternion(moveCameraTransform.rotation, 0.15f);
    }
}
