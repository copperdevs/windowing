using CopperDevs.Celesium;

namespace CopperDevs.Windowing;

public sealed partial class Window
{
    private bool backendSupportsTotalTime;
    private bool backendSupportsDeltaTime;

    /// <summary>
    /// Get the total time the window has been open
    /// </summary>
    public double TotalTime => GetTotalTime();

    /// <summary>
    /// Get the current frames delta time
    /// </summary>
    public double DeltaTime => GetDeltaTime();

    private long GetTotalTime()
    {
        return backendSupportsTotalTime ? backend.GetTotalTime() : (TimeUtility.UtcNow() - startTime);
    }

    private long GetDeltaTime()
    {
        return backendSupportsDeltaTime ? backend.GetDeltaTime() : currentFrameDeltaTime;
    }

    private void CheckTimeSupport()
    {
        try
        {
            backend.GetTotalTime();
            backendSupportsTotalTime = true;
        }
        catch (Exception)
        {
            backendSupportsTotalTime = false;
        }

        try
        {
            backend.GetDeltaTime();
            backendSupportsDeltaTime = true;
        }
        catch (Exception)
        {
            backendSupportsDeltaTime = false;
        }
    }

    private void TimeTick()
    {
        var currentTime = TimeUtility.UtcNow();
        currentFrameDeltaTime = (currentTime - previousFrameStartTime) / 1000;

        previousFrameStartTime = currentTime;
    }
}