// using Website_Ecommerce.API.Data;
// using Microsoft.EntityFrameworkCore;
// using Website_Ecommerce.API.Repositories;
// using Website_Ecommerce.API.services;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using Website_Ecommerce.API.Queries;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// var services = builder.Services;

// var connectionString = builder.Configuration.GetConnectionString("Default");
// // Add services to the container.
// services.AddCors(o =>
//     o.AddPolicy("CorsPolicy", builder =>
//         builder.WithOrigins("http://localhost:4200")
//             .AllowAnyHeader()
//             .AllowAnyMethod()));

// var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
// services.AddDbContext<DataContext>(
//     dbContextOptions => dbContextOptions
//         .UseMySql(connectionString, serverVersion)
//         .LogTo(Console.WriteLine, LogLevel.Information)
//         .EnableSensitiveDataLogging()
//         .EnableDetailedErrors()
// );

// services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// services.AddEndpointsApiExplorer();
// services.AddSwaggerGen();


// services.AddTransient<IIdentityServices, IdentityServices>();
// services.AddTransient<IAppQueries>(x=> new AppQueries(connectionString));
// services.AddTransient<IProductRepository, ProductRepository>();
// services.AddTransient<ICategroyRepository, CategroyRepository>();
// services.AddTransient<IUserRepository, UserRepository>();


// services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters()
//         {
//             ValidateIssuer = false,
//             ValidateAudience = false,
//             ValidateLifetime = false,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
//         };
//     });
 
// services.AddScoped<IIdentityServices, IdentityServices>();


// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseCors("CorsPolicy");

// app.UseHttpsRedirection();

// app.UseAuthentication(); // check ban la ai

// app.UseAuthorization(); //ban co quyen gi

// app.MapControllers();

// app.Run();

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PBL4.WebAPI
{
    public class Program
    {
        private static byte[] result = new byte[1024];
        private static int myPort = 6969;
        static Socket serverSocket;
        public static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // conver a ip address to IpAddress Instance
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ipAddress, myPort));
            serverSocket.Listen(10); //quantity of client can listen
            Console.WriteLine("Enpoint dung de giao tiep cua server: ", serverSocket.LocalEndPoint.ToString());
            // khi co 1 client connect toi thi khoi tao 1 thread moi
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            
            CreateHostBuilder(args).Build().Run();
            Console.ReadLine();
        }
        /// <summary>
        /// while a client connect with server and server accept with create a socket instance 
        /// </summary>
        private static void ListenClientConnect()
        {
            while(true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello to Client : "
                    + clientSocket.AddressFamily.ToString()));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }
        private static void ReceiveMessage(object clientSocket) // client get message from server
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    int receiveNumber = myClientSocket.Receive(result); // get data receive length(server send)
                    Console.WriteLine("client endpoint: ", myClientSocket.RemoteEndPoint.ToString(),
                        Encoding.ASCII.GetString(result, 0, receiveNumber)); // 0: offset 
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both); // disable two socket client and server while exception occur
                    myClientSocket.Close(); // release all resource associate with this socket
                    break;
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

