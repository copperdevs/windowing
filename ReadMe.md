# windowing
> simple, thin, windowing abstraction

## installation
### packages
- Base Package: https://www.nuget.org/packages/CopperDevs.Windowing/
- SDL3 Support: https://www.nuget.org/packages/CopperDevs.Windowing.SDL3/

## simple eample
*A full, simple, example project based off of SDL3 can be found [here](./src/sdl3/CopperDevs.Windowing.SDL3.Example/Program.cs)*

Example of the creation of an empty window
```csharp
using var window = new Window(WindowOptions.Default, options);
window.Run();
```
