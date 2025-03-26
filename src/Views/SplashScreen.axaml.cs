using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using dev.craftengine.editor.Minecraft;

namespace dev.craftengine.editor.Views;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();

        VersionText.Text = VersionControl.Version;
    }

    private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        await Task.Delay(1000);

        ProgressBar.IsVisible = true;
        InfoText2.IsVisible = true;
        InfoText.Text = "Downloading Minecraft Assets...";

        await AssetDownloader.Download(ProgressBar, InfoText2, "1.21.4");

        new Editor().Show();
        Close();
    }
}