using ProjectGuard.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Models
{
    /// <summary>
    /// вот ето я понимаю костыль
    /// </summary>
    public class ProjectFileListViewModel
    {
        public ProjectFileListViewModel(Project project)
        {
            ProjectDirectory = new ProjectDirectory("");
            Project = project;
        }

        public ProjectDirectory ProjectDirectory { get; set; }

        public Project Project { get; set; }

        /// <summary>
        /// Пройдется рекурсивно по всем директориям
        /// </summary>
        public IEnumerable<ProjectDirectoryRow> GetProjectDirectories(ProjectDirectory projectDirectoryRoot, int offset = 0)
        {
            foreach (var projectDirectory in projectDirectoryRoot.ProjectDirectories)
            {
                foreach (var currentProjectDirectory in GetProjectDirectories(projectDirectory, offset + 1))
                {
                    yield return currentProjectDirectory;
                }

                yield return new ProjectDirectoryRow(projectDirectory, offset + 1);
            }

            // Главная директория проекта
            if (offset == 0)
            {
                yield return new ProjectDirectoryRow(projectDirectoryRoot, offset);
            }
        }
    }

    public class ProjectDirectory
    {
        public ProjectDirectory(string name)
        {
            ProjectDirectories = new List<ProjectDirectory>();
            HashValueRows = new List<HashValueRow>();
            Name = name;
        }

        public string Name { get; set; }
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

    public class ProjectDirectoryRow
    {
        public ProjectDirectoryRow(ProjectDirectory projectDirectory, int offset)
        {
            ProjectDirectory = projectDirectory;
            Offset = offset;
        }

        public ProjectDirectory ProjectDirectory { get; set; }
        public int Offset { get; set; }
    }
}
