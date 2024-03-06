using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float lookSpeed = 5f;
    [SerializeField]
    private float jumpHeight = 20f;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private LayerMask groundMask;
    void Update()
    {
        Move();
        Look();


        if (Input.GetAxis("Jump")>0 && characterController.isGrounded)
        {
            Vector3 jumpDirection = new Vector3(0, jumpHeight, 0);
            characterController.Move(jumpDirection * Time.deltaTime);

        }
    }


    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 MoveDirection = transform.forward * inputY + transform.right * inputX;
        MoveDirection *= walkSpeed;
        MoveDirection.y += -gravity;

        characterController.Move(MoveDirection * Time.deltaTime);

    }

    private void Look()
    {
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");

        //Yaw Rotation
        Vector3 curRot = transform.eulerAngles;
        curRot.y += lookSpeed * mouseInputX;
        transform.eulerAngles = curRot;


        //Pitch Rotation

        curRot = head.eulerAngles;
        curRot.x -= lookSpeed * mouseInputY;
        if (curRot.x > 180)
        {
            curRot.x = Mathf.Clamp(curRot.x, 270f, 361f);

        }
        else
        {
            curRot.x = Mathf.Clamp(curRot.x, -1, 90f);

        }
        head.eulerAngles = curRot;
    }

    private bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        return Physics.Raycast(ray, 0.6f, groundMask);
    }
}
