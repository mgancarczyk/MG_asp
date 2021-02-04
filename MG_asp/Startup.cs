using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MG_asp.Middleware;
using MG_asp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication9.Models;
using Microsoft.OpenApi.Models;
using SignalRChat.Hubs;
//using MG_asp.Hubs;

namespace MG_asp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"])); //przekazujemy typ kontesktu
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();

            services.AddTransient<IProductRepository, EFProductRepository>(); //powi¹zanie wszczykujemy interfejs
            services.AddMvc();          //?
            services.AddSwaggerGen();
            services.AddSignalR();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage(); // informacje szczegó³owe o b³êdach

            app.UseHttpMethodOverride();
            app.UseElapsedTimeMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api";
            });

            app.UseStatusCodePages(); // Wyœwietla strony ze statusem b³êdu
            app.UseStaticFiles(); // obs³uga treœci statycznych css, images, js

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {

                routes.MapHub<ChatHub>("/chathub"); // przy jego pomocy odbywa siê komunikacja miêdzy kilentem a serwerem 
                //routes.MapHub<CounterHub>("/counterhub");

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=List}/{id?}");
                routes.MapControllerRoute(
                    name: null,
                    pattern: "Product/{category}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List"
                    });
                routes.MapControllerRoute(
                   name: null,
                   pattern: "Admin",
                   defaults: new
                   {
                       controller = "Admin",
                       action = "Index",
                   });


                
            });
            SeedData.EnsurePopulated(app); // sprawia ¿e dane przy starcie aplikacji s¹ dodawane jesli ich nie ma
        }
    }
}
