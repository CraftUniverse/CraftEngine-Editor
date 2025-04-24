using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using dev.craftengine.editor.Minecraft.AssetDownloader;

namespace dev.craftengine.editor.Views;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();

        Title = $"CraftEngine Editor {VersionControl.Version}";
        
        ProjectName.Text = ProjectManagement.ProjectManagement.CurrentProjectConfig?.ProjectName;
        VersionText.Text = VersionControl.Version;
        CreditText.Text = editor.Resources.Resources.startup_image_credits.Replace("{credit}", "unknown");
    }

    private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        await Task.Delay(1000);

        ProgressBar.IsVisible = true;
        InfoText2.IsVisible = true;
        InfoText.Text = editor.Resources.Resources.startup_text_downloading_minecraft_assets;

        await AssetDownloader.Download(ProgressBar, InfoText2, "1.21.4");

        new Editor().Show();
        Close();
    }
}