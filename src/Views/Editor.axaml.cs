﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using dev.craftengine.editor.Views.Panels;

namespace dev.craftengine.editor.Views;

public partial class Editor : Window
{
    public Editor()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version;

        PanelLayout1.Children.Add(new Hierarchy("Hierarchy"));
        PanelLayout2.Children.Add(new Panels.GameViewer("GameViewer"));
        PanelLayout3.Children.Add(new AssetBrowser("AssetBrowser"));
        PanelLayout4.Children.Add(new Properties("Properties"));
    }

    private void Menu_File_Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Menu_Preferences_OnClick(object? sender, RoutedEventArgs e)
    {
        var prefWin = new Preferences();
        prefWin.Show(this);
    }
}