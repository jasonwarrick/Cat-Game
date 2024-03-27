using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rb;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    InputActionsManager inputActionsManager;

    InputAction moveAction;
    InputAction lookAction;
    public Vector2 lookVector = new Vector2();

    void Start()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();

        inputActionsManager = new InputActionsManager();
        
        moveAction = inputActionsManager.Player.Move;
        moveAction.Enable();
        lookAction = inputActionsManager.Player.Look;
        lookAction.Enable();
        inputActionsManager.Player.Interact.Enable();
    }

    void OnEnable() {
        // moveAction = inputActionsManager.Player.Move;
        // moveAction.Enable();
        // inputActionsManager.Player.Interact.Enable();
    }

    void OnDisable() {
        moveAction.Disable();
        inputActionsManager.Player.Interact.Disable();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        lookVector = lookAction.ReadValue<Vector2>();

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = moveAction.ReadValue<Vector2>() * targetMovingSpeed;

        // Apply movement.
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
    }
}