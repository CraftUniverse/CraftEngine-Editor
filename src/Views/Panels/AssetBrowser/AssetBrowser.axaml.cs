using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using dev.craftengine.editor.ViewModels;
using FluentAvalonia.UI.Controls;

namespace dev.craftengine.editor.Views.Panels.AssetBrowser;

public partial class AssetBrowser : UserControl
{
    private readonly List<Border> _borders;

    public AssetBrowser()
    {
        InitializeComponent();

        _borders = new List<Border>();
        DataContext = new AssetBrowserModel();

        BreadcrumbBar.ItemClicked += BreadcrumbBar_ItemClicked;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as AssetBrowserModel;
        var children = new ObservableCollection<AssetBrowserModel.Entry>();

        vm!.Files.Add(
            new AssetBrowserModel.Entry(
                name: "Back",
                path: "",
                displayOptions:
                new AssetBrowserModel.DisplayOptions(AssetBrowserModel.DisplayType.ICON, "arrow_enter_regular"),
                isBackButton: true
            )
        );

        for (var x = 1; x <= 5; x++)
        {
            children.Add(
                new AssetBrowserModel.Entry(
                    name: $"test_{x}",
                    path: $"@/test/test_{x}",
                    displayOptions:
                    new AssetBrowserModel.DisplayOptions(AssetBrowserModel.DisplayType.ICON, "document_regular")
                )
            );
        }

        vm!.Directories.Add(
            new AssetBrowserModel.Entry(
                name: "test",
                path: "@/test/test",
                displayOptions:
                new AssetBrowserModel.DisplayOptions(AssetBrowserModel.DisplayType.ICON, "document_regular"),
                children
            )
        );

        vm!.Directories.Add(
            new AssetBrowserModel.Entry(
                name: "Test_22",
                path: "@/test/Test_22",
                displayOptions:
                new AssetBrowserModel.DisplayOptions(AssetBrowserModel.DisplayType.ICON, "document_regular")
            )
        );

        var rand = new Random();

        for (var x = 0; x < 5; x++)
        {
            int num = rand.Next(1000000, 9999999);

            vm!.Files.Add(
                new AssetBrowserModel.Entry(
                    name: $"test{num}.txt",
                    path: $"@/test/test{num}.txt",
                    displayOptions:
                    new AssetBrowserModel.DisplayOptions(AssetBrowserModel.DisplayType.ICON, "document_regular")
                )
            );
        }

        vm!.Breadcrumbs.Add(new AssetBrowserBreadcrumbItem(name: "test"));
        vm!.Breadcrumbs.Add(new AssetBrowserBreadcrumbItem(name: "test22"));
    }

    private void ItemClick(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint((Visual?)sender).Properties.IsRightButtonPressed)
        {
            return;
        }

        bool shiftDown = e.KeyModifiers == KeyModifiers.Shift;

        if (!shiftDown)
        {
            ClearBorderList();
        }

        var border = (Border)sender!;
        var data = border.DataContext as AssetBrowserModel.Entry;

        Application.Current!.Styles
            .TryGetResource("SystemAccentColor", null, out var accentColor);

        _borders.Add(border);

        border.Background = new SolidColorBrush((Color)accentColor!);

        if (e.ClickCount != 2) // Only double clicks are allowed
        {
            return;
        }

        if (data.IsBackButton)
        {
            Console.WriteLine("BACK!");

            return;
        }

        Console.WriteLine($"OPEN FILE, {data.Path}");
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var presenters = FileContentHolder.ItemsPanelRoot!.Children;

        foreach (ContentPresenter child in presenters)
        {
            if (child.Child!.IsPointerOver)
            {
                return;
            }
        }

        ClearBorderList();
    }

    private void ClearBorderList()
    {
        foreach (var board in _borders)
        {
            board.Background = new SolidColorBrush(Colors.Gray);
        }

        _borders.Clear();
    }

    private void BreadcrumbBar_ItemClicked(object? sender, BreadcrumbBarItemClickedEventArgs e)
    {
        Console.WriteLine($"Breacrumb Index:{e.Index}");
    }

    private void ContextMenu_OnOpening(object? sender, CancelEventArgs e)
    {
        var menu = sender as ContextMenu;

        foreach (MenuItem? item in menu!.Items)
        {
            if (item == null || item!.GroupName != "NewItem")
            {
                continue;
            }

            var newItemDropdown = new AssetBrowserNewItemDropdown();

            item.Items.Clear();

            foreach (var child in newItemDropdown.Items)
            {
                item.Items.Add(child);
            }

            break;
        }
    }
}