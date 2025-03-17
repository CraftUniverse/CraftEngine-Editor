using System.Collections.ObjectModel;

namespace dev.craftengine.editor.ViewModels;

public class AssetBrowserModel : ViewModelBaseModel
{
    public ObservableCollection<Entry> Directories { get; } = [];
    public ObservableCollection<Entry> Files { get; } = [];

    public class Entry
    {
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

        public string Name { get; private set; }
        public string Path { get; private set; }
        public ObservableCollection<Entry>? Children { get; private set; }
    }
}