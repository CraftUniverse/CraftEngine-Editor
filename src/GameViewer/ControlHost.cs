using System;
using Avalonia.Controls;
using Avalonia.Platform;
using SDL3;

namespace dev.craftengine.editor.GameViewer;

public class ControlHost : NativeControlHost
{
    private nint _window;
    private nint _renderer;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if (!SDL.Init(SDL.InitFlags.Video | SDL.InitFlags.Camera))
        {
            throw new Exception($"Failed at init SDL3. {SDL.GetError()}");
        }

        var props = SDL.CreateProperties();

        SDL.SetPointerProperty(props, SDL.Props.WindowCreateParentPointer, parent.Handle);

        _window = SDL.CreateWindowWithProperties(props);

        if (_window == nint.Zero)
        {
            throw new Exception($"There was an issue creating the window. {SDL.GetError()}");
        }

        _renderer = SDL.CreateRenderer(_window, null);

        if (_renderer == nint.Zero)
        {
            throw new Exception($"There was an issue creating the renderer. {SDL.GetError()}");
        }

        SDL.SetRenderDrawColor(_renderer, 255, 0, 0, 255);
        SDL.RenderClear(_renderer);
        SDL.RenderPresent(_renderer);

        return new PlatformHandle(_window, "Sdl2Hwnd");
    }
}