using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.GameViewer;
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
        _window = new Window(HostFrame.Handle);
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (!IsLoaded)
        {
            return;
        }

  //      _window.Resize((int)e.NewSize.Width, (int)e.NewSize.Height);
    }
}