using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.Utility;

namespace CopperDevs.Windowing.Win32.Example;

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

        using var window = new Window(new Win32Backend(), options);
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
        
        Console.CursorTop = 0;
        Console.CursorLeft = 0;
        Console.WriteLine($"{fps.AverageFrameRate}".PadRight(10));
    }

    private static void OnRender()
    {
    }

    private static void OnClose()
    {
    }
}