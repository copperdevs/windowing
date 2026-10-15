using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3;
using CopperDevs.Windowing.SDL3.Data;
using CopperDevs.Windowing.Utility;

namespace CopperDevs.Windowing.Testing;

// testing project moment
public static class Program
{
    private static SDL3Window window = null!;
    private static SDLRenderer renderer = null!;

    private static FPSCounter fps = null!;

    public static void Main()
    {
        var options = SDL3WindowOptions.Default with
        {
            Title = "CopperDevs Windowing Example"
        };

        window = Window.Create<SDL3Window>(options);
        renderer = window.GetRenderer()!;

        window.OnLoad += OnLoad;
        window.OnUpdate += OnUpdate;
        window.OnRender += OnRender;
        window.OnClose += OnClose;

        fps = window.CreateFPSCounter();

        window.Run();

        window.Dispose();
    }


    private static void OnLoad()
    {
    }

    private static void OnUpdate()
    {
        Log.Info($"{fps.AverageFrameRate}");
    }

    private static void OnRender()
    {
    }

    private static void OnClose()
    {
    }
}