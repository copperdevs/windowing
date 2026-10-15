using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing;

public partial class Window
{
    /// <summary>
    /// Should the window still be updating
    /// </summary>
    protected bool ShouldRun = true;

    /// <summary>
    /// Start the windowing system once setup
    /// </summary>
    public void Run()
    {
        Time.Setup(this);
        SetupInput();

        OnLoad?.Invoke();

        while (ShouldRun)
            RenderWindow();

        OnClose?.Invoke();
        DestroyWindow();
    }

    /// <summary>
    /// Shutdown the window
    /// </summary>
    public void Stop()
    {
        ShouldRun = false;
    }

    /// <summary>
    /// Render a frame of the window
    /// </summary>
    public void RenderWindow()
    {
        OnPreUpdate?.Invoke();
        
        UpdateInput();
        StartWindowUpdate();

        OnUpdate?.Invoke();
        OnRender?.Invoke();

        if (Options.FrameRenderDelay > 0)
            Thread.Sleep(Options.FrameRenderDelay);

        StopWindowUpdate();
    }
}