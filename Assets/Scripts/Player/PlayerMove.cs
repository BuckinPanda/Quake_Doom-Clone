using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveSmoothTime;
    [SerializeField] private float gravityStrength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    // Player Components
    private CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;

    private Vector3 currentForceVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if (PlayerInput.magnitude > 1f)
        {
            PlayerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(PlayerInput);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        currentMoveVelocity = Vector3.SmoothDamp(
            currentMoveVelocity,
            moveVector * currentSpeed,
            ref moveDampVelocity,
            moveSmoothTime);

        controller.Move(currentMoveVelocity * Time.deltaTime);

        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(groundCheckRay, 1.1f))
        {
            Debug.DrawRay(groundCheckRay.origin, groundCheckRay.direction * 1.1f);
            currentForceVelocity.y = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentForceVelocity.y = jumpStrength;
            }
        }
        else
        {
            currentForceVelocity.y -= gravityStrength * Time.deltaTime;
        }

        controller.Move(currentForceVelocity * Time.deltaTime);
    }
}

