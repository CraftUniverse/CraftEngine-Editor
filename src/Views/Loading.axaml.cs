using Avalonia.Controls;

namespace dev.craftengine.editor.Views;

public partial class Loading : Window
{
    public Loading()
    {
        InitializeComponent();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
    }
}