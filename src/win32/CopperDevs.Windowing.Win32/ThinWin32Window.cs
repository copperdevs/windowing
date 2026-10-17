using System.Collections.Concurrent;
using static Windows.Win32.PInvoke;
using Windows.Win32.Foundation;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_STYLE;
using Windows.Win32.UI.WindowsAndMessaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Win32;

// TODO: lotta thread handling to deal with here :p
[SupportedOSPlatform("windows5.1.2600")]
internal class ThinWin32Window
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly Thread windowThread;

    private const string WindowClassName = "CopperWindowing"; // TODO: make the window class be registered once per lifetime
    private WNDCLASSW wndClass;

    private HINSTANCE hInstance;
    private HWND hwndMain;

    private readonly WindowOptions options;

    public bool RequestsShutdown { get; private set; }

    private readonly ConcurrentQueue<MSG> unresolvedMessages = new();

    private volatile bool isAlive;

    public void HandleUnresolvedMessages(Action<MSG> action)
    {
        while (unresolvedMessages.TryDequeue(out var msg))
            action(msg);
    }

    public void PurgeUnhandledMessages()
    {
        unresolvedMessages.Clear();
    }

    public ThinWin32Window(WindowOptions options)
    {
        this.options = options;

        windowThread = new Thread(WindowMain)
        {
            IsBackground = true,
            Name = $"{WindowClassName} Window",
        };

        windowThread.Start();
    }

    private void WindowMain()
    {
        try
        {
            WindowSetup();
        }
        catch (Exception e)
        {
            Log.Fatal($"Failed to setup the window: {e.Message}");
            Log.Exception(e);
            return;
        }

        try
        {
            WindowMessageHandling();
        }
        catch (Exception e)
        {
            Log.Fatal($"Failure while handling messages: {e.Message}");
            Log.Exception(e);
        }
        finally
        {
            WindowShutdown();
        }
    }

    private unsafe void WindowSetup()
    {
        hInstance = (HINSTANCE)GetModuleHandle((PCWSTR)null).Value;

        fixed (char* className = WindowClassName.ToCharArray())
        fixed (char* titleName = options.Title.ToCharArray())
        {
            var classNameStr = new PCWSTR(className);
            var titleNameStr = new PCWSTR(titleName);

            wndClass = new WNDCLASSW
            {
                lpfnWndProc = WindowProc,
                hInstance = hInstance,
                lpszClassName = classNameStr
            };

            if (RegisterClass(wndClass) == 0)
            {
                var error = Marshal.GetLastPInvokeError();
                throw new InvalidOperationException($"RegisterClass failed with Win32 error {error}.");
            }

            if (TryCreateWindow(classNameStr, titleNameStr, hInstance, out hwndMain))
            {
                isAlive = true;
                ShowWindow(hwndMain, SHOW_WINDOW_CMD.SW_SHOW);
            }
            else
            {
                var error = Marshal.GetLastPInvokeError();
                UnregisterClass(new PCWSTR(className), hInstance);

                throw new InvalidOperationException($"CreateWindowEx failed with Win32 error {error}.");
            }
        }
    }

    private unsafe void WindowMessageHandling()
    {
        MSG msg = default;

        while (true)
        {
            var result = GetMessage(&msg, (HWND)null, 0, 0);

            if (result == -1)
                throw new InvalidOperationException($"GetMessage failed {Marshal.GetLastPInvokeError()}.");

            if (msg.message is WM_QUIT)
            {
                RequestsShutdown = true;
                break;
            }

            TranslateMessage(msg);
            DispatchMessage(msg);

            unresolvedMessages.Enqueue(msg);
        }
    }

    private unsafe void WindowShutdown()
    {
        isAlive = false;

        if (!hwndMain.IsNull)
            DestroyWindow(hwndMain);

        hwndMain = default;

        fixed (char* classNamePtr = WindowClassName.ToCharArray())
            UnregisterClass(new PCWSTR(classNamePtr), hInstance);
    }

    private static unsafe bool TryCreateWindow(PCWSTR className, PCWSTR titleName, HINSTANCE hinstance, out HWND hwnd)
    {
        hwnd = CreateWindowEx
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
            null // no window creation data
        );

        return !hwnd.IsNull;
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

    public void Shutdown()
    {
        var hwnd = hwndMain;
        if (!isAlive || hwnd.IsNull || !IsWindow(hwnd))
            return;

        if (PostMessage(hwnd, WM_CLOSE, default, default))
            return;
        
        throw new InvalidOperationException($"{nameof(PostMessage)} failed {Marshal.GetLastPInvokeError()}.");
    }
}