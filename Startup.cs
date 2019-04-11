using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Innovativo.Services;
using Innovativo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Innovativo
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string connection = Configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<InnovativoContext>(options => options.UseLazyLoadingProxies().UseMySql(connection));
            services.AddMvc();   

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configuração do jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Segredo);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var usuarioService = context.HttpContext.RequestServices.GetRequiredService<IUsuarioService>();
                        int usuarioID = int.Parse(context.Principal.Identity.Name);
                        Usuario usuario = usuarioService.ObterPorID(usuarioID);
                        if (usuario == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew= TimeSpan.Zero
                };
            });
            services.AddScoped<IUsuarioService, UsuarioService>();             
            services.AddScoped<IEficaciaCanaisService, EficaciaCanaisService>();        
            services.AddScoped<IClienteService, ClienteService>();
            services.AddAutoMapper();
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
            ConfigurarCors(app,false);
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

        }

        private void ConfigurarCors(IApplicationBuilder app, bool habilitarTudo)
        {
            CorsPolicyBuilder cp = new CorsPolicyBuilder();
            if(habilitarTudo)
            {
                app.UseCors(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                ); 
            }
            else
            {
                app.UseCors(builder =>
                    builder
                    .WithOrigins("http://marketing62.tempsite.ws")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                ); 
            }

            app.UseCors(builder=>builder=cp); 
        }
    }
}
