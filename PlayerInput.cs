using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCamera))]
public sealed class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerCamera playerCamera;
    private Vector2 lookInput;

    private void Awake()
    {
        playerCamera = GetComponent<PlayerCamera>();

        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (actions == null) return;
        Cursor.lockState = CursorLockMode.Locked;

        actions.Player.Look.performed += OnLook;
        actions.Player.Look.canceled += OnLook;

        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;

        actions.Player.Attack.performed += OnAttack;
        actions.Player.Attack.canceled += OnAttack;

        actions.Player.Interact.performed += OnInteract;
        actions.Player.Interact.canceled += OnInteract;

        actions.Player.Crouch.performed += OnCrouch;
        actions.Player.Crouch.canceled += OnCrouch;

        actions.Player.Jump.performed += OnJump;
        actions.Player.Jump.canceled += OnJump;

        actions.Player.Previous.performed += OnPrev;
        actions.Player.Previous.canceled += OnPrev;

        actions.Player.Next.performed += OnNext;
        actions.Player.Next.canceled += OnNext;

        actions.Player.Enable();
    }

    private void OnDisable()
    {
        if (actions == null) return;

        actions.Player.Look.performed -= OnLook;
        actions.Player.Look.canceled -= OnLook;

        actions.Player.Move.performed -= OnMove;
        actions.Player.Move.canceled -= OnMove;

        actions.Player.Attack.performed -= OnAttack;
        actions.Player.Attack.canceled -= OnAttack;

        actions.Player.Interact.performed -= OnInteract;
        actions.Player.Interact.canceled -= OnInteract;

        actions.Player.Crouch.performed -= OnCrouch;
        actions.Player.Crouch.canceled -= OnCrouch;

        actions.Player.Jump.performed -= OnJump;
        actions.Player.Jump.canceled -= OnJump;

        actions.Player.Previous.performed -= OnPrev;
        actions.Player.Previous.canceled -= OnPrev;

        actions.Player.Next.performed -= OnNext;
        actions.Player.Next.canceled -= OnNext;

        actions.Player.Disable();
    }

    private void Update()
    {
        // Drive these every frame.
        playerCamera.Look(lookInput);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move action performed");
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack action performed");
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact action performed");
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouch action performed");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump action performed");
    }

    private void OnPrev(InputAction.CallbackContext context)
    {
        Debug.Log("Prev action performed");
    }

    private void OnNext(InputAction.CallbackContext context)
    {
        Debug.Log("Next action performed");
    }
}