using Abp.Application.Services;
using ProjectGuard.Ef.Entities;
using System.Collections.Generic;
using System.IO;
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

            var project = new Project(name, path, hashValues);
            await _dataService.InsertAsync(project);

            return project;
        }
    }
}
