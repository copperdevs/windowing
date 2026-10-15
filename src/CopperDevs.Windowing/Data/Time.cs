namespace CopperDevs.Windowing.Data;

/// <summary>
/// Wrapper above <see cref="CopperDevs.Windowing.Window"/> for getting time values easier
/// </summary>
public static class Time
{
    private static Window? window;

    internal static void Setup(Window connectedWindow) => window ??= connectedWindow;

    /// <summary>
    /// Get the total time the window has been open
    /// </summary>
    public static double TotalTime => window!.TotalTime;
    
    /// <summary>
    /// Get the current frames delta time
    /// </summary>
    public static double DeltaTime => window!.DeltaTime;

    /// <summary>
    /// Get the total time the window has been open
    /// </summary>
    /// <remarks>Same as <see cref="TotalTime"/>, just as a float</remarks>
  
    public static float TotalTimeAsFloat => (float)TotalTime;
    
    /// <summary>
    /// Get the current frames delta time
    /// </summary>
    /// <remarks>Same as <see cref="TotalTime"/>, just as a float</remarks>
    public static float DeltaTimeAsFloat => (float)DeltaTime;
}