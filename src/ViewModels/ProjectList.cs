using System.Collections.Generic;

namespace dev.craftengine.editor.ViewModels;

public class ProjectList
{
    public List<Project> Projects { get; }

    public ProjectList()
    {
        Projects = new List<Project>()
        {
            new("Test")
        };
    }

    public class Project
    {
        public string Name { get; set; }

        public Project(string name)
        {
            Name = name;
        }
    }
}