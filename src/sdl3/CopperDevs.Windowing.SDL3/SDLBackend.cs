using CopperDevs.Celesium;
using CopperDevs.Windowing.Backend;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.SDL3;

public class SDLBackend : IWindowingBackend
{
    private ThinSDLWindow window = null!;
    private unsafe int* tempX = null, tempY = null;

    private bool shouldRun = true;

    public ThinSDLWindow GetBackingWindow() => window;

    private void HandleEvent(SDL_Event e)
    {
        shouldRun = e.Type switch
        {
            SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED or SDL_EventType.SDL_EVENT_QUIT => false,
            _ => shouldRun
        };
    }

    public void CreateWindow(WindowOptions options)
    {
        window = new ThinSDLWindow(options);
        window.OnEvent += HandleEvent;
    }

    public void StartWindowUpdate()
    {
        window.PollEvents();
    }

    public void StopWindowUpdate()
    {
    }

    public void DestroyWindow()
    {
        window.Dispose();
    }

    public bool RequestsStop()
    {
        return !shouldRun;
    }

    public long GetTotalTime()
    {
        return (long)SDL_GetTicks();
    }

    public IInput GetRelevantInputBackend()
    {
        return new SDLInput(this, window);
    }

    public unsafe Vector2Int GetWindowSize()
    {
        SDL_GetWindowSize(window.GetNative(), tempX, tempY);
        return new Vector2Int(*tempX, *tempY);
    }

    public unsafe void SetWindowSize(Vector2Int size)
    {
        SDL_SetWindowSize(window.GetNative(), size.X, size.Y);
    }

    public unsafe Vector2Int GetWindowMaximumSize()
    {
        SDL_GetWindowMaximumSize(window.GetNative(), tempX, tempY);
        return new Vector2Int(*tempX, *tempY);
    }

    public unsafe void SetWindowMaximumSize(Vector2Int size)
    {
        SDL_SetWindowMaximumSize(window.GetNative(), size.X, size.Y);
    }

    public unsafe Vector2Int GetWindowMinimumSize()
    {
        SDL_GetWindowMinimumSize(window.GetNative(), tempX, tempY);
        return new Vector2Int(*tempX, *tempY);
    }

    public unsafe void SetWindowMinimumSize(Vector2Int size)
    {
        SDL_SetWindowMinimumSize(window.GetNative(), size.X, size.Y);
    }

    public unsafe Vector2Int GetWindowPosition()
    {
        SDL_GetWindowPosition(window.GetNative(), tempX, tempY);
        return new Vector2Int(*tempX, *tempY);
    }

    public unsafe void SetWindowPosition(Vector2Int position)
    {
        SDL_SetWindowPosition(window.GetNative(), position.X, position.Y);
    }

    public unsafe string GetWindowTitle()
    {
        return SDL_GetWindowTitle(window.GetNative()) ?? string.Empty;
    }

    public unsafe void SetWindowTitle(string title)
    {
        SDL_SetWindowTitle(window.GetNative(), title);
    }

    public unsafe void SetFullscreen(bool fullscreen)
    {
        SDL_SetWindowFullscreen(window.GetNative(), fullscreen);
    }

    public unsafe bool GetFullscreen()
    {
        return SDL_GetWindowFlags(window.GetNative()).HasFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
    }

    public unsafe void SetAlwaysOnTop(bool alwaysOnTop)
    {
        SDL_SetWindowAlwaysOnTop(window.GetNative(), alwaysOnTop);
    }

    public unsafe bool GetAlwaysOnTop()
    {
        return SDL_GetWindowFlags(window.GetNative()).HasFlag(SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP);
    }

    public unsafe bool GetMinimized()
    {
        return SDL_GetWindowFlags(window.GetNative()).HasFlag(SDL_WindowFlags.SDL_WINDOW_MINIMIZED);
    }

    public unsafe void SetMinimize()
    {
        SDL_MinimizeWindow(window.GetNative());
    }

    public unsafe bool GetMaximized()
    {
        return SDL_GetWindowFlags(window.GetNative()).HasFlag(SDL_WindowFlags.SDL_WINDOW_MAXIMIZED);
    }

    public unsafe void SetMaximize()
    {
        SDL_MaximizeWindow(window.GetNative());
    }

    public unsafe bool GetFocused()
    {
        return SDL_GetWindowFlags(window.GetNative()).HasFlag(SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS);
    }

    public unsafe bool GetHovered()
    {
        return SDL_GetWindowFlags(window.GetNative()).HasFlag(SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS);
    }


    public void Dispose()
    {
        window.Dispose();
    }
}