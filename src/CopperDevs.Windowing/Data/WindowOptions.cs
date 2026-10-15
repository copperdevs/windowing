using CopperDevs.Celesium;

namespace CopperDevs.Windowing.Data;

public record WindowOptions
{
    /// <summary>
    /// Starting size of the window on creation
    /// </summary>
    public Vector2Int Size = new(1150, 680);

    /// <summary>
    /// Starting title of the window on creation
    /// </summary>
    public string Title = "Untitled Window";

    /// <summary>
    /// Delay in milliseconds between each frame render
    /// </summary>
    /// <remarks>
    /// Set to 0 (or any value below) to disable 
    /// </remarks>
    public int FrameRenderDelay = 0;

    /// <summary>
    /// Default settings
    /// </summary>
    public static WindowOptions Default => new();
}