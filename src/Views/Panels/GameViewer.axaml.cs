﻿using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Window = dev.craftengine.editor.GameViewer.Window;

namespace dev.craftengine.editor.Views.Panels;

public partial class GameViewer : UserControl
{
    private Window _window;

    public GameViewer()
    {
        InitializeComponent();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _window = new Window(
            HostFrame.Handle,
            (uint)Bounds.Size.Width,
            (uint)Bounds.Size.Height
        );
    }

    private async void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (!IsLoaded)
        {
            return;
        }

        // Fix Maximizing the Window
        await Task.Delay(10);

        _window.swapchain.Resize((uint)e.NewSize.Width, (uint)e.NewSize.Height);
        _window.Paint();
    }
}