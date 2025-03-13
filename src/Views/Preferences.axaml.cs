using Avalonia.Controls;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class Preferences : Window
{
    public Preferences()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version + " - Preferences";

        SidebarGeneral.SelectedItems?.Add(SidebarGeneralEdior);
    }

    private void SidebarGeneral_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ClosePanels();

        if (e.AddedItems.Contains(SidebarGeneralEdior)) // GENERAL - EDITOR
        {
            PanelGeneralEditor.IsVisible = true;
        }
        else if (e.AddedItems.Contains(SidebarGeneralPerformance)) // GENERAL - PERFORMANCE
        {
            PanelGeneralPerformance.IsVisible = true;
        }
        else if (e.AddedItems.Contains(SidebarGeneralExternal)) // GENERAL - EXTERNAL TOOLS
        {
            PanelGeneralExternal.IsVisible = true;
        }
    }

    private void ClosePanels()
    {
        PanelGeneralEditor.IsVisible = false;
        PanelGeneralPerformance.IsVisible = false;
        PanelGeneralExternal.IsVisible = false;
    }
}