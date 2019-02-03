using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWorkshop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiWorkshop
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
            services.AddDbContext<StudentDBContext>(options =>
            options.UseInMemoryDatabase("StudenDB"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StudentDBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            if (!context.Students.Any())
            {
                context.Students.AddRange(new List<Student>()

                {
                    new Student() {FirstName="Edward",LastName="Rojas",Email="efrojasm@gmail.com",Address="Aguas Zarcas" },
                    new Student() { FirstName = "Francisco", LastName = "Mendez", Email = "efrojasm23@gmail.com", Address = "Los Chiles" }

                });
                context.SaveChanges();
            }
        }
    }
}
