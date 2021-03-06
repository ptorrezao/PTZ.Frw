﻿using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PTZ.Frw.DataAccess.EF;
using PTZ.Frw.DataAccess;
using PTZ.Frw.DataAccess.Repositories;
using PTZ.Frw.DataAccess.EF.Repositories;
using PTZ.Frw.WebAPI.Library.Services;
using PTZ.Frw.WebAPI.Library.Interfaces;

namespace PTZ.Frw.WebAPI
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
            services.AddMvc();

            this.AddSwagger(services);
            this.AddAuthentication(services);
            this.DatabaseContext(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ISignInManager, SignInManager>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PTZ.Frw - V1");
            });

            app.UseAuthentication();
            app.UseMvc();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = Configuration["Tokens:Issuer"],
                                    ValidAudience = Configuration["Tokens:Issuer"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                                };
                            });
        }

        private void DatabaseContext(IServiceCollection services)
        {
            var server = Configuration["Database:Server"];
            var database = Configuration["Database:Name"];
            var user = Configuration["Database:User"];
            var password = Configuration["Database:Password"];
            var connection = String.Format("Server={0};Database={1};User={2};Password={3};", server, database, user, password);

            services.AddDbContext<PTZFrwContext>(options => options.UseSqlServer(connection));
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "PTZ.Frw",
                    Version = "v1",
                    Contact = new Contact()
                    {
                        Name = "Pedro Torrezão",
                        Url = "https://github.com/ptorrezao/PTZ.Frw"
                    },
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.IgnoreObsoleteActions();
                
            });
          
        }
    }
}
