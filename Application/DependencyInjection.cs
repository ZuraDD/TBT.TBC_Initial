using System.Linq;
using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.PersonController.Queries.GetPersonInfo.Mappings;
using Application.PersonController.Queries.GetPersonInfo.Models;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile(provider.GetService<IPhotoUploadService>()));
            }).CreateMapper());

            //register custom mappers
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && x.GetInterfaces()
                    .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICustomMapperInterface<,>))))
            {
                foreach (var genericType in type.GetInterfaces().Where(y =>
                    y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICustomMapperInterface<,>)))
                {
                    if (typeof(ICustomScopedMapperInterface).IsAssignableFrom(type))
                    {
                        services.AddScoped(genericType, type);
                    }
                    else if (typeof(ICustomTransientMapperInterface).IsAssignableFrom(type))
                    {
                        services.AddTransient(genericType, type);
                    }
                    else if (typeof(ICustomSingletonMapperInterface).IsAssignableFrom(type))
                    {
                        services.AddSingleton(genericType, type);
                    }
                }
            }

            //register custom service mappers
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && x.GetInterfaces()
                    .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICustomServiceMapperInterface<>))))
            {
                if (typeof(ICustomScopedMapperInterface).IsAssignableFrom(type))
                {
                    services.AddScoped(type);
                }
                else if (typeof(ICustomTransientMapperInterface).IsAssignableFrom(type))
                {
                    services.AddTransient(type);
                }
                else if (typeof(ICustomSingletonMapperInterface).IsAssignableFrom(type))
                {
                    services.AddSingleton(type);
                }
            }

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainExceptionHandlerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
