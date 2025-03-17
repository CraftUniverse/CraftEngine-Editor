using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.GameViewer;

namespace dev.craftengine.editor.Views.Panels;

public partial class GameViewer : UserControl
{
    private SDLWindow _sdlWindow;

    public GameViewer()
    {
        InitializeComponent();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _sdlWindow = new SDLWindow(HostFrame.Handle);
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (!IsLoaded)
        {
            return;
        }

        _sdlWindow.Resize((int)e.NewSize.Width, (int)e.NewSize.Height);
    }
}