using System.Runtime.Versioning;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Backend;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Win32;

[SupportedOSPlatform("windows5.1.2600")]
public class Win32Backend : IWindowingBackend
{
    private ThinWin32Window window = null!;

    public void CreateWindow(WindowOptions options)
    {
        window = new ThinWin32Window(options);
    }

    public void StartWindowUpdate()
    {
        window.HandleUnresolvedMessages(_ => {});
    }

    public void StopWindowUpdate()
    {
        window.PurgeUnhandledMessages(); // probs not the best approach
    }

    public void DestroyWindow()
    {
        window.Shutdown();
    }

    public bool RequestsStop()
    {
        return window.RequestsShutdown;
    }

    public IInput GetRelevantInputBackend()
    {
        return new Win32Input();
    }

    public Vector2Int GetWindowSize()
    {
        throw new NotImplementedException();
    }

    public void SetWindowSize(Vector2Int size)
    {
        throw new NotImplementedException();
    }

    public Vector2Int GetWindowMaximumSize()
    {
        throw new NotImplementedException();
    }

    public void SetWindowMaximumSize(Vector2Int size)
    {
        throw new NotImplementedException();
    }

    public Vector2Int GetWindowMinimumSize()
    {
        throw new NotImplementedException();
    }

    public void SetWindowMinimumSize(Vector2Int size)
    {
        throw new NotImplementedException();
    }

    public Vector2Int GetWindowPosition()
    {
        throw new NotImplementedException();
    }

    public void SetWindowPosition(Vector2Int position)
    {
        throw new NotImplementedException();
    }

    public string GetWindowTitle()
    {
        throw new NotImplementedException();
    }

    public void SetWindowTitle(string title)
    {
        throw new NotImplementedException();
    }

    public void SetFullscreen(bool fullscreen)
    {
        throw new NotImplementedException();
    }

    public bool GetFullscreen()
    {
        throw new NotImplementedException();
    }

    public void SetAlwaysOnTop(bool alwaysOnTop)
    {
        throw new NotImplementedException();
    }

    public bool GetAlwaysOnTop()
    {
        throw new NotImplementedException();
    }

    public bool GetMinimized()
    {
        throw new NotImplementedException();
    }

    public void SetMinimize()
    {
        throw new NotImplementedException();
    }

    public bool GetMaximized()
    {
        throw new NotImplementedException();
    }

    public void SetMaximize()
    {
        throw new NotImplementedException();
    }

    public bool GetFocused()
    {
        throw new NotImplementedException();
    }

    public bool GetHovered()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}