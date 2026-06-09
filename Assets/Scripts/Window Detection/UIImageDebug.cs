using UnityEngine;
using System.Collections.Generic;

public class WindowDebugRenderer : MonoBehaviour
{
    public RectTransform canvas; // assign your canvas
    public GameObject rectPrefab;

    List<GameObject> activeRects = new List<GameObject>();

    public void DrawWindows(List<WindowData> windows)
    {
        
        // Clear old rects
        foreach (var rect in activeRects)
            Destroy(rect);

        activeRects.Clear();

        foreach (var win in windows)
        {
            //Debug.Log("Valid window found at: " + win.x + ", " + win.y + ", size: " + win.Width + "x" + win.Height);
            CreateRect(win);
        }
    }

    void CreateRect(WindowData win)
    {
        GameObject go = Instantiate(rectPrefab, canvas);
        RectTransform rt = go.GetComponent<RectTransform>();

        // Convert screen coords → Unity UI coords
        Vector2 pos = new Vector2(win.x + win.Width/2, win.y + win.Height/2);
        Vector2 size = new Vector2(win.Width, win.Height);

        rt.anchoredPosition = ConvertToCanvasPosition(pos);
        rt.sizeDelta = size;

        activeRects.Add(go);
    }

    Vector2 ConvertToCanvasPosition(Vector2 screenPos)
    {
        // Flip Y because Windows = top-left origin, Unity = bottom-left
        return new Vector2(
            screenPos.x,
            Screen.currentResolution.height - screenPos.y
        );
    }
}