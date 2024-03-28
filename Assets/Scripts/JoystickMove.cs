using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{

    [SerializeField] Joystick movementJoystick;
    [SerializeField] float playerSpeed = 8.5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementJoystick.Direction.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}