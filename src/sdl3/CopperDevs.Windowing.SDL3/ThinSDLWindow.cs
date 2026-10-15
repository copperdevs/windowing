using CopperDevs.Celesium;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3.Data;

namespace CopperDevs.Windowing.SDL3;

public sealed unsafe class ThinSDLWindow : SafeDisposable
{
    private WindowOptions usedOptions;

    private EventHandler events = new();
    public Action<SDL_Event> OnEvent = null!;

    private readonly SDL_InitFlags initFlags;
    private SDL_Window* native;

    public SDL_Window* GetNative() => native;

    public ThinSDLWindow(WindowOptions options)
    {
        SDL_SetHint(SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4, "1"u8);
        SDL_SetHint(SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4, "1");

        usedOptions = options;
        initFlags = SDL_InitFlags.SDL_INIT_VIDEO;
        var initWindowFlags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY;

        if (options.GetType() == typeof(SDL3WindowOptions))
        {
            initFlags = ((SDL3WindowOptions)options).InitFlags;
            initWindowFlags = ((SDL3WindowOptions)options).WindowFlags;
        }

        if (!SDL_InitSubSystem(initFlags))
        {
            throw new InvalidOperationException($"Failed to initialise SDL. Error: {SDL_GetError()}");
        }

        events.HandleEvent += e => OnEvent?.Invoke(e);

        native = SDL_CreateWindow(options.Title, options.Size.X, options.Size.Y, initWindowFlags);
    }

    public void PollEvents()
    {
        events.Poll();
    }

    public override void DisposeResources()
    {
        SDL_QuitSubSystem(initFlags);
        SDL_Quit();
    }

    private class EventHandler
    {
        private const int EventsPerPeep = 64;
        private readonly SDL_Event[] events = new SDL_Event[EventsPerPeep];

        public Action<SDL_Event> HandleEvent = null!;

        public void Poll()
        {
            SDL_PumpEvents();

            int eventsRead;

            do
            {
                eventsRead = SDL_PeepEvents(events, SDL_EventAction.SDL_GETEVENT, SDL_EventType.SDL_EVENT_FIRST, SDL_EventType.SDL_EVENT_LAST);
                for (var i = 0; i < eventsRead; i++)
                    HandleEvent?.Invoke(events[i]);
            } while (eventsRead == EventsPerPeep);
        }
    }
}