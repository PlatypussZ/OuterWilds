using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class OverlayManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy,
        uint uFlags
    );

    //constraints

    const int GWL_EXSTYLE = -20;

    const int WS_EX_LAYERED = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOSIZE = 0x0001;



    //#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    const int GWL_STYLE   = -16;
        const int WS_BORDER   = 0x00800000;
        const int WS_DLGFRAME = 0x00400000;
        const int WS_CAPTION  = WS_BORDER | WS_DLGFRAME;

        [StructLayout(LayoutKind.Sequential)]
        struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong
            (IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern int DwmExtendFrameIntoClientArea
            (IntPtr hWnd, ref MARGINS pMargins);

        void Start()
        {
            if (Application.isEditor) { return; }

            IntPtr hwnd = GetActiveWindow();

            int style = GetWindowLong(hwnd, GWL_STYLE);
            style &= ~WS_CAPTION;
            SetWindowLong(hwnd, GWL_STYLE, style);

            var margins = new MARGINS
            {
                cxLeftWidth = -1,
                cxRightWidth = 0,
                cyTopHeight = 0,
                cyBottomHeight = 0
            };
            DwmExtendFrameIntoClientArea(hwnd, ref margins);

            //NEW: Always on top
            SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

            //NEW: Click-through
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle);
    }
//#endif
}