#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.CommonInitializer
 * 唯一标识：fd36704e-1284-481f-afa0-a1216ef9b61a
 * 文件名：WebApplicationBuilderExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:41:41
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>
using DotNetCore.ASPNETCore;
using DotNetCore.Common;
using DotNetCore.Common.JsonConverters;
using DotNetCore.CommonInitializer;
using DotNetCore.EventBus;
using DotNetCore.Infrastructure.EFCore;
using DotNetCore.JWT;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInitializer
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureDbConfiguration(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureAppConfiguration((hostCtx, configBuilder) =>
            {
                //不能使用ConfigureAppConfiguration中的configBuilder去读取配置，否则就循环调用了，因此这里直接自己去读取配置文件
                //var configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                //string connStr = configRoot.GetValue<string>("DefaultDB:ConnStr");
                string connStr = builder.Configuration.GetValue<string>("DefaultDB:ConnStr");
                configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(5));
            });
        }

        /// <summary>
        /// 添加根据传入的不同的接口类型
        /// 实现DI实体类的自动注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="initOptions"></param>
        /// <param name="option"></param>
        public static void ConfigureExtraServices(this WebApplicationBuilder builder, InitializerOptions initOptions,Action<IocOption> option)
        {
            IServiceCollection services = builder.Services;
            IConfiguration configuration = builder.Configuration;
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();

            services.RunModuleInitializers(assemblies, option);
            services.AddAllDbContexts(ctx =>
            {
                //连接字符串如果放到appsettings.json中，会有泄密的风险
                //如果放到UserSecrets中，每个项目都要配置，很麻烦
                //因此这里推荐放到环境变量中。
                string connStr = configuration.GetValue<string>("DefaultDB:ConnStr");
                ctx.UseSqlServer(connStr);
            }, assemblies);

            //开始:Authentication,Authorization
            //只要需要校验Authentication报文头的地方（非IdentityService.WebAPI项目）也需要启用这些
            //IdentityService项目还需要启用AddIdentityCore
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication();
            JWTOptions jwtOpt = configuration.GetSection("JWT").Get<JWTOptions>();
            builder.Services.AddJWTAuthentication(jwtOpt);
            //启用Swagger中的【Authorize】按钮。这样就不用每个项目的AddSwaggerGen中单独配置了
            builder.Services.Configure<SwaggerGenOptions>(c =>
            {
                c.AddAuthenticationHeader();
            });
            //结束:Authentication,Authorization

            services.AddMediatR(assemblies);
            //现在不用手动AddMVC了，因此把文档中的services.AddMvc(options =>{})改写成Configure<MvcOptions>(options=> {})这个问题很多都类似
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<UnitOfWorkFilter>();
            });
            services.Configure<JsonOptions>(options =>
            {
                //设置时间格式。而非“2008-08-08T08:08:08”这样的格式
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            });

            services.AddCors(options =>
            {
                //更好的在Program.cs中用绑定方式读取配置的方法：https://github.com/dotnet/aspnetcore/issues/21491
                //不过比较麻烦。
                var corsOpt = configuration.GetSection("Cors").Get<CorsSettings>();
                string[] urls = corsOpt.Origins;
                options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            }
            );
            services.AddLogging(builder =>
            {
                Log.Logger = new LoggerConfiguration()
                   // .MinimumLevel.Information().Enrich.FromLogContext()
                   .WriteTo.Console()
                   .WriteTo.File(initOptions.LogFilePath)
                   .CreateLogger();
                builder.AddSerilog();
            });
            services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(assemblies);
            });
            services.Configure<JWTOptions>(configuration.GetSection("JWT"));
            services.Configure<IntegrationEventRabbitMQOptions>(configuration.GetSection("RabbitMQ"));
            services.AddEventBus(initOptions.EventBusQueueName, assemblies);

            //Redis的配置
            //string redisConnStr = configuration.GetValue<string>("Redis:ConnStr");
            //IConnectionMultiplexer redisConnMultiplexer = ConnectionMultiplexer.Connect(redisConnStr);
            //services.AddSingleton(typeof(IConnectionMultiplexer), redisConnMultiplexer);
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
            });
        }
    }
}
