﻿using Abp.AspNetCore;
using Abp.Configuration.Startup;
using Abp.EntityFrameworkCore;
using Abp.MailKit;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Microsoft.Extensions.Configuration;
using ProjectGuard.Ef.Utils;
using ProjectGuard.Services.Background;

namespace ProjectGuard.StartupFiles
{
    [DependsOn(
          typeof(AbpAspNetCoreModule),
          typeof(AbpEntityFrameworkCoreModule),
          typeof(AbpMailKitModule)
      )]
    public class WebModule : AbpModule
    {
        private readonly IConfiguration _configuration;

        public WebModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
            Configuration.Authorization.IsEnabled = false;
            Configuration.DefaultNameOrConnectionString = _configuration.GetConnectionString("PostgreSQL");
            Automigrator.Migrate(Configuration.DefaultNameOrConnectionString);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<ProjectChecker>());
            // потом включить надо)
        }
    }
}
