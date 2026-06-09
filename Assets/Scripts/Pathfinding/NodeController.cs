using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VectorGraphics;

public class NodeController : MonoBehaviour
{
    private List<NodeData> nodes = new List<NodeData>();

    public void recieveData(List<PathData> paths, List<PathData> walls)
    {
        nodes.Clear();

        foreach (PathData path in paths)
        {
            NodeData node1 = new NodeData();
            node1.coords = path.coord1;
            node1.type = "top left corner";

            NodeData node2 = new NodeData();
            node2.coords = path.coord2;
            node2.type = "top right corner";

            nodes.Add(node1);
            nodes.Add(node2);
        }

        foreach (PathData wall in walls)
        {
            NodeData node1 = new NodeData();
            node1.coords = wall.coord1;
            node1.type = "bottom left corner";

            NodeData node2 = new NodeData();
            node2.coords = wall.coord2;
            node2.type = "bottom right corner";

            nodes.Add(node1);
            nodes.Add(node2);
        }

        PathManager.instance.SendDataToNodeDebugger(nodes);

    }
}
