using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDistanceCalculator : MonoBehaviour
{
    public float MouseDistance { get; private set; }

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public float GetMouseDistance(GameObject target)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = cam.WorldToScreenPoint(target.transform.position).z;

        return Vector3.Distance(
            cam.ScreenToWorldPoint(mousePos),
            target.transform.position
        );
    }
}

