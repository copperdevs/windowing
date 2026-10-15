using CopperDevs.Celesium;

namespace CopperDevs.Windowing;

public sealed partial class Window
{
    /// <summary>
    /// Current position of the window
    /// </summary>
    public Vector2Int Position
    {
        get => backend.GetWindowPosition();
        set => backend.SetWindowPosition(value);
    }

    /// <summary>
    /// Current size of the window
    /// </summary>
    public Vector2Int Size
    {
        get => backend.GetWindowSize();
        set => backend.SetWindowSize(value);
    }

    /// <summary>
    /// Current minimum size of the window
    /// </summary>
    public Vector2Int MinimumSize
    {
        get => backend.GetWindowMinimumSize();
        set => backend.SetWindowMinimumSize(value);
    }

    /// <summary>
    /// Current maximum size of the window
    /// </summary>
    public Vector2Int MaximumSize
    {
        get => backend.GetWindowMaximumSize();
        set => backend.SetWindowMaximumSize(value);
    }

    /// <summary>
    /// Current title of the window
    /// </summary>
    public string Title
    {
        get => backend.GetWindowTitle();
        set => backend.SetWindowTitle(value);
    }

    /// <summary>
    /// If the window should always be on top of everything else regardless of if focused or not
    /// </summary>
    public bool AlwaysOnTop
    {
        get => backend.GetAlwaysOnTop();
        set => backend.SetAlwaysOnTop(value);
    }

    /// <summary>
    /// Should the window be fullscreen
    /// </summary>
    public bool FullScreen
    {
        get => backend.GetFullscreen();
        set => backend.SetFullscreen(value);
    }

    /// <summary>
    /// Is the window currently minimized
    /// </summary>
    public bool Minimized => backend.GetMinimized();

    /// <summary>
    /// Is the window currently maximized
    /// </summary>
    public bool Maximized => backend.GetMaximized();

    /// <summary>
    /// Is the window currently being focused
    /// </summary>
    public bool Focused => backend.GetFocused();

    /// <summary>
    /// Is the window currently being hovered by the mouse
    /// </summary>
    public bool Hovered => backend.GetHovered();

    /// <summary>
    /// Maximize the window 
    /// </summary>
    public void Maximize() => backend.SetMaximize();

    /// <summary>
    /// Minimize the window
    /// </summary>
    public void Minimize() => backend.SetMinimize();
}