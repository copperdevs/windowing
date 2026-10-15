using System.Numerics;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Backend;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3.Data;

namespace CopperDevs.Windowing.SDL3;

// ReSharper disable once InconsistentNaming
internal class SDLInput : IInput
{
    private readonly SDLBackend backend;
    private readonly ThinSDLWindow window;

    // ReSharper disable CollectionNeverUpdated.Local
    private readonly SdlKeyMap keyMap = new();
    private readonly SdlMouseMap mouseMap = new();
    // ReSharper restore CollectionNeverUpdated.Local

    private readonly Dictionary<SDL_Keycode, bool> keyCurrentlyPressed = new();
    private readonly Dictionary<SDLButton, bool> buttonCurrentlyPressed = new();

    private Dictionary<SDL_Keycode, bool> previousFrameKeyCurrentlyPressed = new();
    private Dictionary<SDLButton, bool> previousFrameButtonCurrentlyPressed = new();

    private Vector2 mousePosition;
    private Vector2 previousMousePosition;

    private Vector2 mouseScroll;

    private CursorMode cursorMode = CursorMode.Normal;

    private bool GetKeyPressState(InputKey key, bool previousFrame)
    {
        return previousFrame ? previousFrameKeyCurrentlyPressed.GetValueOrDefault(keyMap[key], false) : keyCurrentlyPressed.GetValueOrDefault(keyMap[key], false);
    }

    private bool GetButtonPressState(MouseButton button, bool previousFrame)
    {
        return previousFrame ? previousFrameButtonCurrentlyPressed.GetValueOrDefault(mouseMap[button], false) : buttonCurrentlyPressed.GetValueOrDefault(mouseMap[button], false);
    }

    private readonly List<SDL_EventType> targetEvents =
    [
        SDL_EventType.SDL_EVENT_MOUSE_MOTION,
        SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN,
        SDL_EventType.SDL_EVENT_MOUSE_BUTTON_UP,
        SDL_EventType.SDL_EVENT_MOUSE_WHEEL,
        SDL_EventType.SDL_EVENT_KEY_DOWN,
        SDL_EventType.SDL_EVENT_KEY_UP,
    ];

    public SDLInput(SDLBackend target, ThinSDLWindow thin)
    {
        backend = target;
        window = thin;
        thin.OnEvent += EventHandler;
    }

    private void EventHandler(SDL_Event eventData)
    {
        if (!targetEvents.Contains(eventData.Type))
            return;

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (eventData.Type)
        {
            case SDL_EventType.SDL_EVENT_MOUSE_MOTION:
                mousePosition = mousePosition with
                {
                    X = eventData.motion.x,
                    Y = eventData.motion.y,
                };
                break;
            case SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN:
                buttonCurrentlyPressed[eventData.button.Button] = true;
                break;
            case SDL_EventType.SDL_EVENT_MOUSE_BUTTON_UP:
                buttonCurrentlyPressed[eventData.button.Button] = false;
                break;
            case SDL_EventType.SDL_EVENT_MOUSE_WHEEL:
                // lol invert moment
                mouseScroll = mouseScroll with
                {
                    X = eventData.wheel.x * -1,
                    Y = eventData.wheel.y * -1,
                };
                break;
            case SDL_EventType.SDL_EVENT_KEY_DOWN:
                keyCurrentlyPressed[eventData.key.key] = true;
                break;
            case SDL_EventType.SDL_EVENT_KEY_UP:
                keyCurrentlyPressed[eventData.key.key] = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eventData.Type), eventData.Type, null);
        }
    }

    public void UpdateInput()
    {
        // TODO: fix this shit bro
        previousFrameKeyCurrentlyPressed = new Dictionary<SDL_Keycode, bool>(keyCurrentlyPressed);
        previousFrameButtonCurrentlyPressed = new Dictionary<SDLButton, bool>(buttonCurrentlyPressed);

        previousMousePosition = previousMousePosition with
        {
            X = mousePosition.X,
            Y = mousePosition.Y,
        };

        mouseScroll = Vector2.Zero;


        if (cursorMode == CursorMode.Locked)
        {
            unsafe
            {
                var size = backend.GetWindowSize();
                SDL_WarpMouseInWindow(window.GetNative(), size.X, size.Y);
            }
        }
    }

    public bool SupportsInputKey(InputKey inputKey)
    {
        return keyMap[inputKey] != SDL_Keycode.SDLK_UNKNOWN;
    }

    public bool IsKeyPressed(InputKey key)
    {
        return !GetKeyPressState(key, true) && GetKeyPressState(key, false);
    }

    public bool IsKeyDown(InputKey key)
    {
        return GetKeyPressState(key, false);
    }

    public bool IsKeyReleased(InputKey key)
    {
        return GetKeyPressState(key, true) && !GetKeyPressState(key, false);
    }

    public bool IsKeyUp(InputKey key)
    {
        return !GetKeyPressState(key, false);
    }

    public bool IsMouseButtonPressed(MouseButton button)
    {
        return !GetButtonPressState(button, true) && GetButtonPressState(button, false);
    }

    public bool IsMouseButtonDown(MouseButton button)
    {
        return GetButtonPressState(button, false);
    }

    public bool IsMouseButtonReleased(MouseButton button)
    {
        return GetButtonPressState(button, true) && !GetButtonPressState(button, false);
    }

    public bool IsMouseButtonUp(MouseButton button)
    {
        return !GetButtonPressState(button, false);
    }

    public Vector2 GetMousePosition()
    {
        return mousePosition;
    }

    public Vector2 GetMouseDelta()
    {
        return mousePosition - previousMousePosition;
    }

    public Vector2 GetMouseScroll()
    {
        return mouseScroll;
    }

    public unsafe void SetCursorMode(CursorMode newMode)
    {
        cursorMode = newMode;

        switch (cursorMode)
        {
            case CursorMode.Normal:
                SDL_ShowCursor();
                SDL_SetWindowRelativeMouseMode(window.GetNative(), false);
                break;
            case CursorMode.Hidden:
                SDL_HideCursor();
                SDL_SetWindowRelativeMouseMode(window.GetNative(), false);
                break;
            case CursorMode.Locked:
                SDL_HideCursor();
                SDL_SetWindowRelativeMouseMode(window.GetNative(), true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cursorMode), cursorMode, null);
        }
    }
}