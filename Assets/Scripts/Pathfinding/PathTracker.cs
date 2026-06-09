using UnityEngine;
using System.Collections.Generic;

public class PathTracker : MonoBehaviour
{
    private List<PathData> Paths = new List<PathData>();
    private List<PathData> Walls = new List<PathData>();
    private Vector2 IgnorePath = new Vector2(10000000000000, 10000000000000);

    public void assignScreenBoundaries()
    {
        PathData floor = new PathData();
        floor.coord1 = Vector2.zero;
        floor.coord2.x = Screen.width;
        floor.coord2.y = 0;

        PathData leftBorder = new PathData();
        leftBorder.coord1 = Vector2.zero;
        leftBorder.coord2.x = 0;
        leftBorder.coord2.y = Screen.height;

        PathData rightBorder = new PathData();
        rightBorder.coord1.x = Screen.width;
        rightBorder.coord1.y = 0;
        rightBorder.coord2.x = Screen.width;
        rightBorder.coord2.y = Screen.height;

        Paths.Add(floor);
        Walls.Add(leftBorder);
        Walls.Add(rightBorder);

    }
    public void recieveData( List<WindowData> validWindows )
    {
        Paths.Clear();
        Walls.Clear();

        assignScreenBoundaries();

        for (int i = 0; i < validWindows.Count; i++)
        {
            WindowData window = validWindows[i];

            PathData LeftWall;
            PathData RightWall;
            PathData Platform;

            if (window.VisibleCorners.x == 1)
            {
                Platform.coord1 = new Vector2(window.Left, window.Top);
                LeftWall.coord2 = new Vector2(window.Left, window.Top);
            }
            else
            {
                LeftWall.coord2 = new Vector2(window.IntersectionTopLeft.z, window.IntersectionTopLeft.w);
                if (window.VisibleCorners.y == 1)
                    Platform.coord1 = new Vector2(window.IntersectionTopLeft.x, window.IntersectionTopLeft.y);
                else
                    Platform.coord1 = IgnorePath;
            }
            
            if(window.VisibleCorners.y == 1)
            {
                Platform.coord2 = new Vector2(window.Right, window.Top);
                RightWall.coord2 = new Vector2(window.Right, window.Top);
            }
            else
            {
                Platform.coord2 = new Vector2(window.IntersectionTopRight.z, window.IntersectionTopRight.w);
                RightWall.coord2 = new Vector2(window.IntersectionTopRight.x, window.IntersectionTopRight.y);

            }
            
            if (window.VisibleCorners.z == 1)
            {
                LeftWall.coord1 = new Vector2(window.Left, window.Bottom);
            }
            else
            {
                if (window.VisibleCorners.x == 1)
                    LeftWall.coord1 = new Vector2(window.IntersectionBottomLeft.x, window.IntersectionBottomLeft.y);
                else
                    LeftWall.coord1 = IgnorePath;
            }
            
            if (window.VisibleCorners.w == 1)
            {
                RightWall.coord1 = new Vector2(window.Right, window.Bottom);
            }
            else
            {
                if (window.VisibleCorners.y == 1)
                    RightWall.coord1 = new Vector2(window.IntersectionBottomRight.z, window.IntersectionBottomRight.w);
                else
                    RightWall.coord1 = IgnorePath;
            }

            if (Platform.coord1 != IgnorePath && Platform.coord1 != IgnorePath)
                Paths.Add(Platform);
            if (LeftWall.coord1 != IgnorePath && LeftWall.coord2 != IgnorePath)
                Walls.Add(LeftWall);
            if (RightWall.coord1 != IgnorePath && LeftWall.coord2 != IgnorePath)
                Walls.Add(RightWall);
        }

        PathManager.instance.SendDataToNodeController(Paths, Walls);

    }
}
