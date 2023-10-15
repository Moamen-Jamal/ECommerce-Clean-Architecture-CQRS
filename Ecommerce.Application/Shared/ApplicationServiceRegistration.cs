using Ecommerce.Application.Features.Categories.Handlers;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            //services.AddMediatR(typeof(ApplicationServiceRegistration).GetTypeInfo().Assembly);
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //Configure AutoMapper && MediatR && Fluient Validation
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
