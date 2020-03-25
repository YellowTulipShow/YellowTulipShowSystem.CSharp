using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace YTS.WebApi
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 注入服务 Controllers 配置
        /// </summary>
        public static void EnterServiceControllers(this IServiceCollection services)
        {
            // 增加 Controller 注册启用
            services.AddControllers(option =>
            {
                // 关闭 启用端点路由
                option.EnableEndpointRouting = false;
            });
        }

        /// <summary>
        /// 应用程序启用 API 路由路径模板
        /// </summary>
        public static void StartEnableRoute(this IApplicationBuilder app)
        {
            // 启用路由
            app.UseRouting();

            // 使用MVC
            app.UseMvc(routes =>
            {
                // 配置默认MVC路由模板
                routes.MapRoute(
                name: "default",
                template: ApiConfig.APIRoute);
            });
        }

        /// <summary>
        /// 注入服务 跨域配置
        /// </summary>
        public static void EnterServiceCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(ApiConfig.CorsName, builder =>
                {
                    builder.WithOrigins("*").WithHeaders("*");
                });
            });
        }
        /// <summary>
        /// 应用程序启用跨域策略
        /// </summary>
        public static void StartEnableCors(this IApplicationBuilder app)
        {
            // 使用跨域策略
            app.UseCors(ApiConfig.CorsName);
        }

        /// <summary>
        /// 注入服务 Swagger API 浏览配置
        /// </summary>
        public static void EnterServiceSwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            var swaggerInfo = Configuration.GetSection(ApiConfig.APPSettingName_SwaggerInfo);
            var model = swaggerInfo.Get<SwaggerInfo>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            // 注册Swagger生成器，定义1个或多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                // 配置 v1文档
                c.SwaggerDoc(model.Version, new OpenApiInfo
                {
                    Version = model.Version,
                    Title = model.Title,
                    Description = model.Description,
                    Contact = new OpenApiContact
                    {
                        Name = model.Contact.Name,
                        Email = model.Contact.Email,
                        Url = new Uri(model.Contact.Url),
                    },
                    License = new OpenApiLicense
                    {
                        Name = model.License.Name,
                        Url = new Uri(model.License.Url),
                    }
                });

                // //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<SwaggerAuthTokenHeaderParameter>();

                // Set the comments path for the Swagger JSON and UI.
                // 设置Swagger JSON和UI的注释路径。读取代码XML注释文档
                var name = Assembly.GetExecutingAssembly().GetName().Name;
                var xmlFile = $"{name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        /// <summary>
        /// 应用程序启用 Swagger API 文档浏览
        /// </summary>
        /// <param name="app"></param>
        public static void StartEnableSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // 启用中间件以将生成的Swagger用作JSON端点。
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            // 启用中间件以提供swagger-ui（HTML，JS，CSS等），
            // 指定Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(ApiConfig.SwaggerEndpointUrl, ApiConfig.SwaggerEndpointName);
                c.RoutePrefix = string.Empty;
            });
        }

        /// <summary>
        /// 注入服务 Jwt Token 请求验证
        /// </summary>
        public static void EnterServiceJWTAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            var tokenManagement = Configuration.GetSection(ApiConfig.APPSettingName_JWTTokenManagement);
            services.Configure<TokenManagement>(tokenManagement);
            var token = tokenManagement.Get<TokenManagement>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
