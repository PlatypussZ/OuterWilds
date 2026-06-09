using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;

public class WindowEvents : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr SetWinEventHook(
        uint eventMin,
        uint eventMax,
        IntPtr hmodWinEventProc,
        WinEventDelegate lpfnWinEventProc,
        uint idProcess,
        uint idThread,
        uint dwFlags
    );

    delegate void WinEventDelegate(
        IntPtr hWinEventHook,
        uint eventType,
        IntPtr hwnd,
        int idObject,
        int idChild,
        uint dwEventThread,
        uint dwmsEventTime
    );

    const uint EVENT_OBJECT_CREATE = 0x8000;
    const uint EVENT_OBJECT_DESTROY = 0x8001;
    const uint EVENT_OBJECT_SHOW = 0x8002;
    const uint EVENT_OBJECT_HIDE = 0x8003;
    const uint EVENT_OBJECT_LOCATIONCHANGE = 0x800B;

    const uint WINEVENT_OUTOFCONTEXT = 0;

    IntPtr hook;
    WinEventDelegate callback;

    float lastEventTime;
    float delay = 0f;

    bool pendingUpdate = true;

    void Start()
    {
        Application.runInBackground = true;

        callback = new WinEventDelegate(OnWindowEvent);

        hook = SetWinEventHook(
            EVENT_OBJECT_CREATE,
            EVENT_OBJECT_LOCATIONCHANGE,
            IntPtr.Zero,
            callback,
            0,
            0,
            WINEVENT_OUTOFCONTEXT
        );
    }

    void OnWindowEvent(
        IntPtr hWinEventHook,
        uint eventType,
        IntPtr hwnd,
        int idObject,
        int idChild,
        uint dwEventThread,
        uint dwmsEventTime)
    {
        if (idObject != 0 || hwnd == IntPtr.Zero)
            return;

        if (!WindowManager.instance.IsUsableWindow(hwnd))
            return;

        lastEventTime = Time.realtimeSinceStartup;
        pendingUpdate = true;
    }

    void Update()
    {
        if (!pendingUpdate)
            return;

        if (Time.realtimeSinceStartup - lastEventTime < delay)
            return;

        pendingUpdate = false;

        WindowManager.instance.SendDataToWindowTracker(true);
    }

    [DllImport("user32.dll")]
    static extern bool UnhookWinEvent(IntPtr hWinEventHook);

    void OnDestroy()
    {
        if (hook != IntPtr.Zero)
            UnhookWinEvent(hook);
    }
}