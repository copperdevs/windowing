using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3;
using CopperDevs.Windowing.Utility;

namespace CopperDevs.Windowing.Testing;

// testing project moment
public static class Program
{
    private static FPSCounter fps = null!;

    public static void Main()
    {
        var options = WindowOptions.Default with
        {
            Title = "Windowing Example",
            FrameRenderDelay = 10
        };

        using var window = new Window(new SDLBackend(), options);
        window.Create();
        
        fps = window.CreateFPSCounter();
        
        window.OnLoad += OnLoad;
        window.OnUpdate += OnUpdate;
        window.OnRender += OnRender;
        window.OnClose += OnClose;
        
        window.Run();
    }

    private static void OnLoad()
    {
    }

    private static void OnUpdate()
    {
    }

    private static void OnRender()
    {
    }

    private static void OnClose()
    {
    }
}