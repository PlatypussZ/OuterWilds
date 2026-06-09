using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct WindowData
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
    public int Width => Right - Left;
    public int Height => Bottom - Top;
    public int x => Left;
    public int y => Top;


    public Vector4 VisibleCorners; //vec4(topleft, topright, bottomleft, bottomright), 1 for visible and 0 for invisible

    public Vector4 IntersectionTopLeft; //xy = first interection, zw = xy for second intersection (first = clockwise from corner, second = counterclockwise)
    public Vector4 IntersectionTopRight;
    public Vector4 IntersectionBottomLeft;
    public Vector4 IntersectionBottomRight;

}
