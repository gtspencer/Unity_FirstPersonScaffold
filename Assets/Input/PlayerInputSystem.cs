using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputSystem Instance;
    
    private UnityDefaultInputActions playerInputAction;
    private PlayerInput playerInput;
    
    public Vector2 Movement => playerInputAction.Player.Move.ReadValue<Vector2>();
    public bool Sprint => playerInputAction.Player.Sprint.IsPressed();
    public Vector2 Look => playerInputAction.Player.Look.ReadValue<Vector2>();
    public bool Crouch => playerInputAction.Player.Crouch.IsPressed();

    public Action OnJump = () => { };
    public Action InputSwitched = () => { };
    public Action OnEscape = () => { };
    public Action OnUIBack = () => { };

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        playerInput = GetComponent<PlayerInput>();

        playerInputAction = new UnityDefaultInputActions();
        playerInputAction.Player.Enable();

        DontDestroyOnLoad(gameObject);
    }

    public enum InputType
    {
        UI,
        Movement
    }
    
    public void ChangeInputType(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.UI:
                // playerInput.SwitchCurrentActionMap("UI");
                playerInputAction.UI.Enable();
                playerInputAction.Player.Disable();
                Cursor.lockState = CursorLockMode.None;
                break;
            case InputType.Movement:
                // playerInput.SwitchCurrentActionMap("Player");
                Cursor.lockState = CursorLockMode.Locked;
                playerInputAction.UI.Disable();
                playerInputAction.Player.Enable();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputAction.Player.Jump.performed += JumpPerformed;
        playerInputAction.Player.Escape.performed += EscapePerformed;
        playerInputAction.UI.Escape.performed += EscapePerformed;
        playerInputAction.UI.Back.performed += BackPerformed;
    }

    private void BackPerformed(InputAction.CallbackContext context)
    {
        OnUIBack.Invoke();
    }

    private void EscapePerformed(InputAction.CallbackContext context)
    {
        OnEscape.Invoke();
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        OnJump.Invoke();
    }
}
