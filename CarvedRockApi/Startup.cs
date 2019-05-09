using CarvedRock.Api.Data;
using GraphQL.Server;
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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            //app.UseGraphQL<CarvedRockSchema>();
            //app.UseGraphQLPlayGround(new GraphQLPlayGroundOptions());
            dbContext.Seed();
        }
    }
}
