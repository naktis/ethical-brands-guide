using Api.Validators;
using Business.Calculators;
using Business.Mappers;
using Business.Mappers.Interfaces;
using Business.Services;
using Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IBrandMapper, BrandMapper>();
            services.AddTransient<ICategoryMapper, CategoryMapper>();
            services.AddTransient<ICompanyMapper, CompanyMapper>();
            services.AddTransient<IRatingMapper, RatingMapper>();
            services.AddTransient<IBrandCategoryMapper, BrandCategoryMapper>();
            services.AddTransient<IBrandCategoryProvider, BrandCategoryProvider>();

            services.AddTransient<ICategoryProvider, CategoryProvider>();
            services.AddTransient<ICompanyProvider, CompanyProvider>();
            services.AddTransient<IBrandProvider, BrandProvider>();

            services.AddTransient<IRatingCalculator, RatingCalculator>();

            services.AddTransient<IValidator, Validator>();

            return services;
        }
    }
}
