using System.Runtime.InteropServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace CopperDevs.Windowing.SDL3.Utility;

// ReSharper disable once InconsistentNaming
public static unsafe class SDLUtil
{
    public delegate void UnsafeAction<T>(T* ptr) where T : unmanaged;

    public static void UseArrayAsPointer<TTarget>(
        List<TTarget> items,
        UnsafeAction<TTarget> usage)
        where TTarget : unmanaged
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(usage);

        fixed (TTarget* ptr = CollectionsMarshal .AsSpan(items)) 
            usage(ptr);
    }

    public static void UseArrayAsPointer<TType, TTarget>(List<TType> items, Func<TType, TTarget> conversion, UnsafeAction<TTarget> usage) where TTarget : unmanaged
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(conversion);
        ArgumentNullException.ThrowIfNull(usage);
        
        var nativePoint = new TTarget[items.Count];

        for (var i = 0; i < items.Count; i++)
            nativePoint[i] = conversion.Invoke(items[i]);

        fixed (TTarget* pointsPtr = nativePoint)
            usage?.Invoke(pointsPtr);
    }

    public static TTarget[] ToArray<TTarget>(SDLPointerArray<TTarget>? pointerArray) where TTarget : unmanaged
    {
        ArgumentNullException.ThrowIfNull(pointerArray);
        
        var array = new TTarget[pointerArray.Count];

        for (var i = 0; i < pointerArray.Count; i++)
            array[i] = pointerArray[i];

        pointerArray.Dispose();

        return array;
    }

    public static TTarget[] ToArray<TTarget>(SDLArray<TTarget>? pointerArray) where TTarget : unmanaged
    {
        ArgumentNullException.ThrowIfNull(pointerArray);

        var array = new TTarget[pointerArray.Count];

        for (var i = 0; i < pointerArray.Count; i++)
            array[i] = pointerArray[i];

        pointerArray.Dispose();

        return array;
    }

    public static TTarget[] ToArray<TTarget>(SDLOpaquePointerArray<TTarget>? pointerArray) where TTarget : unmanaged
    {
        ArgumentNullException.ThrowIfNull(pointerArray);

        var array = new TTarget[pointerArray.Count];

        for (var i = 0; i < pointerArray.Count; i++)
            array[i] = *pointerArray[i];

        pointerArray.Dispose();

        return array;
    }
}