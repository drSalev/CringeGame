using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f);
    public float speed = 1f;
    private Transform target;
    private float eulerX = 0f;
    private float eulerY = 0f;
    void Start()
    {
        target = GameObject.Find("Player").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //float X = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        //float Y = -Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
        //eulerX = (transform.rotation.eulerAngles.x + Y) % 360;
        //eulerY = (transform.rotation.eulerAngles.y + X) % 360;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void LateUpdate()
    {
        this.transform.position = target.TransformPoint(camOffset);
       //this.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        this.transform.LookAt(target);
    }
}

