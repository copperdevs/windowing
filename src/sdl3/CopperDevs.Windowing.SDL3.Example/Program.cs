using System.Numerics;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3.Data;
using CopperDevs.Windowing.Utility;

namespace CopperDevs.Windowing.SDL3.Example;

public static class Program
{
    private static SDL3Window window = null!;
    private static SDLRenderer renderer = null!;

    private static readonly List<Vector2> Points = [];

    // represents a line, with X and Y being the index of the point in points (Points[X] -> Points[Y])
    private static readonly List<Vector2Int> PointIndexes = [];

    private static readonly Vector2 ScaleRange = new(0.5f, 10f);
    private const float ArrowChanger = 0.5f;
    private static float scale = 1;

    private static FPSCounter fps;

    public static void Main()
    {
        var options = SDL3WindowOptions.Default with
        {
            Title = "CopperDevs SDL3 Windowing Example",
            RendererOptions = new RendererOptions
            {
                TargetRenderer = SDLRenderers.Renderer,
            },
            FrameRenderDelay = 0
        };

        window = Window.Create<SDL3Window>(options);
        renderer = window.GetRenderer()!;

        window.OnUpdate += OnUpdate;
        window.OnRender += OnRender;

        fps = window.CreateFPSCounter();

        window.Run();

        window.Dispose();
    }

    private static void OnUpdate()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            Points.AddRange([Input.GetMousePosition(), Input.GetMousePosition()]);
            PointIndexes.Add(new Vector2Int(Points.Count - 2, Points.Count - 1));
        }

        if (Input.IsMouseButtonReleased(MouseButton.Left) || Input.IsMouseButtonDown(MouseButton.Left))
            Points[PointIndexes[^1].Y] = Input.GetMousePosition();

        if (Input.IsMouseButtonReleased(MouseButton.Left) && Points[PointIndexes[^1].X] == Points[PointIndexes[^1].Y])
            PointIndexes.Remove(PointIndexes[^1]);

        if (Input.IsKeyPressed(InputKey.Left))
            scale -= ArrowChanger;

        if (Input.IsKeyPressed(InputKey.Right))
            scale += ArrowChanger;

        scale = Math.Clamp(scale, ScaleRange.X, ScaleRange.Y);

        renderer.Scale = Vector2.One * scale;
    }


    private static void OnRender()
    {
        renderer.Clear(Color.DarkGray);

        foreach (var pointIndex in PointIndexes)
            renderer.DrawLine(renderer.WindowToRendererCoordinates(Points[pointIndex.X]),
                renderer.WindowToRendererCoordinates(Points[pointIndex.Y]), Color.White);

        foreach (var point in Points)
            renderer.DrawPoint(renderer.WindowToRendererCoordinates(point), Color.Red);

        if (Input.IsKeyDown(InputKey.Space))
            renderer.Screenshot(); // we do this here so we only take a screenshot of the lines and not the debug text

        RenderDebugText();

        renderer.Present();
    }

    private static void RenderDebugText()
    {
        var oldScale = renderer.Scale;
        renderer.Scale = Vector2.One * 1.5f; // bigger text moment

        renderer.DrawDebugText($"FPS: {fps.AverageFrameRate}", new Vector2(16, 16), Color.Black);
        renderer.DrawDebugText($"Mouse Pos: {Input.GetMousePosition()}", new Vector2(16, 26), Color.Black);
        renderer.DrawDebugText($"Scale: {scale}", new Vector2(16, 36), Color.Black);
        renderer.DrawDebugText($"Line Count: {PointIndexes.Count}", new Vector2(16, 46), Color.Black);
        renderer.DrawDebugText($"Points Count: {Points.Count}", new Vector2(16, 56), Color.Black);


        renderer.Scale = Vector2.One * 1.35f; // smaller text for these items

        for (var i = 0; i < PointIndexes.Count; i++)
            renderer.DrawDebugText(
                $"Line {i + 1}: {Points[PointIndexes[i].X].Round(2)} -> {Points[PointIndexes[i].Y].Round(2)}",
                new Vector2(20, 74 + i * 10), Color.Black);

        renderer.Scale = oldScale;
    }
}