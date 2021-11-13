using Api.Validators;
using Api.Validators.Interfaces;
using Business.Calculators;
using Business.Mappers;
using Business.Mappers.Interfaces;
using Business.Security;
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
            services.AddTransient<IUserMapper, UserMapper>();
            services.AddTransient<IDefaultSetter, DefaultSetter>();

            services.AddTransient<ICategoryProvider, CategoryProvider>();
            services.AddTransient<ICompanyProvider, CompanyProvider>();
            services.AddTransient<IBrandProvider, BrandProvider>();
            services.AddTransient<IUserProvider, UserProvider>();

            services.AddTransient<IRatingCalculator, RatingCalculator>();
            services.AddTransient<IHasher, Hasher>();
            services.AddTransient<IGenerator, Generator>();

            services.AddTransient<IKeyValidator, KeyValidator>();
            services.AddTransient<IBrandParamsValidator, BrandParamsValidator>();
            services.AddTransient<INewBrandValidator, NewBrandValidator>();
            services.AddTransient<ICompanyValidator, CompanyValidator>();

            return services;
        }
    }
}
