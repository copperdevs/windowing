using System.Numerics;

namespace CopperDevs.Windowing.SDL3.Example;

/// <summary>
/// Utility Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Rounds both values to the nearest amount of digits
    /// </summary>
    /// <returns>Both numbers rounded to the numbers nearest to them that contains a number of fractional digits equal to digits.</returns>
    public static Vector2 Round(this Vector2 vector, int digits)
    {
        return new Vector2(MathF.Round(vector.X, digits), MathF.Round(vector.Y, digits));
    }
}
