using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DiplTrab1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ClassRegistroContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("RegistrosBD"));
            });

            //services.AddSingleton<IMateria, InMemoryMateriaService>();
            //services.AddSingleton<IEstudiante, InMemoryEstudianteService>();
            //services.AddScoped<IMateria, ClassRegistroServiceAdo>(sp=> {
            //    return new ClassRegistroServiceAdo(Configuration.GetConnectionString("RegistrosBD"));
            //});
            services.AddScoped<IMateria, ClassRegistroService>();
            services.AddScoped<IEstudiante, ClassRegistroEstService>();

            services.AddRazorPages();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
