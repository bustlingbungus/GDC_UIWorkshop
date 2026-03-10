using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // required references
    Transform camera_component;
    Rigidbody rigidbody_compoonent;


    // Editor parameters
    [SerializeField] float Sensitivity = 1f;
    [SerializeField] float MovementSpeed = 10f;
    [SerializeField] float JumpForce = 50f;
    [SerializeField] float LookAngleLimit = 80f;


    // helper vairables
    bool is_grounded = true;
    float current_camera_rotation = 0f;

    public bool player_control = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get required references
        camera_component = transform.GetChild(0);
        rigidbody_compoonent = GetComponent<Rigidbody>();
        // hide cursor
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (player_control)
        {
            LookAround();
            Move();
            Jump();
        }
    }


    /* Move postion based on user input. */
    void Move()
    {
        // get movement vector from input axes
        Vector3 playerInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        );

        // convert movement vector to local space
        Vector3 movementVector = transform.TransformDirection(playerInput);

        // move position
        transform.position += movementVector * Time.deltaTime * MovementSpeed;
    }


    /* Rotate the player and camera according to mouse movement */
    void LookAround()
    {
        // Get mouse movement
        float XMouseMovement = Input.GetAxis("Mouse X");
        float YMouseMovement = Input.GetAxis("Mouse Y");

        // rotate the player for horizontal movement
        transform.Rotate(0f, XMouseMovement * Sensitivity, 0f);

        // calculate new vertical angle for camera, and clamp
        current_camera_rotation -= YMouseMovement * Sensitivity;
        current_camera_rotation = Mathf.Clamp(current_camera_rotation, -LookAngleLimit, LookAngleLimit);

        // set camera's rotation to the new angle, and copy the player's horizontal rotations
        camera_component.rotation = Quaternion.Euler(
            current_camera_rotation,
            transform.rotation.eulerAngles.y, 
            transform.rotation.eulerAngles.z
        );
    }


    /* If the player presses space while grounded, add vertical velocity */
    void Jump()
    {
        // check for space input while grounded
        if (Input.GetKeyDown(KeyCode.Space) && is_grounded)
        {
            // get current velocity and add jump force to y component
            Vector3 velocity = rigidbody_compoonent.linearVelocity;
            velocity.y += JumpForce;
            rigidbody_compoonent.linearVelocity = velocity;

            // no longer grounded after jumping
            is_grounded = false;
        }
    }


    /* Make player grounded on collision. */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            is_grounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            is_grounded = true;
        }
    }


    /* Make player not grounded on collision. */
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            is_grounded = false;
        }
    }
}
