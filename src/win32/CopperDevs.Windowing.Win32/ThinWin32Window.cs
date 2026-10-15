using System;
using System.Runtime.CompilerServices;
using static Windows.Win32.PInvoke;
using Windows.Win32.Foundation;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_EX_STYLE;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_STYLE;
using Windows.Win32.UI.WindowsAndMessaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Win32;

[SupportedOSPlatform("windows5.0")]
internal unsafe class ThinWin32Window : SafeDisposable
{
    private HINSTANCE hinstance;
    private HWND hwndMain;
    private readonly WNDCLASSW wndClass;

    private const string WindowClassName = "CopperWindowing";

    public ThinWin32Window(WindowOptions options)
    {
        hinstance = new HINSTANCE();

        hinstance = (HINSTANCE)GetModuleHandle((PCWSTR)null).Value;

        fixed (char* className = WindowClassName.ToCharArray())
        {
            wndClass = new WNDCLASSW()
            {
                lpfnWndProc = WindowProc,
                hInstance = hinstance,
                lpszClassName = new PCWSTR(className)
            };

            if (RegisterClass(wndClass) == 0)
            {
                var error = Marshal.GetLastPInvokeError();
                throw new InvalidOperationException($"RegisterClass failed with Win32 error {error}.");
            }

            fixed (char* titleName = options.Title.ToCharArray())
            {
                hwndMain = CreateWindowEx
                (
                    0, // no extended style
                    className, // class name
                    titleName, // window name 
                    WS_OVERLAPPEDWINDOW, // window style
                    CW_USEDEFAULT, // default horizontal position
                    CW_USEDEFAULT, // default vertical position
                    CW_USEDEFAULT, // default width
                    CW_USEDEFAULT, // default width
                    (HWND)null, // no parent or owner window
                    (HMENU)null, // class menu used 
                    hinstance, // instance handle 
                    null // no window creation data}
                );


                if (hwndMain.IsNull)
                {
                    var error = Marshal.GetLastPInvokeError();
                    UnregisterClass(new PCWSTR(className), hinstance);

                    throw new InvalidOperationException($"CreateWindowEx failed with Win32 error {error}.");
                }

                ShowWindow(hwndMain, SHOW_WINDOW_CMD.SW_SHOW);
            }
        }
    }

    private const int EventsPerPeep = 64;
    public Action<MSG> HandleEvent = null!;

    public bool RequestsShutdown { get; private set; }

    public void Poll()
    {
        for (var i = 0; i < EventsPerPeep; i++)
        {
            if (!PeekMessage(out var msg, (HWND)null, 0, 0, PEEK_MESSAGE_REMOVE_TYPE.PM_REMOVE))
                break;

            if (msg.message is WM_QUIT)
            {
                RequestsShutdown = true;
                break;
            }

            TranslateMessage(msg);
            DispatchMessage(msg);

            HandleEvent?.Invoke(msg);
        }
    }

    private static LRESULT WindowProc(HWND hwnd, uint msg, WPARAM wParam, LPARAM lParam)
    {
        switch (msg)
        {
            case WM_CLOSE:
                DestroyWindow(hwnd);
                return new LRESULT(0);

            case WM_DESTROY:
                PostQuitMessage(0);
                return new LRESULT(0);
        }

        return DefWindowProc(hwnd, msg, wParam, lParam);
    }

    public override void DisposeResources()
    {
        if (!hwndMain.IsNull)
            DestroyWindow(hwndMain);

        fixed (char* classNamePtr = WindowClassName.ToCharArray())
            UnregisterClass(new PCWSTR(classNamePtr), hinstance);
    }
}