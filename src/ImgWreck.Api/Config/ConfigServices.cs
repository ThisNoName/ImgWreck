using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using ImgWreck.Svc;

namespace ImgWreck.Api;

// register service collection

public static class ConfigServices
{
    public static void AddServiceConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<PingRequest>());
        services.AddAWSService<IAmazonS3>(configuration.GetAWSOptions("S3ImageShed"));
    }

    // copilot auto generated
    //public static IServiceCollection AddAppServices(this IServiceCollection services)
    //{
    //    services.AddMediatR(typeof(PingRequest).Assembly);
    //    services.AddAutoMapper(typeof(PingRequest).Assembly);
    //    services.AddControllers();
    //    services.AddSwaggerGen(c =>
    //    {
    //        c.SwaggerDoc("v1", new() { Title = "ImgRekg.Api", Version = "v1" });
    //    });
    //    return services;
    //}
}