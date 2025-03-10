using Avalonia.Controls;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class Editor : Window
{
    public Editor()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version;
    }
}