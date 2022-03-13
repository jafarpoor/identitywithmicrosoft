using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjctModel.Context;
using ProjctModel.Entites;
using ProjctModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExJwt2
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
            string ConnectionString = @"data source =.; initial catalog = JWTDb2;  integrated security = true  ";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(ConnectionString));
            services.AddControllers();
            services.AddScoped<CategoryService , CategoryService>();
            services.AddScoped<UserService , UserService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExJwt2", Version = "v1" });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultScheme =JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {

                option.RequireHttpsMetadata = false;
                option.SaveToken = true;

                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //RequireExpirationTime = true,
                    //ValidateLifetime = true,
                    //ValidateIssuer = true,
                    //ValidIssuer = "",
                    //ValidateAudience = true,
                    //ValidAudience = " ",
                    //ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A441292-4131-4862-99F5-F074D8566DF1"))
                    //TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("")),
                };

                option.Events = new JwtBearerEvents
                {

                };
            });
        
            services.AddIdentity<User, Role>(option =>
            {
                //pasword
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;


                //lookout
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            }).AddEntityFrameworkStores<DataBaseContext>().AddDefaultTokenProviders();
}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExJwt2 v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
