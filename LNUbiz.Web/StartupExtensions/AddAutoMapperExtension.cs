using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LNUbiz.Web.StartupExtensions
{
    public static class AddAutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()
               .Where(x =>
                   x.FullName.Equals("LNUbiz.BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null") ||
                   x.FullName.Equals("LNUbiz.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")));

            return services;
        }
    }
}
