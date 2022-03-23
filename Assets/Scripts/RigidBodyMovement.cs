using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRotation;

    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] bool lockCursor = true;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] GameObject knee;
    [SerializeField] GameObject feet;
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.1f;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        knee.transform.position = new Vector3(knee.transform.position.x, stepHeight, knee.transform.position.z);
    }

    void Update()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        StepClimb();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerBody.velocity = new Vector3(moveVector.x, playerBody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(feetTransform.position, 0.1f, floorMask))
            {
                playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void MovePlayerCamera()
    {
        xRotation -= playerMouseInput.y * sensitivity;

        transform.Rotate(0.0f, playerMouseInput.x * sensitivity, 0.0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
    }

    void StepClimb()
    {
        RaycastHit hitFeet;
        if (Physics.Raycast(feet.transform.position, transform.TransformDirection(Vector3.forward), out hitFeet, 0.1f))
        {
            RaycastHit hitKnee;
            if (!Physics.Raycast(knee.transform.position, transform.TransformDirection(Vector3.forward), out hitKnee, 0.2f))
            {
                playerBody.position -= new Vector3(0.0f, -stepSmooth, 0.0f);
            }
        }

        RaycastHit hitFeet45;
        if (Physics.Raycast(feet.transform.position, transform.TransformDirection(1.5f, 0,1), out hitFeet45, 0.1f))
        {
            RaycastHit hitKnee45;
            if (!Physics.Raycast(knee.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitKnee45, 0.2f))
            {
                playerBody.position -= new Vector3(0.0f, -stepSmooth, 0.0f);
            }
        }

        RaycastHit hitFeetMinus45;
        if (Physics.Raycast(feet.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitFeetMinus45, 0.1f))
        {
            RaycastHit hitKneeMinus45;
            if (!Physics.Raycast(knee.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitKneeMinus45, 0.2f))
            {
                playerBody.position -= new Vector3(0.0f, -stepSmooth, 0.0f);
            }
        }
    }
}
