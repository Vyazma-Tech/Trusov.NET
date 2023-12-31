﻿using System.Data.Common;
using Infrastructure.DataAccess.Contexts;
using Infrastructure.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Handlers.Fixtures;

public class CoreDatabaseFixture : DatabaseFixture
{
    public DatabaseContext Context { get; private set; } = null!;
    public AsyncServiceScope Scope { get; private set; }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabase(x =>
            x
                // .UseLazyLoadingProxies()
                .UseNpgsql(Container.GetConnectionString()));
    }

    public override async Task ResetAsync()
    {
        await base.ResetAsync();
        Context.ChangeTracker.Clear();
    }

    public override async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await Scope.DisposeAsync();
    }

    protected override DbConnection CreateConnection()
    {
        return Context.Database.GetDbConnection();
    }

    protected override async Task UseProviderAsync(IServiceProvider provider)
    {
        Scope = provider.CreateAsyncScope();
        await Scope.UseDatabase();

        Context = Scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    }
}