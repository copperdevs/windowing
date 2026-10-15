using System.Numerics;
using CopperDevs.Celesium;
using CopperDevs.Windowing.Backend;
using CopperDevs.Windowing.Data;
using CopperDevs.Windowing.SDL3.Data;

namespace CopperDevs.Windowing.SDL3;

// ReSharper disable once InconsistentNaming
internal class SDLInput : IInput
{
    private const bool InputLogs = false;

    private SDL3Window connectedWindow;

    private readonly SdlKeyMap keyMap = new();
    private readonly SdlMouseMap mouseMap = new();

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

    private readonly List<EventType> targetEvents =
    [
        // EventType.WindowMouseEnter,
        // EventType.WindowMouseLeave,
        EventType.MouseMotion,
        EventType.MouseButtonDown,
        EventType.MouseButtonUp,
        EventType.MouseWheel,
        // EventType.MouseAdded,
        // EventType.MouseRemoved,
        EventType.KeyDown,
        EventType.KeyUp,
        // EventType.KeymapChanged,
        // EventType.KeyboardAdded,
        // EventType.KeyboardRemoved,
    ];

    public SDLInput(SDL3Window connectedWindow)
    {
        this.connectedWindow = connectedWindow;

        connectedWindow.GetManagedSDLWindow().HandleEvent += EventHandler;
    }

    private void EventHandler(EventType eventType, SDL_Event eventData)
    {
        if (!targetEvents.Contains(eventType))
            return;

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (eventType)
        {
            case EventType.MouseMotion:
                mousePosition = mousePosition with
                {
                    X = eventData.motion.x,
                    Y = eventData.motion.y,
                };
                if (InputLogs)
                    Log.Debug($"Mouse Motion: <{eventData.motion.x},{eventData.motion.y}> | Mouse Motion Relative: <{eventData.motion.xrel},{eventData.motion.yrel}>");
                break;
            case EventType.MouseButtonDown:
                buttonCurrentlyPressed[eventData.button.Button] = true;

                if (InputLogs)
                    Log.Debug($"Mouse Button Down: {eventData.button.Button}");
                break;
            case EventType.MouseButtonUp:
                buttonCurrentlyPressed[eventData.button.Button] = false;

                if (InputLogs)
                    Log.Debug($"Mouse Button Up: {eventData.button.Button}");
                break;
            case EventType.MouseWheel:
                // lol invert moment
                mouseScroll = mouseScroll with
                {
                    X = eventData.wheel.x * -1,
                    Y = eventData.wheel.y * -1,
                };

                if (InputLogs)
                    Log.Debug($"Mouse Wheel: <{eventData.wheel.x},{eventData.wheel.y}> | Direction: <{eventData.wheel.direction}>");
                break;
            case EventType.KeyDown:
                keyCurrentlyPressed[eventData.key.key] = true;

                if (InputLogs)
                    Log.Debug($"Key Down: {eventData.key.key} | Modifier: {eventData.key.mod}");
                break;
            case EventType.KeyUp:
                keyCurrentlyPressed[eventData.key.key] = false;

                if (InputLogs)
                    Log.Debug($"Key Up: {eventData.key.key} | Modifier: {eventData.key.mod}");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
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
                SDLAPI.WarpMouseInWindow(connectedWindow.GetNativeWindow(), connectedWindow.Size / 2);
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
                SDLAPI.ShowCursor();
                SDLAPI.SetMouseRelativeMode(connectedWindow.GetNativeWindow(), false);
                break;
            case CursorMode.Hidden:
                SDLAPI.HideCursor();
                SDLAPI.SetMouseRelativeMode(connectedWindow.GetNativeWindow(), false);
                break;
            case CursorMode.Locked:
                SDLAPI.HideCursor();
                SDLAPI.SetMouseRelativeMode(connectedWindow.GetNativeWindow(), true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cursorMode), cursorMode, null);
        }
    }
}