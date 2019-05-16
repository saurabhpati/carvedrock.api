using CarvedRock.Api.Data;
using CarvedRockApi.GraphQL;
using CarvedRockApi.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRockApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarvedRockDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CarvedRock")));
            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
                .AddScoped<CarvedRockSchema>()
                .AddScoped<IDependencyResolver>(provider => new FuncDependencyResolver(provider.GetRequiredService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            app.UseGraphQL<CarvedRockSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            dbContext.Seed();
        }
    }
}
