using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
    public class Startup
    {
								IConfigurationRoot Configuration;

								public Startup(IHostingEnvironment env)
								{
												Configuration = new ConfigurationBuilder()
																.SetBasePath(env.ContentRootPath)
																.AddJsonFile("appsettings.json").Build();
								}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
												services.AddDbContext<ApplicationDbContext>(options => 
																options.UseSqlServer(
																				Configuration["Data:SportsStoreProducts:ConnectionString"]));
												services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();
//												services.AddMemoryCache();
												services.AddDistributedMemoryCache();
												services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
												app.UseSession(); //Vigtigt at placere denne før UseMvcWithDefaultRoute()" og "UseMvc(routes =>...)" - alt, der har med routes at gøre!!
												app.UseMvcWithDefaultRoute();
												app.UseMvc(routes =>
												{
																routes.MapRoute(
																				name: null,
																				template: "{category}/Page{page:int}",
																				defaults: new { controller = "Product", action = "List" });
																routes.MapRoute(
																				name: null,
																				template: "Page{page:int}",
																				defaults: new { controller = "Product", action = "List", page = 1 });
																routes.MapRoute(
																				name: null,
																				template: "{category}",
																				defaults: new { controller = "Product", action = "List", page = 1 });
																routes.MapRoute(
																				name: null,
																				template: "",
																				defaults: new { controller = "Product", action = "List", page = 1 });
																routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
												});
												SeedData.EnsurePopulated(app);
        }
    }
}
