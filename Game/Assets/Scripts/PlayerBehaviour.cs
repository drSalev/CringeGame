using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    public LayerMask groundLayer;
    public float speed = 20f;
    public float rotationSpeed = 200f;
    public float jumpForce = 20f;
    public float distanceToGround = 0.1f;
    
    private float hRotation = 0f;
    private bool forward = false; 
    private bool backward = false;
    private bool left = false;
    private bool right = false;
    private bool up = false;
    private Rigidbody rb;
    private CapsuleCollider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
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

        hRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }


    void FixedUpdate()
    {
        if (forward)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Force);
            forward = false;
        }
        if (backward)
        {
            rb.AddForce(-transform.forward * speed / 2, ForceMode.Force);
            backward = false;
        }
        if (left)
        {
            rb.AddForce(-transform.right * speed / 2, ForceMode.Force);
            left = false;
        }
        if (right)
        {
            rb.AddForce(transform.right * speed / 2, ForceMode.Force);
            right = false;
        }
        if (IsGrounded() && up)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            up = false;
        }

        Vector3 rotation = hRotation * Vector3.up;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angleRot);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
