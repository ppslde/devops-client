using DevOps.Client.Authentication;
using DevOps.Client.Session;
using DevOps.Core.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace DevOps.Client;

internal static class DependencyInjection
{

  public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
  {

    services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    services.AddScoped<ISessionDataStore, SessionDataStore>();

    services.AddScoped<ITokenProvider, SessionDataProvider>();
    services.AddScoped<ISessionData, SessionDataProvider>();

    return services;
  }
}
