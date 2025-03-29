using Avalonia.Controls;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views.Panels;

public partial class About : Window
{
    public About()
    {
        InitializeComponent();

        VersionText.Text = VersionControl.Version;
    }
}