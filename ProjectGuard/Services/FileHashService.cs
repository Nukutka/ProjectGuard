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
    public class FileHashService
    {
        private readonly DataService _dataService;

        public FileHashService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task SetControlHashesAsync(List<string> filePaths)
        {
            var hashValues = new List<HashValue>();

            foreach (var filePath in filePaths)
            {
                var fileBytes = File.ReadAllBytes(filePath);
                var hash = Streebog.GetHashCode(fileBytes);
                var hashValue = new HashValue(filePath, true, hash);
                hashValues.Add(hashValue);
            }

            await _dataService.BulkInsertAsync<HashValue>(hashValues);
        }

        public async Task<List<CheckFileHashesOutput>> CheckFileHashesAsync(List<string> filePaths)
        {
            var result = new List<CheckFileHashesOutput>();

            var dbHashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => filePaths.Contains(h.FileName))
                .ToListAsync();

            foreach (var filePath in filePaths)
            {
                var fileBytes = File.ReadAllBytes(filePath);
                var hash = Streebog.GetHashCode(fileBytes);

                var dbHashValue = dbHashValues.FirstOrDefault(h => h.FileName == filePath);
                if (dbHashValue == null)
                {
                    result.Add(new CheckFileHashesOutput(filePath, false, "Контрольное значения для файла отсутсвует."));
                }
                else if (dbHashValue.Hash != hash)
                {
                    result.Add(new CheckFileHashesOutput(filePath, false, "Контрольное значение не совпадает с текущим."));
                }
                else
                {
                    result.Add(new CheckFileHashesOutput(filePath, true, ""));
                }
            }

            return result;
        }
    }
}
