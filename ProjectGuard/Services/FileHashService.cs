﻿using Abp.Application.Services;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Models;
using SharpHash.Base;
using System.Collections.Generic;
using System.Diagnostics;
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
            var sw = Stopwatch.StartNew();

            var hashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => h.ProjectId == projectId)
                .ToListAsync();

            foreach (var hashValue in hashValues)
            {
                if (hashValue.NeedHash)
                {
                    var fileBytes = File.ReadAllBytes(hashValue.FileName);

                    var provider = HashFactory.Crypto.CreateGOST3411_2012_256();
                    var hash = provider.ComputeBytes(fileBytes).ToString();

                    hashValue.Hash = hash;
                }
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed.TotalSeconds);
        }

        public async Task<Verification> CheckFileHashesAsync(int projectId)
        {
            var sw = Stopwatch.StartNew();

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
                    fileCheckResult.HashValue = hashValue;

                    var checkFile = File.Exists(hashValue.FileName);

                    if (!checkFile)
                    {
                        verification.Result = false;
                        fileCheckResult.Result = false;
                        fileCheckResult.Message = "Файл отсутствует.";
                    }
                    else
                    {
                        var fileBytes = File.ReadAllBytes(hashValue.FileName);
                        var provider = HashFactory.Crypto.CreateGOST3411_2012_256();
                        var hash = provider.ComputeBytes(fileBytes).ToString();

                        if (hashValue.Hash != hash)
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
                    }

                    verification.FileCheckResults.Add(fileCheckResult);
                }
            }

            await _dataService.InsertAsync<Verification>(verification);
            sw.Stop();
            Debug.WriteLine(sw.Elapsed.TotalSeconds);

            return verification;
        }

        public async Task ChangeFileNeedHash(int fileId, bool needHash)
        {
            var hashValue = await _dataService.GetAsync<HashValue>(fileId);
            hashValue.NeedHash = needHash;
        }

        public async Task ChangeFilesNeedHash(List<FileNeedHash> needHashes)
        {
            var fileIds = needHashes.Select(n => n.FileId);

            var hashValues = await _dataService.GetAllQuery<HashValue>()
                .Where(h => fileIds.Contains(h.Id))
                .ToListAsync();

            foreach (var fileNeedHash in needHashes)
            {
                var hashValue = hashValues.Find(h => h.Id == fileNeedHash.FileId);
                hashValue.NeedHash = fileNeedHash.NeedHash;
            }
        }
    }
}
