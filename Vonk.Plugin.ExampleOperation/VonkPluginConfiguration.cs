using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Vonk.Core.Pluggability;

namespace Vonk.Plugin.ExampleOperation {
    [VonkConfiguration (order: 900)] // Needs to be configured before the HttpToVonkConfiguration
    public class CustomContentTypeMiddlewareConfiguration {
        public static IServiceCollection ConfigureServices (IServiceCollection services) {
            return services;
        }

        public static IApplicationBuilder Configure (IApplicationBuilder builder) {
            builder.UseMiddleware<ForwardedHeadersMiddleware> ();
            return builder;
        }
    }
}
