using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    public LayerMask groundLayer;
    public float moveSpeed;
    public float sideSpeed;
    public float jumpForce;
    public float distanceToGround;
    
    private bool forward; 
    private bool backward;
    private bool left;
    private bool right;
    private bool up;
    private Rigidbody rb;
    private CapsuleCollider col;
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        forward = false;
        backward = false;
        left = false;
        right = false;
        up = false;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        cam = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
            forward = true;
        if (Input.GetKey(KeyCode.S))
            backward = true;
        if (Input.GetKey(KeyCode.A))
            left = true;
        if (Input.GetKey(KeyCode.D))
            right = true;
        if (Input.GetKeyDown(KeyCode.Space))
            up = true;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }


    void FixedUpdate()
    {
        if (IsGrounded() && forward)
        {
            rb.AddForce(cam.transform.forward * moveSpeed, ForceMode.Force);
            forward = false;
        }
        if (IsGrounded() && backward)
        {
            rb.AddForce(-cam.transform.forward * sideSpeed, ForceMode.Force);
            backward = false;
        }
        if (IsGrounded() && left)
        {
            rb.AddForce(-cam.transform.right * sideSpeed, ForceMode.Force);
            left = false;
        }

        if (IsGrounded() && right)
        {
            rb.AddForce(cam.transform.right * sideSpeed, ForceMode.Force);
            right = false;
        }
        if (IsGrounded() && up)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            up = false;
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
