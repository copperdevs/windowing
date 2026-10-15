using System.Numerics;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing;

public sealed partial class Window
{
    /// <summary>
    /// Checks if a key was pressed this frame
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was pressed this frame</returns>
    public bool IsKeyPressed(InputKey key)
    {
        return input!.SupportsInputKey(key) && input!.IsKeyPressed(key);
    }

    /// <summary>
    /// Checks if a key is down
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was down this frame</returns>
    public bool IsKeyDown(InputKey key)
    {
        return input!.SupportsInputKey(key) && input!.IsKeyDown(key);
    }

    /// <summary>
    /// Checks if a key was released this frame
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was released this frame</returns>
    public bool IsKeyReleased(InputKey key)
    {
        return input!.SupportsInputKey(key) && input!.IsKeyReleased(key);
    }

    /// <summary>
    /// Checks if a key is up
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was up this frame</returns>
    public bool IsKeyUp(InputKey key)
    {
        return input!.SupportsInputKey(key) && input!.IsKeyUp(key);
    }

    /// <summary>
    /// Checks if a mouse button was pressed this frame
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was pressed this frame</returns>
    public bool IsMouseButtonPressed(MouseButton button)
    {
        return input!.IsMouseButtonPressed(button);
    }

    /// <summary>
    /// Checks if a mouse button is down
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was down this frame</returns>
    public bool IsMouseButtonDown(MouseButton button)
    {
        return input!.IsMouseButtonDown(button);
    }

    /// <summary>
    /// Checks if a mouse button was released this frame
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was released this frame</returns>
    public bool IsMouseButtonReleased(MouseButton button)
    {
        return input!.IsMouseButtonReleased(button);
    }

    /// <summary>
    /// Checks if a mouse button is up
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was up this frame</returns>
    public bool IsMouseButtonUp(MouseButton button)
    {
        return input!.IsMouseButtonUp(button);
    }

    /// <summary>
    /// Get the current position of the mouse relative to the window
    /// </summary>
    /// <returns>Position of the mouse</returns>
    public Vector2 GetMousePosition()
    {
        return input!.GetMousePosition();
    }

    /// <summary>
    /// Get the relative movement of the mouse
    /// </summary>
    /// <returns>Movement of the mouse</returns>
    public Vector2 GetMouseDelta()
    {
        return input!.GetMouseDelta();
    }

    /// <summary>
    /// Get the mouse scroll delta
    /// </summary>
    /// <returns>Scroll value of the mouse</returns>
    public Vector2 GetMouseScroll()
    {
        return input!.GetMouseScroll();
    }

    /// <summary>
    /// Set the cursor mode
    /// </summary>
    /// <param name="cursorMode">Mode to set</param>
    public void SetCursorMode(CursorMode cursorMode) => input!.SetCursorMode(cursorMode);
}