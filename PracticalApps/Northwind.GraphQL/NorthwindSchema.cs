﻿using GraphQL.Types; // схема
namespace Northwind.GraphQL;
using Packt.Shared; // NorthwindContext
using Microsoft.Extensions.DependencyInjection; // GetRequiredService
public class NorthwindSchema : Schema
{
    public NorthwindSchema(IServiceProvider provider) : base(provider)
    {
        // Query = new GreetQuery();
        Query = new NorthwindQuery(provider.GetRequiredService<NorthwindContext>());
    }
}

