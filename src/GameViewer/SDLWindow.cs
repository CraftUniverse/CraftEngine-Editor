using System;
using Avalonia.Controls;
using SDL_Sharp;

namespace dev.craftengine.editor.GameViewer;

public class SDLWindow
{
    private readonly IntPtr _renderer;
    private readonly IntPtr _window;

    public SDLWindow(IntPtr windowHandle)
    {
        if (Design.IsDesignMode) return;

        if (SDL.Init(SdlInitFlags.Video) < 0)
        {
            throw new Exception($"Failed at init SDL2. {SDL.GetError()}");
        }

        _window = SDL.CreateWindowFrom(windowHandle);

        if (_window == nint.Zero)
        {
            throw new Exception($"There was an issue creating the window. {SDL.GetError()}");
        }

        _renderer = SDL.CreateRenderer(_window, -1, RendererFlags.Accelerated);

        if (_renderer == nint.Zero)
        {
            throw new Exception($"There was an issue creating the renderer. {SDL.GetError()}");
        }

        Redraw();
    }

    private void Redraw()
    {
        if (_renderer == nint.Zero) return;

        SDL.SetRenderDrawColor(_renderer, 0, 255, 0, 255);
        SDL.RenderClear(_renderer);
        SDL.RenderPresent(_renderer);
    }

    public void Resize(int width, int height)
    {
        if (_window == nint.Zero) return;

        SDL.SetWindowSize(_window, width, height);
        Redraw();
    }
}