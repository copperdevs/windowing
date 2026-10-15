using System.Runtime.Versioning;
using CopperDevs.Windowing.Data;

namespace CopperDevs.Windowing.Win32;

[SupportedOSPlatform("windows5.0")]
public record Win32WindowOptions : WindowOptions
{
    public string? WindowClassName;

    /// <summary>
    /// Default settings
    /// </summary>
    public new static Win32WindowOptions Default => new();
}