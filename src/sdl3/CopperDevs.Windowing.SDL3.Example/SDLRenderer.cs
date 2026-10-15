using System.Drawing;
using System.Numerics;
using CopperDevs.Celesium;
using CopperDevs.Windowing.SDL3.Utility;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace CopperDevs.Windowing.SDL3.Example;

// ReSharper disable once InconsistentNaming
public unsafe class SDLRenderer : SafeDisposable
{
    private SDL_Renderer* native;

    public SDL_Renderer* GetNativeRenderer() => native;

    // ReSharper disable once ConvertToPrimaryConstructor
    public SDLRenderer(SDLBackend backend)
    {
        native = SDL_CreateRenderer(backend.GetBackingWindow().GetNative(), string.Empty);
    }

    public Vector2 Scale
    {
        get
        {
            float x;
            float y;

            SDL_GetRenderScale(native, &x, &y);

            return new Vector2(x, y);
        }
        set => SDL_SetRenderScale(native, value.X, value.Y);
    }

    public Vector2 RendererToWindowCoordinates(Vector2 position)
    {
        float x;
        float y;

        SDL_RenderCoordinatesToWindow(native, position.X, position.Y, &x, &y);

        return new Vector2(x, y);
    }

    public Vector2 WindowToRendererCoordinates(Vector2 position)
    {
        float x;
        float y;

        SDL_RenderCoordinatesFromWindow(native, position.X, position.Y, &x, &y);

        return new Vector2(x, y);
    }

    public void Present() => SDL_RenderPresent(native);

    public void Clear() => SDL_RenderClear(native);

    public void Clear(SDL_FColor color)
    {
        SetDrawColor(color);
        Clear();
    }

    public void Clear(float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        Clear();
    }


    public void SetDrawColor(SDL_FColor color)
    {
        SDL_SetRenderDrawColorFloat(native, color.r, color.g, color.b, color.a);
    }

    public void SetDrawColor(float r, float g, float b, float a)
    {
        SDL_SetRenderDrawColorFloat(native, r, g, b, a);
    }

    public void DrawLine(Vector2 posOne, Vector2 posTwo)
    {
        SDL_RenderLine(native, posOne.X, posOne.Y, posTwo.X, posTwo.Y);
    }

    public void DrawLine(Vector2 posOne, Vector2 posTwo, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawLine(posOne, posTwo);
    }

    public void DrawLine(Vector2 posOne, Vector2 posTwo, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawLine(posOne, posTwo);
    }

    public void DrawDebugText(string text, Vector2 position)
    {
        SDL_RenderDebugText(native, position.X, position.Y, text);
    }

    public void DrawDebugText(string text, Vector2 position, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawDebugText(text, position);
    }

    public void DrawDebugText(string text, Vector2 position, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawDebugText(text, position);
    }

    public void DrawLines(List<Vector2> points)
    {
        points.UseAsPointer(vector => new SDL_FPoint
        {
            x = vector.X,
            y = vector.Y
        }, ptr => SDL_RenderLines(native, ptr, points.Count));
    }

    public void DrawLines(List<Vector2> points, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawLines(points);
    }

    public void DrawLines(List<Vector2> points, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawLines(points);
    }

    public void DrawPoint(Vector2 point)
    {
        SDL_RenderPoint(native, point.X, point.Y);
    }

    public void DrawPoint(Vector2 point, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawPoint(point);
    }

    public void DrawPoint(Vector2 point, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawPoint(point);
    }

    public void DrawPoints(List<Vector2> points)
    {
        points.UseAsPointer(vector => new SDL_FPoint
        {
            x = vector.X,
            y = vector.Y
        }, ptr => SDL_RenderPoints(native, ptr, points.Count));
    }

    public void DrawPoints(List<Vector2> points, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawPoints(points);
    }

    public void DrawPoints(List<Vector2> points, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawPoints(points);
    }

    public void DrawRect(Vector2 position, Vector2 size)
    {
        SDL_FRect rect = new()
        {
            x = position.X,
            y = position.Y,
            w = size.X,
            h = size.Y
        };

        SDL_RenderRect(native, &rect);
    }

    public void DrawRect(Vector2 position, Vector2 size, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawRect(position, size);
    }

    public void DrawRect(Vector2 position, Vector2 size, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawRect(position, size);
    }

    public void DrawRects(List<SDL_FRect> rects)
    {
        rects.UseAsPointer(ptr => SDL_RenderRects(native, ptr, rects.Count));
    }

    public void DrawRects(List<SDL_FRect> rects, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawRects(rects);
    }

    public void DrawRects(List<SDL_FRect> rects, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawRects(rects);
    }

    public void DrawFillRect(Vector2 position, Vector2 size)
    {
        SDL_FRect rect = new()
        {
            x = position.X,
            y = position.Y,
            w = size.X,
            h = size.Y
        };

        SDL_RenderFillRect(native, &rect);
    }

    public void DrawFillRect(Vector2 position, Vector2 size, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawFillRect(position, size);
    }

    public void DrawFillRect(Vector2 position, Vector2 size, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawFillRect(position, size);
    }

    public void DrawFillRects(List<SDL_FRect> rects)
    {
        rects.UseAsPointer(ptr => SDL_RenderFillRects(native, ptr, rects.Count));
    }

    public void DrawFillRects(List<SDL_FRect> rects, SDL_FColor color)
    {
        SetDrawColor(color);
        DrawFillRects(rects);
    }

    public void DrawFillRects(List<SDL_FRect> rects, float r, float g, float b, float a = 1)
    {
        SetDrawColor(r, g, b, a);
        DrawFillRects(rects);
    }

    public void SetVSync(int count)
    {
        if (!SDL_SetRenderVSync(native, count))
            Log.Error($"Failed to set Vsync: {SDL_GetError()}");
    }

    /// <summary>
    /// Disposes of the renderer and shuts down its SDL counterpart
    /// </summary>
    /// <remarks>Call <see cref="SDLRenderer.Dispose"/> instead to make sure resources aren't attempted to be disposed of multiple times</remarks>
    public override void DisposeResources()
    {
        SDL_DestroyRenderer(native);
    }
}