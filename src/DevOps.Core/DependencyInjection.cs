﻿using DevOps.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevOps.Core;

public static class DependencyInjection {

    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddScoped<IUserService, UserService>();

        return services;
    }
}