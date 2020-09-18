using Abp.Application.Services;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Services
{
    public class ProjectService : ApplicationService
    {
        private readonly DataService _dataService;

        public ProjectService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Project> AddProjectAsync(string name, string path)
        {
            var fileNames = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var hashValues = new List<HashValue>();

            foreach (var fileName in fileNames)
            {
                var hashValue = new HashValue(fileName);
                hashValues.Add(hashValue);
            }

            var project = new Project(name, path);
            project.HashValues = hashValues;
            await _dataService.InsertAsync(project);

            return project;
        }

        /// <summary>
        /// Создаст дерево каталогов и файлов (костыль, т.к. я не хочу ради этого править бд)
        /// </summary>
        /// <param name="project">Include ProjectFiles</param>
        public async Task<ProjectFileListViewModel> GetProjectFilesViewModel(int projectId)
        {
            // TODO: result
            var project = await _dataService.GetAllQuery<Project>()
                .Include(p => p.HashValues)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            var projectListViewModel = new ProjectFileListViewModel(project);
            var projectDirectories = new List<ProjectDirectory>();

            foreach (var hashValue in project.HashValues)
            {
                var projectRoot = hashValue.FileName.Replace(project.Path, ""); // убрал путь до проекта
                var filePathSplitted = projectRoot.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
                var directories = filePathSplitted.Take(filePathSplitted.Length - 1).ToArray(); // директории
                var fileName = filePathSplitted.LastOrDefault();

                var tmpProjectDirectories = projectDirectories;
                var projectDirectory = projectListViewModel.ProjectDirectory;

                foreach (var directory in directories)
                {
                    projectDirectory = tmpProjectDirectories.Find(pd => pd.Name == directory);

                    if (projectDirectory == null) // нет пути - добавляем
                    {
                        projectDirectory = new ProjectDirectory(directory);
                        tmpProjectDirectories.Add(projectDirectory);
                    }

                    tmpProjectDirectories = projectDirectory.ProjectDirectories; // переходим на уровень ниже
                }

                if (fileName != null)
                {
                    projectDirectory.HashValueRows.Add(new HashValueRow(hashValue, fileName));
                }
            }

            projectListViewModel.ProjectDirectory.ProjectDirectories = projectDirectories;

            return projectListViewModel;
        }
    }
}
