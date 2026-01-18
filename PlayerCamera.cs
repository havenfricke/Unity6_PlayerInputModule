using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCamera : MonoBehaviour
{
    [Header("Settings")]
    [Range(1, 100)][SerializeField] private float sensitivity = 50f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 60f;
    [SerializeField] private float distance = 4.5f;
    [SerializeField] private Vector3 pivotOffset = new Vector3(0f, 1.6f, 0f);

    private Camera playerCamera;
    private Vector2 lookInput;
    private float yaw;
    private float pitch = 15f;

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = FindFirstObjectByType<Camera>();
        }

        yaw = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if (playerCamera == null) return;

        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Vector3 pivot = transform.position + pivotOffset;

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 camPos = pivot - rot * Vector3.forward * distance;

        playerCamera.transform.position = camPos;
        playerCamera.transform.LookAt(pivot);
    }

    public void Look(Vector2 input)
    {
        lookInput = input;
    }
}
