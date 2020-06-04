using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets; 

namespace WebSocketServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();                                                    // Adds websocket fuctionality to the server

            app.Use(async (context, next) => 
            {
                if (context.WebSockets.IsWebSocketRequest)                          // checks if the request is a websocket request type       
                {
                    // Accepts the incomming websocket connection
                    WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
                    Console.WriteLine("WebSocket Connected");
                }
                else 
                {
                    Console.WriteLine("hello from the 2rd request delegate.");
                    await next();                                                   // whait for the next request
                }
            });

            app.Run(async context =>
            {   
                Console.WriteLine("hello from the 3rd request delegate.");                      // write to server console window 
                await context.Response.WriteAsync("hello from the 3rd request delegate.");      // write a response
            });
        }
    }
}
