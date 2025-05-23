﻿// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using DotNetCore.CAP.RabbitMQ;
using DotNetCore.CAP.Transport;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace DotNetCore.CAP;

internal sealed class RabbitMqCapOptionsExtension : ICapOptionsExtension
{
    private readonly Action<RabbitMQOptions> _configure;

    public RabbitMqCapOptionsExtension(Action<RabbitMQOptions> configure)
    {
        _configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(new CapMessageQueueMakerService("RabbitMQ"));

        services.Configure(_configure);
        services.AddSingleton<ITransport, RabbitMqTransport>();
        services.AddSingleton<IConsumerClientFactory, RabbitMqConsumerClientFactory>();
        services.AddSingleton<IConnectionChannelPool, ConnectionChannelPool>();
    }
}