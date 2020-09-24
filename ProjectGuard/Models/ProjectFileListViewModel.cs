using ProjectGuard.Ef.Entities;
using System.Collections.Generic;

namespace ProjectGuard.Models
{
    public class ProjectFileListViewModel
    {
        public ProjectFileListViewModel(Project project)
        {
            ProjectDirectory = new ProjectDirectory("");
            Project = project;
        }

        public ProjectDirectory ProjectDirectory { get; set; }
        public Project Project { get; set; }
    }

    public class ProjectDirectory
    {
        public ProjectDirectory(string name)
        {
            ProjectDirectories = new List<ProjectDirectory>();
            HashValueRows = new List<HashValueRow>();
            Name = name;
        }

        public string Name { get; }
        public List<ProjectDirectory> ProjectDirectories { get; set; }
        public List<HashValueRow> HashValueRows { get; set; }
    }

    public class HashValueRow : HashValue
    {
        public string ShortName { get; set; }

        public HashValueRow(HashValue hashValue, string shortName)
        {
            ShortName = shortName;
            Id = hashValue.Id;
            FileName = hashValue.FileName;
            NeedHash = hashValue.NeedHash;
            Hash = hashValue.Hash;
            ProjectId = hashValue.ProjectId;
            Project = hashValue.Project;
        }
    }
}
