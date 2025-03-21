using System;
using System.Runtime.InteropServices;
using Avalonia.Platform;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace dev.craftengine.editor.GameViewer;

public class Window
{
    private GraphicsDevice _graphicsDevice;

    public Window(IntPtr windowHandle)
    {
        var windowCI = new WindowCreateInfo()
        {
            WindowTitle = "Veldrid Tutorial"
        };
        var window = VeldridStartup.CreateWindow(ref windowCI);

        var options = new GraphicsDeviceOptions
        {
            PreferStandardClipSpaceYDirection = true,
            PreferDepthRangeZeroToOne = true
        };

        var swapchain = SwapchainSource.CreateWin32(windowHandle, windowHandle);


        while (window.Exists)
        {
            window.PumpEvents();
        }
    }
}