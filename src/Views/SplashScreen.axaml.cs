using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace dev.craftengine.editor.Views;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();
        Focus();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        // temp. for developing, until a proper system exists
        Thread.Sleep(1000 * 2);

        new Editor().Show();
        Close();
    }
}