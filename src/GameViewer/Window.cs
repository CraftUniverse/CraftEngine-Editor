using System;
using Veldrid;

namespace dev.craftengine.editor.GameViewer;

public class Window
{
    public GraphicsDevice graphicsDevice;
    public Swapchain swapchain;
    public CommandList commandList;

    public Window(IntPtr windowHandle, uint width, uint height)
    {
        var options = new GraphicsDeviceOptions
        {
            PreferStandardClipSpaceYDirection = true, PreferDepthRangeZeroToOne = true,
        };

        graphicsDevice = GraphicsDevice.CreateVulkan(options);

        var swapchainSource = SwapchainSource.CreateWin32(windowHandle, windowHandle);

        swapchain = graphicsDevice.ResourceFactory.CreateSwapchain(
            new SwapchainDescription(
                swapchainSource,
                width,
                height,
                null,
                false
            )
        );

        commandList = graphicsDevice.ResourceFactory.CreateCommandList();

        Paint();
    }

    public void Paint()
    {
        commandList.Begin();
        commandList.SetFramebuffer(swapchain.Framebuffer);
        commandList.ClearColorTarget(0, new RgbaFloat(1, 0, 0, 1));
        commandList.End();

        graphicsDevice.SubmitCommands(commandList);
        graphicsDevice.SwapBuffers(swapchain);
    }
}