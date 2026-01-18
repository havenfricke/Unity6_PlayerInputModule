using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerCamera))]
public sealed class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerCamera camera;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (actions == null)
            actions = new InputSystem_Actions();

        if (camera == null)
            camera = GetComponent<PlayerCamera>();
    }

    private void OnEnable()
    {
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

    private void OnLook(InputAction.CallbackContext context)
    {
        camera.Look(context.ReadValue<Vector2>());
    }

    private void OnMove(InputAction.CallbackContext context)
    {
       Debug.Log($"{context.ReadValue<Vector2>()}");
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
