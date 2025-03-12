using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.GameViewer;

namespace dev.craftengine.editor.Views.Panels;

public partial class GameViewer : UserControl
{
    public GameViewer(string name)
    {
        Name = name;

        InitializeComponent();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        new SDLWindow(HostFrame.Handle);
    }
}