using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f);
    public float rotationSpeed = 1f;
    public float minAngle = 20f;
    public float maxAngle = 340f;

    private Transform target;
    private float X = 0f;
    private float Y = 0f;
    void Start()
    {
        target = GameObject.Find("Player").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        X = transform.eulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        Y += Input.GetAxis("Mouse Y") * rotationSpeed;
        Y = Mathf.Clamp(Y, -90, 90);
        transform.eulerAngles = new Vector3(-Y, X, 0);
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void LateUpdate()
    {
        this.transform.position = target.TransformPoint(camOffset);
    }
}

