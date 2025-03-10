using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();

        VersionText.Text = VersionControl.Version;
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        // temp. for developing, until a proper system exists
        Thread.Sleep(1000 * 2);

        new Editor().Show();
        Close();
    }
}