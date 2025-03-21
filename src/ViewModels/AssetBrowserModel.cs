using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;

namespace dev.craftengine.editor.ViewModels;

public class AssetBrowserModel : ViewModelBaseModel
{
    public ObservableCollection<Entry> Directories { get; } = [];
    public ObservableCollection<Entry> Files { get; } = [];
    public ObservableCollection<AssetBrowserBreadcrumbItem> Breadcrumbs { get; } = [];

    public enum DisplayType
    {
        ICON,
        THUMBNAIL
    }

    public class DisplayOptions
    {
        public DisplayType DisplayType { get; private set; }
        public string Source { get; private set; }
        public StreamGeometry Icon { get; private set; }

        public DisplayOptions(DisplayType displayType, string source)
        {
            DisplayType = displayType;
            Source = source;

            Application.Current!.Resources.TryGetResource(Source, ThemeVariant.Default, out object? icon);

            Icon = (StreamGeometry)icon!;
        }
    }

    public class Entry
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public DisplayOptions DisplayOptions { get; private set; }
        public bool IsBackButton { get; private set; }
        public ObservableCollection<Entry>? Children { get; private set; }

        public Entry(string name, string path, DisplayOptions displayOptions,
            bool isBackButton = false)
        {
            Name = name;
            Path = path;
            DisplayOptions = displayOptions;
            IsBackButton = isBackButton;
        }

        public Entry(string name, string path, DisplayOptions displayOptions,
            ObservableCollection<Entry> children, bool isBackButton = false)
        {
            Name = name;
            Path = path;
            DisplayOptions = displayOptions;
            IsBackButton = isBackButton;
            Children = children;
        }
    }
}

public class AssetBrowserBreadcrumbItem
{
    public string Name { get; private set; }

    public AssetBrowserBreadcrumbItem(string name)
    {
        Name = name;
    }
}