using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
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

        var parentWindow = (Avalonia.Controls.Window)TopLevel.GetTopLevel(this)!;
        var iconUri = new Uri("avares://CraftEngine Editor/Assets/Icon.ico");

        parentWindow.Icon = new WindowIcon(AssetLoader.Open(iconUri));
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