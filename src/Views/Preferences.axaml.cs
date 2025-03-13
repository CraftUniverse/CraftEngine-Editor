﻿using Avalonia.Controls;
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
    }

    private void ClosePanels()
    {
        PanelGeneralExternal.IsVisible = false;
    }
}