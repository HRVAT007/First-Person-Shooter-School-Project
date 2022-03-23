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
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerBody.velocity = new Vector3(moveVector.x, playerBody.velocity.y, moveVector.z);

        if(Input.GetKeyDown(KeyCode.Space))
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
}
