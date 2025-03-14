using System.Collections.ObjectModel;

namespace dev.craftengine.editor.ViewModels;

public class AssetBrowser : ViewModelBase
{
    public ObservableCollection<Entry> Directories { get; }
    public ObservableCollection<Entry> Files { get; }

    public AssetBrowser()
    {
        Directories = new()
        {
            new Entry("Test1", "@/test/test22"),
        };

        Files = new()
        {
            new Entry("test2222.txt", "@/test/test2222.txt"),
        };
    }

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