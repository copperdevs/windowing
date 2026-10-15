using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Utility;

// ReSharper disable once InconsistentNaming
public sealed class FPSCounter
{
    private int fps;
    private int fpsCounter;
    private long fpsUpdateTime = TimeUtility.UtcNow();

    public FPSCounter(Window target)
    {
        target.OnPreUpdate += Update;
    }

    // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
    public int AverageFrameRate => fps;

    private void Update()
    {
        ++fpsCounter;

        var currentTime = TimeUtility.UtcNow();

        if (currentTime < fpsUpdateTime + 1000)
            return;

        fpsUpdateTime = currentTime;
        fps = fpsCounter;
        fpsCounter = 0;
    }
}

// ReSharper disable once InconsistentNaming
public static class FPSCounterExtensions
{
    // ReSharper disable once UnusedType.Global
    extension(Window window)
    {
        // ReSharper disable once InconsistentNaming
        public FPSCounter CreateFPSCounter()
        {
            return new FPSCounter(window);
        }
    }
}