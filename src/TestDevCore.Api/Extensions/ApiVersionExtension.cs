using Asp.Versioning;

namespace TestDevCore.Api.Extension
{
    public static class ApiVersionExtension
    {
        public static IServiceCollection AddApiVersionConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
