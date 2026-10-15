using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.SDL3.Data;

// ReSharper disable once InconsistentNaming
public record SDL3WindowOptions : WindowOptions
{
    /// <summary>
    /// Flags for what to initialize the window with
    /// </summary>
    public SDL_InitFlags InitFlags { get; set; } = SDL_InitFlags.SDL_INIT_VIDEO;

    /// <summary>
    /// Flags for the window to utilize
    /// </summary>
    public SDL_WindowFlags WindowFlags { get; set; } = SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY;

    /// <summary>
    /// Default settings
    /// </summary>
    public new static SDL3WindowOptions Default => new();
}