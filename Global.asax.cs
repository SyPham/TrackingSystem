using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper.Configuration;
using DemoDoan.Dto;
using DemoDoan.Models;

namespace DemoDoan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MappingConfig.RegisterMapping();
        }

        public class MappingConfig
        {
            public static AutoMapper.IMapper Mapper;

            public static void RegisterMapping()
            {
                var mapperConfig = new AutoMapper.MapperConfiguration(config =>
                {
                    config.CreateMap<UserAccountDto, UserAccount>();
                    config.CreateMap<UserAccount, UserAccountDto>();

                });
                Mapper = mapperConfig.CreateMapper();
            }
        }
    }
}
