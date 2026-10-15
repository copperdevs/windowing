using System;
using static Windows.Win32.PInvoke;
using Windows.Win32.Foundation;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_EX_STYLE;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_STYLE;
using Windows.Win32.UI.WindowsAndMessaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Win32;

[SupportedOSPlatform("windows5.0")]
public class ThinWin32Window : SafeDisposable
{
    private SafeHandle hinst;
    private HWND hwndMain;


    public ThinWin32Window(WindowOptions options)
    {
        var className = options.Title.Replace(" ", "");

        if (options.GetType() == typeof(Win32WindowOptions))
        {
            var winOptions = (Win32WindowOptions)options;

            if (winOptions.WindowClassName is not null)
                className = winOptions.WindowClassName;
        }

        unsafe
        {

            hwndMain = CreateWindowEx(
                0,                      // no extended style
                className,              // class name
                options.Title,          // window name 
                WS_OVERLAPPEDWINDOW,    // window style
                CW_USEDEFAULT,          // default horizontal position
                CW_USEDEFAULT,          // default vertical position
                CW_USEDEFAULT,          // default width
                CW_USEDEFAULT,          // default width
                (HWND)null,             // no parent or owner window
                null,                   // class menu used 
                hinst,                  // instance handle 
                null);                  // no window creation data
        }

        ShowWindow(hwndMain, SHOW_WINDOW_CMD.SW_SHOW);
    }

    public override void DisposeResources()
    {

    }
}