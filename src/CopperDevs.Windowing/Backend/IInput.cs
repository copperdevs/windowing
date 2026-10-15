using System.Numerics;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Backend;

/// <summary>
/// Base input class
/// </summary>
public interface IInput
{
    #region Updates

    /// <summary>
    /// Per frame update method
    /// </summary>
    protected internal void UpdateInput();

    #endregion

    #region Checks

    /// <summary>
    /// Check if an input key is supported
    /// </summary>
    /// <param name="inputKey">Key to check</param>
    /// <returns>True if the key is supported</returns>
    protected internal bool SupportsInputKey(InputKey inputKey);

    #endregion

    #region Key Inputs

    /// <summary>
    /// Checks if a key was pressed this frame
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was pressed this frame</returns>
    public bool IsKeyPressed(InputKey key);

    /// <summary>
    /// Checks if a key is down
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was down this frame</returns>
    public bool IsKeyDown(InputKey key);

    /// <summary>
    /// Checks if a key was released this frame
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was released this frame</returns>
    public bool IsKeyReleased(InputKey key);

    /// <summary>
    /// Checks if a key is up
    /// </summary>
    /// <param name="key">Key to check</param>
    /// <returns>True if the key was up this frame</returns>
    public bool IsKeyUp(InputKey key);

    #endregion

    #region Mouse Inputs

    /// <summary>
    /// Checks if a mouse button was pressed this frame
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was pressed this frame</returns>
    public bool IsMouseButtonPressed(MouseButton button);

    /// <summary>
    /// Checks if a mouse button is down
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was down this frame</returns>
    public bool IsMouseButtonDown(MouseButton button);

    /// <summary>
    /// Checks if a mouse button was released this frame
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was released this frame</returns>
    public bool IsMouseButtonReleased(MouseButton button);

    /// <summary>
    /// Checks if a mouse button is up
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    /// <returns>True if the mouse button was up this frame</returns>
    public bool IsMouseButtonUp(MouseButton button);

    #endregion

    #region Mouse Values

    /// <summary>
    /// Get the current position of the mouse relative to the window
    /// </summary>
    /// <returns>Position of the mouse</returns>
    public Vector2 GetMousePosition();

    /// <summary>
    /// Get the relative movement of the mouse
    /// </summary>
    /// <returns>Movement of the mouse</returns>
    public Vector2 GetMouseDelta();

    /// <summary>
    /// Get the mouse scroll delta
    /// </summary>
    /// <returns>Scroll value of the mouse</returns>
    public Vector2 GetMouseScroll();

    #endregion

    #region Cursor Settings

    /// <summary>
    /// Set the cursor mode
    /// </summary>
    /// <param name="cursorMode">Mode to set</param>
    public void SetCursorMode(CursorMode cursorMode);

    #endregion
}