using System;
using SDL_Sharp;

namespace dev.craftengine.editor.GameViewer;

public class SDLWindow
{
    public SDLWindow(IntPtr windowHandle)
    {
        if (SDL.Init(SdlInitFlags.Video) < 0)
        {
            throw new Exception($"Failed at init SDL2. {SDL.GetError()}");
        }

        var window = SDL.CreateWindowFrom(windowHandle);

        if (window == nint.Zero)
        {
            throw new Exception($"There was an issue creating the window. {SDL.GetError()}");
        }

        var renderer = SDL.CreateRenderer(window, -1, RendererFlags.Accelerated);

        if (renderer == nint.Zero)
        {
            throw new Exception($"There was an issue creating the renderer. {SDL.GetError()}");
        }

        SDL.SetRenderDrawColor(renderer, 0, 255, 0, 255);
        SDL.RenderClear(renderer);
        SDL.RenderPresent(renderer);
    }
}