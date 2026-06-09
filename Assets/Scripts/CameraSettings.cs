using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    private void Start()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0, 0, 0, 0);
    }
}
