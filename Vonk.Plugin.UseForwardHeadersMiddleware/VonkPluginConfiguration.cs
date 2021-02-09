using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Vonk.Core.Pluggability;

namespace Vonk.Plugin.UseForwardHeadersMiddleware {
    [VonkConfiguration (order: 900)] // Needs to be configured before the HttpToVonkConfiguration
    public class UseForwardHeadersMiddlewareConfiguration {
        public static IServiceCollection ConfigureServices (IServiceCollection services) {
            if (string.Equals (
                    Environment.GetEnvironmentVariable ("ASPNETCORE_FORWARDEDHEADERS_ENABLED"),
                    "true", StringComparison.OrdinalIgnoreCase)) {
                services.Configure<ForwardedHeadersOptions> (options => {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                        ForwardedHeaders.XForwardedProto;
                    // Only loopback proxies are allowed by default.
                    // Clear that restriction because forwarders are enabled by explicit
                    // configuration.
                    options.KnownNetworks.Clear ();
                    options.KnownProxies.Clear ();
                });
            }

            return services;
        }

        public static IApplicationBuilder Configure (IApplicationBuilder builder) {

            builder.UseMiddleware<ForwardedHeadersMiddleware> ();
            return builder;
        }
    }
}
