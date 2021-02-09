using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.Data.DbModels.SecuritySchema;
using InsuranceClaims.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;

namespace InsuranceClaims.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.WebHostEnvironment = webHostEnvironment;
            this.Configuration = configuration;
        }

        public IWebHostEnvironment WebHostEnvironment
        {
            get;
            private set;
        }

        public IConfiguration Configuration
        {
            get;
            private set;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            if (this.Configuration.GetValue<bool>("EnableSwagger"))
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = $"Insurance Claims Api Gateway ({this.WebHostEnvironment.EnvironmentName})",
                            Version = "v1"
                        });

                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows()
                        {
                            Password = new OpenApiOAuthFlow()
                            {
                                //TokenUrl = new Uri(jwtSettings.TokenEndpoint),
                            }
                        }
                    });

                    //c.OperationFilter<AuthorizeCheckOperationFilter>();
                    c.CustomSchemaIds(i => i.FullName);
                });
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(this.Configuration["StoreOptions:ConnectionString"]));

            // Auto Mapper
            services.AddAutoMapper();

            #region Identity
            services.AddAuthorization();
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider("InsuranceClaims", typeof(DataProtectorTokenProvider<ApplicationUser>));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var signingKey = Convert.FromBase64String(Configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey)
                };
            });
            #endregion

            // Registe our services with Autofac container
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutoFacConfiguration());
            builder.Populate(services);
            IContainer container = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext appDbContext, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment()) // Development
            {
                app.UseDeveloperExceptionPage();
            }
            else // Production
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            DataSeedingIntilization.Seed(appDbContext, serviceProvider);


            if (this.Configuration.GetValue<bool>("EnableSwagger"))
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        "../swagger/v1/swagger.json",
                        $"Insurance Claims v1 ({this.WebHostEnvironment.EnvironmentName})");

                    c.OAuthClientId("test");
                    c.OAuthAppName("Insurance Claims ApiGateway");
                });
            }
        }
    }
}
