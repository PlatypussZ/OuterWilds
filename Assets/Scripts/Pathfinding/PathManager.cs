using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;

    private PathTracker pathTracker;
    private NodeController nodeController;
    private NodeDebugger nodeDebugger;
    [SerializeField] private GameObject CirclePrefab;
    [SerializeField] private RectTransform canvas;

    private void Awake()
    {
        SingletonPattern();
        GameObject scriptHolder = new GameObject("Pathfinding Script Holder");

        pathTracker = scriptHolder.AddComponent<PathTracker>();
        nodeController = scriptHolder.AddComponent<NodeController>();
        nodeDebugger = scriptHolder.AddComponent<NodeDebugger>();
            nodeDebugger.circlePrefab = CirclePrefab;
            nodeDebugger.canvas = canvas;
    }

    private void SingletonPattern()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void SendDataToPathTracker(List<WindowData> validWindows)
    {
        pathTracker.recieveData(validWindows);
    }

    public void SendDataToNodeController(List<PathData> paths, List<PathData> walls)
    {
        nodeController.recieveData(paths, walls);
    }

    public void SendDataToNodeDebugger(List<NodeData> nodes)
    {
        nodeDebugger.recieveData(nodes);
    }
}
