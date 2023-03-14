using DevOps.Core.Contracts;
using DevOps.Infrastructure.AzureDevOps;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevOps.Infrastructure;

public static class DependencyInjection
{

  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
  {

    services.AddScoped<IUserProfileClient, UserProfileClient>();
    services.AddScoped<IOrganisationClient, OrganisationAccountClient>();
    services.AddScoped<IAvatarClient, AvatarClient>();

    return services;
  }
}