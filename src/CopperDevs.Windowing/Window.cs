using CopperDevs.Celesium;
using CopperDevs.Windowing.Backend;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing;

/// <summary>
/// Base window
/// </summary>
public sealed partial class Window : SafeDisposable
{
    private readonly WindowOptions options;
    private readonly IWindowingBackend backend;
    private IInput input;

    private readonly long startTime;
    private long previousFrameStartTime;
    private long currentFrameDeltaTime;

    /// <summary>
    /// Should the window still be updating
    /// </summary>
    private bool shouldRun;

    public Action OnLoad = null!;
    public Action OnFrameStart = null!;
    public Action OnUpdate = null!;
    public Action OnRender = null!;
    public Action OnFrameEnd = null!;
    public Action OnClose = null!;

    public Window(IWindowingBackend backend) : this(backend, WindowOptions.Default)
    {
    }

    public Window(IWindowingBackend backend, WindowOptions options)
    {
        shouldRun = true;

        this.backend = backend;
        this.options = options;

        startTime = TimeUtility.UtcNow();
    }
}