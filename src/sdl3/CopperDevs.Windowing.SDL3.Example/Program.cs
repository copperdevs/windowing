using System.Drawing;
using System.Numerics;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3.Data;
using CopperDevs.Windowing.SDL3.Utility;
using CopperDevs.Windowing.Utility;

namespace CopperDevs.Windowing.SDL3.Example;

public static class Program
{
    private static SDLBackend backend = null!;
    private static Window window = null!;
    private static SDLRenderer renderer = null!;

    private static readonly List<Vector2> Points = [];

    // represents a line, with X and Y being the index of the point in points (Points[X] -> Points[Y])
    private static readonly List<Vector2Int> PointIndexes = [];

    private static readonly Vector2 ScaleRange = new(0.5f, 10f);
    private const float ArrowChanger = 0.5f;
    private static float scale = 1;

    private static FPSCounter fps = null!;

    public static void Main()
    {
        var options = SDL3WindowOptions.Default with
        {
            Title = "CopperDevs SDL3 Windowing Example",
            FrameRenderDelay = 0
        };

        backend = new SDLBackend();
        window = new Window(backend, options);
        window.Create();

        window.OnLoad += OnLoad;
        window.OnUpdate += OnUpdate;
        window.OnRender += OnRender;

        fps = window.CreateFPSCounter();

        window.Run();

        window.Dispose();
    }

    private static void OnLoad()
    {
        renderer = new SDLRenderer(backend);
    }

    private static void OnUpdate()
    {
        if (window.IsMouseButtonPressed(MouseButton.Left))
        {
            Points.AddRange([window.GetMousePosition(), window.GetMousePosition()]);
            PointIndexes.Add(new Vector2Int(Points.Count - 2, Points.Count - 1));
        }

        if (window.IsMouseButtonReleased(MouseButton.Left) || window.IsMouseButtonDown(MouseButton.Left))
            Points[PointIndexes[^1].Y] = window.GetMousePosition();

        if (window.IsMouseButtonReleased(MouseButton.Left) && Points[PointIndexes[^1].X] == Points[PointIndexes[^1].Y])
            PointIndexes.Remove(PointIndexes[^1]);

        if (window.IsKeyPressed(InputKey.Left))
            scale -= ArrowChanger;

        if (window.IsKeyPressed(InputKey.Right))
            scale += ArrowChanger;

        scale = Math.Clamp(scale, ScaleRange.X, ScaleRange.Y);

        renderer.Scale = Vector2.One * scale;
    }

    private static void OnRender()
    {
        renderer.Clear(SDL_FColor.DarkGray);

        foreach (var pointIndex in PointIndexes)
            renderer.DrawLine(renderer.WindowToRendererCoordinates(Points[pointIndex.X]),
                renderer.WindowToRendererCoordinates(Points[pointIndex.Y]), SDL_FColor.White);

        foreach (var point in Points)
            renderer.DrawPoint(renderer.WindowToRendererCoordinates(point), SDL_FColor.Red);

        RenderDebugText();

        renderer.Present();
    }

    private static void RenderDebugText()
    {
        var oldScale = renderer.Scale;
        renderer.Scale = Vector2.One * 1.5f; // bigger text moment

        renderer.DrawDebugText($"FPS: {fps.AverageFrameRate}", new Vector2(16, 16), SDL_FColor.Black);
        renderer.DrawDebugText($"Mouse Pos: {window.GetMousePosition()}", new Vector2(16, 26), SDL_FColor.Black);
        renderer.DrawDebugText($"Scale: {scale}", new Vector2(16, 36), SDL_FColor.Black);
        renderer.DrawDebugText($"Line Count: {PointIndexes.Count}", new Vector2(16, 46), SDL_FColor.Black);
        renderer.DrawDebugText($"Points Count: {Points.Count}", new Vector2(16, 56), SDL_FColor.Black);

        renderer.Scale = Vector2.One * 1.35f; // smaller text for these items

        for (var i = 0; i < PointIndexes.Count; i++)
            renderer.DrawDebugText(
                $"Line {i + 1}: {Points[PointIndexes[i].X].Round(2)} -> {Points[PointIndexes[i].Y].Round(2)}",
                new Vector2(20, 74 + i * 10), SDL_FColor.Black);

        renderer.Scale = oldScale;
    }
}