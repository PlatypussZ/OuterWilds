using System.Collections.Generic;
using UnityEngine;

public class NodeDebugger : MonoBehaviour
{
    public RectTransform canvas;
    public GameObject circlePrefab;
    public float nodeRadius;

    private List<GameObject> activeCircles = new List<GameObject>();
    public void recieveData(List<NodeData> nodes)
    {
        foreach (var circle in activeCircles)
            Destroy(circle);

        activeCircles.Clear();

        foreach (var node in nodes)
        {
            CreateCircle(node);
        }
    }

    void CreateCircle(NodeData node)
    {
        GameObject go = Instantiate(circlePrefab, canvas);
        RectTransform rt = go.GetComponent<RectTransform>();

        // Convert screen coords → Unity UI coords
        Vector2 pos = new Vector2(node.coords.x - (Screen.currentResolution.width / 2), node.coords.y + (Screen.currentResolution.height / 2));

        rt.anchoredPosition = ConvertToCanvasPosition(pos);

        activeCircles.Add(go);
    }

    Vector2 ConvertToCanvasPosition(Vector2 screenPos)
    {
        // Flip Y because Windows = top-left origin, Unity = bottom-left
        return new Vector2(
        screenPos.x,
        Screen.currentResolution.height - screenPos.y);
    }
}
