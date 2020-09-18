using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using ProjectGuard.Ef.Entities;
using System;
using System.Linq;

namespace ProjectGuard.Services.Background
{
    public class ProjectChecker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly FileHashService _fileHashService;
        private readonly DataService _dataService;

        public ProjectChecker(AbpTimer timer, FileHashService fileHashService, DataService dataService)
            : base(timer)
        {
            timer.Period = (int)TimeSpan.Parse("00:00:30").TotalMilliseconds;
            _fileHashService = fileHashService;
            _dataService = dataService;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            var projects = _dataService.GetAllQuery<Project>().ToList();

            foreach (var project in projects)
            {
                _fileHashService.CheckFileHashesAsync(project.Id).Wait();
            }

            CurrentUnitOfWork.SaveChanges();
        }
    }
}
