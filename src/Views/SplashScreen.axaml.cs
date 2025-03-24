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
        ProgressBar.IsVisible = true;
        InfoText.Text = "Downloading Minecraft Assets...";

        await Task.Delay(1000);
        await AssetDownloader.DownloadIndex(ProgressBar);

        new Editor().Show();
        Close();
    }
}