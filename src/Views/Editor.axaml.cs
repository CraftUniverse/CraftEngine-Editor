using System.Diagnostics;
using System.Reflection;
using Avalonia.Controls;

namespace dev.craftengine.editor.Views;

public partial class Editor : Window
{
    public Editor()
    {
        InitializeComponent();

        var version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location)?.ProductVersion;

        Title = "CraftEngine Editor " + version;
    }
}