using System.Collections.ObjectModel;

namespace dev.craftengine.editor.ViewModels;

public class AssetBrowserModel : ViewModelBaseModel
{
    public ObservableCollection<Entry> Directories { get; } = [];
    public ObservableCollection<Entry> Files { get; } = [];
    public ObservableCollection<AssetBrowserBreadcrumbItem> Breadcrumbs { get; } = [];

    public class Entry
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public ObservableCollection<Entry>? Children { get; private set; }

        public Entry(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public Entry(string name, string path, ObservableCollection<Entry> children)
        {
            Name = name;
            Path = path;
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