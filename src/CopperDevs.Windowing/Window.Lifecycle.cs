using CopperDevs.Celesium;

namespace CopperDevs.Windowing;

public sealed partial class Window
{
    public void Create()
    {
        backend.CreateWindow(options);
        input = backend.GetRelevantInputBackend();
        CheckTimeSupport();
        created = true;
    }

    public void Run()
    {
        if (!created)
            throw new Exception("Window not created");

        OnLoad?.Invoke();

        while (shouldRun)
            RenderFrame();

        OnClose?.Invoke();
        backend.DestroyWindow();
    }

    public void Stop()
    {
        shouldRun = false;
    }

    private void RenderFrame()
    {
        OnFrameStart?.Invoke();

        TimeTick();

        input.UpdateInput();
        backend.StartWindowUpdate();

        OnUpdate?.Invoke();
        OnRender?.Invoke();

        if (options.FrameRenderDelay > 0)
            Thread.Sleep(options.FrameRenderDelay);

        backend.StopWindowUpdate();

        OnFrameEnd?.Invoke();

        if (backend.RequestsStop())
            Stop();
    }

    public override void DisposeResources()
    {
        backend.Dispose();
    }
}