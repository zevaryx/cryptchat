using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptChat.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using CryptChat.Server.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CryptChat.Server
{
    public class Startup
    {

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json",
                         optional: false,
                         reloadOnChange: true)
            .AddEnvironmentVariables();
            
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.Configure<UsersCollectionSettings>(Configuration.GetSection(nameof(UsersCollectionSettings)));
            services.Configure<ChatsCollectionSettings>(Configuration.GetSection(nameof(ChatsCollectionSettings)));
            services.Configure<MessagesCollectionSettings>(Configuration.GetSection(nameof(MessagesCollectionSettings)));

            services.AddSingleton<IMongoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value
            );
            services.AddSingleton<IUsersCollectionSettings>(sp =>
                sp.GetRequiredService<IOptions<UsersCollectionSettings>>().Value
            );
            services.AddSingleton<IChatsCollectionSettings>(sp =>
                sp.GetRequiredService<IOptions<ChatsCollectionSettings>>().Value
            );
            services.AddSingleton<IMessagesCollectionSettings>(sp =>
                sp.GetRequiredService<IOptions<MessagesCollectionSettings>>().Value
            );

            services.AddSingleton<UserService>();
            services.AddSingleton<ChatService>();
            services.AddSingleton<MessageService>();
            
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ServerService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
