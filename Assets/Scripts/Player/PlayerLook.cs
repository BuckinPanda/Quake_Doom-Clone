using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerCamera;
    public Vector2 sensitivities;

    private Vector2 xyRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MouseInput = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

        xyRotation.x -= MouseInput.y * sensitivities.y;
        xyRotation.y += MouseInput.x * sensitivities.x;

        xyRotation.x = Mathf.Clamp(xyRotation.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, xyRotation.y, 0f);
        playerCamera.localEulerAngles = new Vector3(xyRotation.x, 0f, 0f);
    }
}
