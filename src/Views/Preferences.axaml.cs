using Avalonia.Controls;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class Preferences : Window
{
    public Preferences()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version + " - Preferences";

        SidebarGeneral.SelectedItems?.Add(SidebarGeneralExternal);
    }

    private void SidebarGeneral_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ClosePanels();

        if (e.AddedItems.Contains(SidebarGeneralExternal)) // GENERAL - EXTERNAL TOOLS
        {
            PanelGeneralExternal.IsVisible = true;
        }
        else if (e.AddedItems.Contains(SidebarGeneralExternal_2)) // GENERAL - EXTERNAL TOOLS 2
        {
            PanelGeneralExternal_2.IsVisible = true;
        }
    }

    private void ClosePanels()
    {
        PanelGeneralExternal.IsVisible = false;
        PanelGeneralExternal_2.IsVisible = false;
    }
}