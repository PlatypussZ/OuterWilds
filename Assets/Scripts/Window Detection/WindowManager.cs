using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using static WindowTracker;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    private WindowDebugRenderer debugRenderer;
    private WindowEvents windowEvents;
    private WindowTracker windowTracker;

    public List<string> blacklistedProcesses;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject rectPrefab;

    void Awake()
    {
        UnityEngine.Debug.developerConsoleEnabled = true;
        UnityEngine.Debug.developerConsoleVisible = true;
        Application.runInBackground = true;

        SingletonPattern();
        GameObject scriptHolder = new GameObject("Windows script Holder");
        windowTracker = scriptHolder.AddComponent<WindowTracker>();
        windowEvents = scriptHolder.AddComponent<WindowEvents>();
        debugRenderer = scriptHolder.AddComponent<WindowDebugRenderer>();
            debugRenderer.canvas = canvas;
            debugRenderer.rectPrefab = rectPrefab;
    }

    public void SendDataToWindowTracker(bool data)
    {
        windowTracker.recieveData(data);

    }
    public void SendDataToDebugRenderer( List<WindowData> validWindowsList )
    {
        debugRenderer.DrawWindows(validWindowsList);
    }
    private void SingletonPattern()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public bool IsBlockingWindow(IntPtr hWnd, WindowData rect)
    {
        if (!IsUsableWindow(hWnd))
            return false;
        
        // Skip invisible
        if (!IsWindowVisible(hWnd))
            return false;

        // Skip minimized
        if (IsIconic(hWnd))
            return false;

        // Skip very small windows
        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;
        if (width < 50 || height < 50)
            return false;

        // Skip blacklisted
        string process = GetProcessName(hWnd);
        //UnityEngine.Debug.LogError(process);
        if (IsBlacklistedProcess(process))
            return false;

        // Passed all filters
        return true;
    }

    public bool IsUsableWindow(IntPtr hWnd)
    {
        // Must have a title
        StringBuilder sb = new StringBuilder(256);
        GetWindowText(hWnd, sb, sb.Capacity);

        if (string.IsNullOrWhiteSpace(sb.ToString()))
            return false;

        // Skip tool windows
        int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        if ((exStyle & WS_EX_TOOLWINDOW) != 0)
            return false;

        // Skip windows with owner
        if (GetWindow(hWnd, GW_OWNER) != IntPtr.Zero)
            return false;

        // Skip blacklisted
        string process = GetProcessName(hWnd);
        
        if (IsBlacklistedProcess(process))
            return false;

        // Passed all filters
        return true;
    }

    string GetProcessName(IntPtr hWnd)
    {
        GetWindowThreadProcessId(hWnd, out uint processId);

        try
        {
            Process proc = Process.GetProcessById((int)processId);
            return proc.ProcessName;
        }
        catch
        {
            return "Unknown";
        }
    }

    string GetWindowTitle(IntPtr hWnd)
    {
        StringBuilder sb = new StringBuilder(256);
        GetWindowText(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    bool IsBlacklistedProcess(string process)
    {
        foreach (string blackListedProcess in blacklistedProcesses)
        {
            if (process == blackListedProcess)
                return true;
        }
        return false;
    }
}
