using BitfinexApi.Configurations;
using BitfinexApi.Reports;
using BitfinexApi.Services;
using BitfinexApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace BitfinexApi
{
    public static class BitfinexApiInstaller
    {
        public static IServiceCollection InstallBitfinexApi(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddHttpClient("bitfinex", configuration =>
               {
                   configuration.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                   configuration.BaseAddress = new Uri("https://api.bitfinex.com/");
               });
            services
                .AddScoped<IRequestService, RequestService>()
                .AddScoped<ISecretService, SecretService>()
                .AddScoped<ICzkRateService, CzkRateService>()
                .AddScoped<IPriceCurrencyService, PriceCurrencyService>()
                .AddScoped<IReport, Report>();
            services
                .Configure<BitfinexApiConfiguration>(configuration.GetSection("BitfinexApiConfiguration"));

            return services;
        }
    }
}
