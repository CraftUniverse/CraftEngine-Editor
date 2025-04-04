using Avalonia.Controls;
using Avalonia.Interactivity;

namespace dev.craftengine.editor.Views.Panels;

public partial class GameViewer : UserControl
{
    public GameViewer()
    {
        InitializeComponent();
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        Control.Game = new editor.GameViewer.GameViewer();
    }
}