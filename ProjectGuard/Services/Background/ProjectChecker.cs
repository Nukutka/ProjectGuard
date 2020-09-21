using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Microsoft.Extensions.Configuration;
using ProjectGuard.Ef.Entities;
using ProjectGuard.Services.Email;
using System;
using System.Linq;

namespace ProjectGuard.Services.Background
{
    public class ProjectChecker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly FileHashService _fileHashService;
        private readonly DataService _dataService;
        private readonly EmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public ProjectChecker(AbpTimer timer, FileHashService fileHashService, DataService dataService, EmailSender emailSender, IConfiguration configuration)
            : base(timer)
        {
            _fileHashService = fileHashService;
            _dataService = dataService;
            _emailSender = emailSender;
            _configuration = configuration;
            var period = _configuration.GetSection("Background:Period").Value;
            timer.Period = (int)TimeSpan.Parse(period).TotalMilliseconds;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            var projects = _dataService.GetAllQuery<Project>().ToList();

            foreach (var project in projects)
            {
                var res = _fileHashService.CheckFileHashesAsync(project.Id).GetAwaiter().GetResult();
                if (!res.Result)
                {
                    _emailSender.SendBadVerification(res);
                }
            }

            CurrentUnitOfWork.SaveChanges();
        }
    }
}
