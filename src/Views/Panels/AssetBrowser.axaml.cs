using Avalonia.Controls;

namespace dev.craftengine.editor.Views.Panels;

public partial class AssetBrowser : UserControl
{
    public AssetBrowser(string name)
    {
        Name = name;

        InitializeComponent();
    }
}