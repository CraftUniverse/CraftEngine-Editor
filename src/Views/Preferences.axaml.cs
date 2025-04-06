using Avalonia.Controls;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views;

public partial class Preferences : SukiWindow
{
    public Preferences()
    {
        InitializeComponent();

        SidebarGeneral.SelectedItems?.Add(SidebarGeneralEditor);
    }

    private void SidebarGeneral_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ClosePanels();

        if (e.AddedItems.Contains(SidebarGeneralEditor)) // GENERAL - EDITOR
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