using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using static WindowTracker;

public class WindowTracker : MonoBehaviour
{
    public WindowData RECT;
    [DllImport("user32.dll")] public static extern IntPtr GetTopWindow(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    public const uint GW_OWNER = 4;

    [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool IsIconic(IntPtr hWnd); // minimized
    [DllImport("user32.dll")] public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    public const int GWL_EXSTYLE = -20;

    // Extended window styles
    public const int WS_EX_TOOLWINDOW = 0x00000080;
    public const int WS_EX_APPWINDOW = 0x00040000;
    [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
    [DllImport("user32.dll")] public static extern int GetSystemMetrics(int nIndex);

    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;

    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out WindowData rect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    public const uint GW_HWNDNEXT = 2;

    // How many top windows you want
    public int maxWindows = 5;

    public List<WindowData> validWindows = new List<WindowData>();

    void UpdateWindows()
    {
        validWindows.Clear();
        List<WindowData> coveredRects = new List<WindowData>();
        IntPtr hWnd = GetTopWindow(IntPtr.Zero);
        while (hWnd != IntPtr.Zero)
        {
            if (GetWindowRect(hWnd, out WindowData rect))
            {
                bool isBlocking = IsBlockingWindow(hWnd, rect);
                if (isBlocking)
                {
                    // Check if it's usable and not fully covered
                    if (IsWindowValid(hWnd, rect) && !IsFullyCovered(rect, coveredRects))
                    {
                        rect = isPartiallyCovered(rect, validWindows);
                        validWindows.Add(rect);
                        UnityEngine.Debug.LogError("valid window found at: " + rect.x + "/" + rect.y + ", size: " + rect.Width + "x" + rect.Height);
                    }
                    // add blocking windows to coverage
                    coveredRects.Add(rect);
                }
            }
            hWnd = GetWindow(hWnd, GW_HWNDNEXT);
        }
    }

    public void recieveData(bool windowsDirty)
    {
        if (!windowsDirty)
            return;
        windowsDirty = false;
        UpdateWindows();
        WindowManager.instance.SendDataToDebugRenderer(validWindows);
        PathManager.instance.SendDataToPathTracker(validWindows);
    }
    bool IsBlockingWindow(IntPtr hWnd, WindowData rect)
    {
        return WindowManager.instance.IsBlockingWindow(hWnd, rect);
    }

    bool IsUsableWindow(IntPtr hWnd)
    {
        return WindowManager.instance.IsUsableWindow(hWnd);
    }

    bool IsUsableRect(WindowData rect)
    {
        // Skip fullscreen
        int screenWidth = GetSystemMetrics(SM_CXSCREEN);
        int screenHeight = GetSystemMetrics(SM_CYSCREEN);
        //UnityEngine.Debug.Log("screen width = " + screenWidth + ", screen height = " + screenHeight);

        bool isFullscreen =
            rect.Width >= screenWidth * 0.95f &&
            rect.Height >= screenHeight * 0.95f;

        if (isFullscreen)
            return false;

        return true;
    }

    bool IsWindowValid(IntPtr hwnd, WindowData rect)
    {
        if (IsUsableWindow(hwnd) && IsUsableRect(rect))
            return true;

        return false;
    }

    bool RectContains(WindowData outer, WindowData inner)
    {
        return inner.Left >= outer.Left &&
               inner.Right <= outer.Right &&
               inner.Top >= outer.Top &&
               inner.Bottom <= outer.Bottom;
    }

    bool IsFullyCovered(WindowData rect, List<WindowData> coveredRects)
    {
        foreach (var cover in coveredRects)
        {
            if (RectContains(cover, rect))
                return true;
        }
        return false;
    }

    WindowData isPartiallyCovered(WindowData rect, List<WindowData> validWindows)
    {
        Vector4 coveredCorners = new Vector4(1, 1, 1, 1);

        Vector2 topLeft = new Vector2(rect.Left, rect.Top);
        Vector2 topRight = new Vector2(rect.Right, rect.Top);
        Vector2 bottomLeft = new Vector2(rect.Left, rect.Bottom);
        Vector2 bottomRight = new Vector2(rect.Right, rect.Bottom);

        foreach (var cover in validWindows)
        {
            if (PointContains(topLeft, cover))
            {
                coveredCorners.x = 0;
                rect.IntersectionTopLeft = new Vector4(cover.Right, rect.Top, rect.Left, cover.Bottom);
            }
            if (PointContains(topRight, cover))
            {
                coveredCorners.y = 0;
                rect.IntersectionTopRight = new Vector4(rect.Right, cover.Bottom, cover.Left, rect.Top);
            }
            if (PointContains(bottomLeft, cover))
            {
                coveredCorners.z = 0;
                rect.IntersectionBottomLeft = new Vector4(rect.Left, cover.Top, cover.Right, rect.Bottom);
            }
            if (PointContains(bottomRight, cover))
            {
                coveredCorners.w = 0;
                rect.IntersectionBottomRight = new Vector4(cover.Left, rect.Bottom, rect.Right, cover.Top);
            }
        }

        rect.VisibleCorners = coveredCorners;
        return rect;
    }

    bool PointContains(Vector2 point, WindowData rect)
    {
        return point.x >= rect.Left &&
               point.x <= rect.Right &&
               point.y >= rect.Top &&
               point.y <= rect.Bottom;
    }
    /*
    Vector4 PlatformContains(Vector4 edge, WindowData rect)
    {
        if (edge.x <= rect.Top && edge.x >= rect.Bottom)
        {
            return new Vector4(edge.x, rect.Left, edge.x, rect.Right);
        }
    }

    Vector4 WallContains(Vector4 edge, WindowData rect)
    {

    }
    */
}
    