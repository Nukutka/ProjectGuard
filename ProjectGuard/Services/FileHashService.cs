using Abp.Application.Services;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models.Requests;
using ProjectGuard.Services.Security;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Services
{
    public class FileHashService : ApplicationService
    {
        private readonly DataService _dataService;

        public FileHashService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task SetControlHashesAsync(int projectId)
        {
            var hashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => h.ProjectId == projectId)
                .ToListAsync();

            foreach (var hashValue in hashValues)
            {
                if (hashValue.NeedHash)
                {
                    var fileBytes = File.ReadAllBytes(hashValue.FileName);
                    var hash = Streebog.GetHashCode(fileBytes);
                    hashValue.Hash = hash;
                }
            }
        }

        // TODO: поменять модель
        public async Task<Verification> CheckFileHashesAsync(int projectId)
        {
            var hashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => h.ProjectId == projectId)
                .ToListAsync();

            var project = await _dataService.GetAsync<Project>(projectId);

            var verification = new Verification(true, projectId);
            verification.Project = project;

            foreach (var hashValue in hashValues)
            {
                if (hashValue.NeedHash)
                {
                    var fileCheckResult = new FileCheckResult(hashValue.Id);

                    var fileBytes = File.ReadAllBytes(hashValue.FileName);
                    var hash = Streebog.GetHashCode(fileBytes);

                    if (hashValue.Hash == null)
                    {
                        verification.Result = false;
                        fileCheckResult.Result = false;
                        fileCheckResult.Message = "Контрольное значения для файла отсутсвует.";
                    }
                    else if (hashValue.Hash != hash)
                    {
                        verification.Result = false;
                        fileCheckResult.Result = false;
                        fileCheckResult.Message = "Контрольное значение не совпадает с текущим.";
                    }
                    else
                    {
                        fileCheckResult.Result = true;
                        fileCheckResult.Message = "Нормас";
                    }

                    verification.FileCheckResults.Add(fileCheckResult);
                }
            }

            await _dataService.InsertAsync<Verification>(verification);

            return verification;
        }

        public async Task ChangeFileNeedHash(int fileId, bool needHash)
        {
            var hashValue = await _dataService.GetAsync<HashValue>(fileId);
            hashValue.NeedHash = needHash;
        }
    }
}
