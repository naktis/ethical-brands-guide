using Api.RequestProcessors.TokenExtractors;
using Api.RequestProcessors.Validators;
using Api.RequestProcessors.Validators.Interfaces;
using Api.Validators;
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
            services.AddTransient<IUserMapper, UserMapper>();

            services.AddTransient<ICategoryProvider, CategoryProvider>();
            services.AddTransient<ICompanyProvider, CompanyProvider>();
            services.AddTransient<IBrandProvider, BrandProvider>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<IBrandCategoryProvider, BrandCategoryProvider>();
            services.AddTransient<IRequestProvider, RequestProvider>();

            services.AddTransient<IRatingCalculator, RatingCalculator>();
            services.AddTransient<IHasher, Hasher>();
            services.AddTransient<IGenerator, Generator>();

            services.AddTransient<IKeyValidator, KeyValidator>();
            services.AddTransient<IBrandParamsValidator, BrandParamsValidator>();
            services.AddTransient<INewBrandValidator, NewBrandValidator>();
            services.AddTransient<ICompanyValidator, CompanyValidator>();
            services.AddTransient<ISharedValidator, SharedValidator>();
            services.AddTransient<IUserValidator, UserValidator>();
            services.AddTransient<IClaimExtractor, ClaimExtractor>();

            return services;
        }
    }
}
