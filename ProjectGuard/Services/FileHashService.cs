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

        public async Task SetControlHashesAsync(int[] hashValueIds, int projectId)
        {
            var hashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => h.ProjectId == projectId)
                .ToListAsync();

            foreach (var hashValue in hashValues)
            {
                if (hashValueIds.Contains(hashValue.Id))
                {
                    var fileBytes = File.ReadAllBytes(hashValue.FileName);
                    var hash = Streebog.GetHashCode(fileBytes);
                    hashValue.NeedHash = true;
                    hashValue.Hash = hash;
                }
                else
                {
                    hashValue.NeedHash = false;
                }
            }
        }

        // TODO: поменять модель
        public async Task<List<CheckFileHashesOutput>> CheckFileHashesAsync(int[] hashValueIds, int projectId)
        {
            var result = new List<CheckFileHashesOutput>();

            var hashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => h.ProjectId == projectId)
                .ToListAsync();

            var verification = new Verification(true);

            foreach (var hashValue in hashValues)
            {
                if (hashValue.NeedHash && hashValueIds.Contains(hashValue.Id))
                {
                    var fileCheckResult = new FileCheckResult(hashValue.Id);

                    var fileBytes = File.ReadAllBytes(hashValue.FileName);
                    var hash = Streebog.GetHashCode(fileBytes);

                    if (hashValue.Hash == null)
                    {
                        verification.Result = false;
                        fileCheckResult.Result = false;
                        result.Add(new CheckFileHashesOutput(hashValue.FileName, false, "Контрольное значения для файла отсутсвует."));
                    }
                    else if (hashValue.Hash != hash)
                    {
                        verification.Result = false;
                        fileCheckResult.Result = false;
                        result.Add(new CheckFileHashesOutput(hashValue.FileName, false, "Контрольное значение не совпадает с текущим."));
                    }
                    else
                    {
                        fileCheckResult.Result = true;
                        result.Add(new CheckFileHashesOutput(hashValue.FileName, true, ""));
                    }

                    verification.FileCheckResults.Add(fileCheckResult);
                }
            }

            await _dataService.InsertAsync<Verification>(verification);

            return result;
        }
    }
}
