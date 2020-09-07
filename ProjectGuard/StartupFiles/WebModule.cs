﻿using Abp.AspNetCore;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;

namespace ProjectGuard.StartupFiles
{
    [DependsOn(
          typeof(AbpAspNetCoreModule)
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
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebModule).GetAssembly());
        }
    }
}