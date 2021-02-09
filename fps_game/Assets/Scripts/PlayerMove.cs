using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Camera followCamera;
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    PlayerController playerController;
    Rigidbody playerRigidbody;
    Animator playerAnimator;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 moveDistance = playerController.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    void Rotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (plane.Raycast(cameraRay, out rayDistance))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayDistance);
            Vector3 lookPoint = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
            transform.LookAt(lookPoint);
        }
    }
}
