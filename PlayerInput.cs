using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCamera))]
public sealed class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerCamera playerCamera;
    private Vector2 lookInput;
    
    // This boolean needs to be accessed from this script
    // See Psuedo-code below then, add Pause/Resume to action maps
    // Once added Subscribe/Unsubscribe to corresponding methods in OnEnable/OnDisable 
    // After this, place the same Subscribe/Unsubscribe logic in the SwitchInputType() Method
    private bool inputSwitchGate = false;

    [HideInInspector]
    public enum InputType
    {
        Player,
        UI
    }

    public InputType inputType;

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = GetComponent<PlayerCamera>();

        if (actions == null)
            actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (actions == null || inputType == InputType.UI) return;
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

        actions.Player.Sprint.performed += OnSprint;
        actions.Player.Sprint.canceled += OnSprint;

        // Once added to action map, uncomment.
        // actions.Player.Pause.performed += OnPause;
        // actions.Player.Pause.canceled += OnPause;

        actions.Player.Enable();
    }

    private void OnDisable()
    {
        if (actions == null) return;
        Cursor.lockState = CursorLockMode.None;

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

        actions.Player.Sprint.performed -= OnSprint;
        actions.Player.Sprint.canceled -= OnSprint;

        // Once added to action map, uncomment.
        // actions.Player.Pause.performed -= OnPause;
        // actions.Player.Pause.canceled -= OnPause;

        // ----------------------------------------

        actions.UI.Navigate.performed -= OnNavigate;
        actions.UI.Navigate.canceled -= OnNavigate;

        actions.UI.Submit.performed -= OnSubmit;
        actions.UI.Submit.canceled -= OnSubmit;

        actions.UI.Cancel.performed -= OnCancel;
        actions.UI.Cancel.canceled -= OnCancel;

        actions.UI.Point.performed -= OnPoint;
        actions.UI.Point.canceled -= OnPoint;

        actions.UI.Click.performed -= OnClick;
        actions.UI.Click.canceled -= OnClick;

        actions.UI.RightClick.performed -= OnRightClick;
        actions.UI.RightClick.canceled -= OnRightClick;

        actions.UI.MiddleClick.performed -= OnMiddleClick;
        actions.UI.MiddleClick.canceled -= OnMiddleClick;

        actions.UI.ScrollWheel.performed -= OnScrollWheel;
        actions.UI.ScrollWheel.canceled -= OnScrollWheel;

        actions.UI.TrackedDevicePosition.performed -= OnTrackedDevicePosition;
        actions.UI.TrackedDevicePosition.canceled -= OnTrackedDevicePosition;

        actions.UI.TrackedDeviceOrientation.performed -= OnTrackedDeviceOrientation;
        actions.UI.TrackedDeviceOrientation.canceled -= OnTrackedDeviceOrientation;

        // Once added to action map, uncomment.
        // actions.UI.Resume.performed -= OnResume;
        // actions.UI.Resume.canceled -= OnResume;

        actions.Player.Disable();
        actions.UI.Disable();
    }

    private void Update()
    {
        // General logic, user input, simple movement, non-physics actions
        if (inputSwitchGate)
        {
            SwitchInputType();
        }
    }

    private void FixedUpdate()
    {
        // Applying forces, moving Rigidbody components, other physics calculations
        // psuedo: playerMovement.Move(moveInput)
    }

    private void LateUpdate()
    {
        // Applying forces, moving Rigidbody components, other physics calculations
        // Drive these every frame.
        playerCamera.Look(lookInput);
    }

    // ----- Player action map methods -----

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

    private void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprint action performed");
    }

    // Psuedo-code for accessing UI action map (Needs a subscrible/Unsubscribe in OnEnable/OnDisable)
    // Add to SwitchInputType() method once added to action map to align with current pattern
    private void OnPause(InputAction.CallbackContext context)
    {
        if (inputType == InputType.Player)
        {
            inputType = InputType.UI;
            inputSwitchGate = true;
        }
        
    }

    // ----- UI action map methods -----
    private void OnNavigate(InputAction.CallbackContext context)
    {
        Debug.Log("Navigate action performed");
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        Debug.Log("Look action performed");
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel action performed");
    }

    private void OnPoint(InputAction.CallbackContext context)
    {
        Debug.Log("Point action performed");
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Click action performed");
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        Debug.Log("RightClick action performed");
    }

    private void OnMiddleClick(InputAction.CallbackContext context)
    {
        Debug.Log("MiddleClick action performed");
    }

    private void OnScrollWheel(InputAction.CallbackContext context)
    {
        Debug.Log("ScrollWheel action performed");
    }

    private void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        Debug.Log("TrackedDevicePosition action performed");
    }

    private void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        Debug.Log("TrackedDeviceOrientation action performed");
    }

    // Psuedo-code for accessing UI action map (Needs a subscrible/Unsubscribe in OnEnable/OnDisable)
    // Add to SwitchInputType() method once added to action map to align with current pattern
    private void OnResume(InputAction.CallbackContext context)
    {
        if (inputType == InputType.UI)
        {
            inputType = InputType.Player;
            inputSwitchGate = true;
        }
    }

    // Main InputType method
private void SwitchInputType()
{
    switch (inputType)
    {
        case InputType.Player:

            Cursor.lockState = CursorLockMode.Locked;

            actions.UI.Navigate.performed -= OnNavigate;
            actions.UI.Navigate.canceled -= OnNavigate;

            actions.UI.Submit.performed -= OnSubmit;
            actions.UI.Submit.canceled -= OnSubmit;

            actions.UI.Cancel.performed -= OnCancel;
            actions.UI.Cancel.canceled -= OnCancel;

            actions.UI.Point.performed -= OnPoint;
            actions.UI.Point.canceled -= OnPoint;

            actions.UI.Click.performed -= OnClick;
            actions.UI.Click.canceled -= OnClick;

            actions.UI.RightClick.performed -= OnRightClick;
            actions.UI.RightClick.canceled -= OnRightClick;

            actions.UI.MiddleClick.performed -= OnMiddleClick;
            actions.UI.MiddleClick.canceled -= OnMiddleClick;

            actions.UI.ScrollWheel.performed -= OnScrollWheel;
            actions.UI.ScrollWheel.canceled -= OnScrollWheel;

            actions.UI.TrackedDevicePosition.performed -= OnTrackedDevicePosition;
            actions.UI.TrackedDevicePosition.canceled -= OnTrackedDevicePosition;

            actions.UI.TrackedDeviceOrientation.performed -= OnTrackedDeviceOrientation;
            actions.UI.TrackedDeviceOrientation.canceled -= OnTrackedDeviceOrientation;

            // Once added to action map, uncomment.
            // actions.UI.Resume.performed -= OnResume;
            // actions.UI.Resume.canceled -= OnResume;

            actions.UI.Disable();

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

            actions.Player.Sprint.performed += OnSprint;
            actions.Player.Sprint.canceled += OnSprint;

            // Once added to action map, uncomment.
            // actions.Player.Pause.performed -= OnPause;
            // actions.Player.Pause.canceled -= OnPause;

            actions.Player.Enable();

            inputSwitchGate = false;
            break;
            
        case InputType.UI:

            Cursor.lockState = CursorLockMode.None;

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

            actions.Player.Sprint.performed -= OnSprint;
            actions.Player.Sprint.canceled -= OnSprint;

            // Once added to action map, uncomment.
            // actions.Player.Pause.performed -= OnPause;
            // actions.Player.Pause.canceled -= OnPause;

            actions.Player.Disable();

            actions.UI.Navigate.performed += OnNavigate;
            actions.UI.Navigate.canceled += OnNavigate;

            actions.UI.Submit.performed += OnSubmit;
            actions.UI.Submit.canceled += OnSubmit;

            actions.UI.Cancel.performed += OnCancel;
            actions.UI.Cancel.canceled += OnCancel;

            actions.UI.Point.performed += OnPoint;
            actions.UI.Point.canceled += OnPoint;

            actions.UI.Click.performed += OnClick;
            actions.UI.Click.canceled += OnClick;

            actions.UI.RightClick.performed += OnRightClick;
            actions.UI.RightClick.canceled += OnRightClick;

            actions.UI.MiddleClick.performed += OnMiddleClick;
            actions.UI.MiddleClick.canceled += OnMiddleClick;

            actions.UI.ScrollWheel.performed += OnScrollWheel;
            actions.UI.ScrollWheel.canceled += OnScrollWheel;

            actions.UI.TrackedDevicePosition.performed += OnTrackedDevicePosition;
            actions.UI.TrackedDevicePosition.canceled += OnTrackedDevicePosition;

            actions.UI.TrackedDeviceOrientation.performed += OnTrackedDeviceOrientation;
            actions.UI.TrackedDeviceOrientation.canceled += OnTrackedDeviceOrientation;

            // Once added to action map, uncomment.
            // actions.UI.Resume.performed += OnResume;
            // actions.UI.Resume.canceled += OnResume;

            actions.UI.Enable();

            inputSwitchGate = false;
            break;

        default:
            Debug.LogError("[PlayerInput] Enumerator value out of bounds or does not exist for available action maps.");
            return;
    }
}
}