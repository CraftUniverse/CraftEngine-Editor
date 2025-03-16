using Avalonia.Controls;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class BuildProject : Window
{
    public BuildProject()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version + " - Build Project";
    }
}