using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoCQRS.Infrastructure.Framework;
using TodoCQRS.Infrastructure.MsSql.Repositories;
using TodoCQRS.Web.Application.Hubs;

namespace TodoCQRS.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      services.AddSignalR().AddNewtonsoftJsonProtocol();
      services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();

      return new Container(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan())
        // optional: support for MEF service discovery
        //.WithMef()
        // setup DI adapter
        .WithDependencyInjectionAdapter(services,
          // optional: get original DryIoc.ContainerException if specified type is not resolved, 
          // and prevent fallback to default resolution by infrastructure
          throwIfUnresolved: type => type.Name.EndsWith("Controller"),

          // optional: You may Log or Customize the infrastructure components registrations
          registerDescriptor: (registrator, descriptor) =>
          {
#if DEBUG
            if (descriptor.ServiceType == typeof(ILoggerFactory))
              Console.WriteLine($"Logger factory is registered as instance: {descriptor.ImplementationInstance != null}");
#endif
            return false; // fallback to default registration logic
          })
          
        // Your registrations are defined in CompositionRoot class
        .ConfigureServiceProvider<DryIocConfig>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseSignalR(routes =>
      {
        routes.MapHub<EventHub>("/events");
      });
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseMvc();
      var migrator = app.ApplicationServices.GetService<IStorageSchemeMigrator>();
      migrator.CreateIfNotExists();
      BusConfig.Initialize(app.ApplicationServices.GetService<IBus>());
    }
  }
}
