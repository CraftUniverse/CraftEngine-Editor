using System;
using System.Reflection;
using Eremex.AvaloniaUI.Controls.Common;

namespace dev.craftengine.editor;

public partial class MainWindow : MxWindow
{
    public MainWindow()
    {
        InitializeComponent();

        var version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion;

        Console.WriteLine(version);
    }
}