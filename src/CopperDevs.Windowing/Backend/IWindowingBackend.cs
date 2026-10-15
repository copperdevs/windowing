using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Backend;

public interface IWindowingBackend : IDisposable
{
    public void CreateWindow(WindowOptions options);
    public void StartWindowUpdate();
    public void StopWindowUpdate();
    public void DestroyWindow();

    // TODO: have supported for handling a PlatformNotSupportedException being thrown here
    //  throw a proper relevant error internally, as well as add an option to override this input backend
    public IInput GetRelevantInputBackend();


    // time
    
    /// <remarks>value in milliseconds</remarks>
    public virtual long GetTotalTime() => throw new NotSupportedException();
    
    /// <remarks>value in milliseconds</remarks>
    public virtual long GetDeltaTime() => throw new NotSupportedException();

    // size
    public Vector2Int GetWindowSize();
    public void SetWindowSize(Vector2Int size);

    // max size
    public Vector2Int GetWindowMaximumSize();
    public void SetWindowMaximumSize(Vector2Int size);

    // min size
    public Vector2Int GetWindowMinimumSize();
    public void SetWindowMinimumSize(Vector2Int size);

    // pos
    public Vector2Int GetWindowPosition();
    public void SetWindowPosition(Vector2Int position);

    // title
    public string GetWindowTitle();
    public void SetWindowTitle(string title);

    // full screen
    public void SetFullscreen(bool fullscreen);
    public bool GetFullscreen();

    // always on top
    public void SetAlwaysOnTop(bool alwaysOnTop);
    public bool GetAlwaysOnTop();

    // minimized
    public bool GetMinimized();
    public void SetMinimize();

    // maximized
    public bool GetMaximized();
    public void SetMaximize();

    // focus
    public bool GetFocused();
    public bool GetHovered();
}