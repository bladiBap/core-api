using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestDevCore.Api.Configuration
{
    public class ConfiguraSwaggerOption : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfiguraSwaggerOption(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = $"SolTestBackend API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = "API documentation for SolTestBackend API",
                    }
                );
            }
        }
    }
}
