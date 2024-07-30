using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsSprinting { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    public static UnityDefaultInputActions playerInputAction;

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        
        playerInputAction = new UnityDefaultInputActions();
        playerInputAction.Player.Enable();
    }

    void FixedUpdate()
    {
        var inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        IsSprinting = playerInputAction.Player.Sprint.IsPressed();

        // Get targetMovingSpeed.
        float targetMovingSpeed = (IsSprinting && canRun) ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( inputVector.x * targetMovingSpeed, inputVector.y * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}