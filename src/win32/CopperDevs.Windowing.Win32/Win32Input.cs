using System.Numerics;
using CopperDevs.Windowing.Backend;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Win32;

public class Win32Input : IInput
{
    public void UpdateInput()
    {
    }

    public bool SupportsInputKey(InputKey inputKey)
    {
        throw new NotImplementedException();
    }

    public bool IsKeyPressed(InputKey key)
    {
        throw new NotImplementedException();
    }

    public bool IsKeyDown(InputKey key)
    {
        throw new NotImplementedException();
    }

    public bool IsKeyReleased(InputKey key)
    {
        throw new NotImplementedException();
    }

    public bool IsKeyUp(InputKey key)
    {
        throw new NotImplementedException();
    }

    public bool IsMouseButtonPressed(MouseButton button)
    {
        throw new NotImplementedException();
    }

    public bool IsMouseButtonDown(MouseButton button)
    {
        throw new NotImplementedException();
    }

    public bool IsMouseButtonReleased(MouseButton button)
    {
        throw new NotImplementedException();
    }

    public bool IsMouseButtonUp(MouseButton button)
    {
        throw new NotImplementedException();
    }

    public Vector2 GetMousePosition()
    {
        throw new NotImplementedException();
    }

    public Vector2 GetMouseDelta()
    {
        throw new NotImplementedException();
    }

    public Vector2 GetMouseScroll()
    {
        throw new NotImplementedException();
    }

    public void SetCursorMode(CursorMode cursorMode)
    {
        throw new NotImplementedException();
    }
}