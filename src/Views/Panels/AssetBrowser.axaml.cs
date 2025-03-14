using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using dev.craftengine.editor.ViewModels;

namespace dev.craftengine.editor.Views.Panels;

public partial class AssetBrowser : UserControl
{
    public AssetBrowser()
    {
        InitializeComponent();
        DataContext = new AssetBrowserModel();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as AssetBrowserModel;

        vm!.Directories.Add(new AssetBrowserModel.Entry("Test22", "@/test/test22"));
        vm!.Files.Add(new AssetBrowserModel.Entry("test22222.txt", "@/test/test22222.txt"));
    }

    private void ItemClick(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint((Visual?)sender).Properties.IsRightButtonPressed) return;
        Console.WriteLine("TEST");
    }
}