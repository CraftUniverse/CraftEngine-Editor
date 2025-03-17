using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dev.craftengine.editor.ViewModels;

public class ProjectListModel : ViewModelBaseModel
{
    public ProjectListModel()
    {
        var projects = new List<ProjectItem>
        {
            new("Test", "1.21.4")
        };

        Projects = new ObservableCollection<ProjectItem>(projects);
    }

    public ObservableCollection<ProjectItem> Projects { get; }

    public class ProjectItem
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public ProjectItem(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}