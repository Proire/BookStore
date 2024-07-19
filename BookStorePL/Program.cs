using BookStoreBL;
using BookStoreRL;
using BookStoreRL.CQRS.Handlers.UserHandlers;
using Microsoft.IdentityModel.Tokens;
using BookStoreRL.Interfaces.BookRepository;
using BookStoreRL.Interfaces.UserRepository;
using BookStoreRL.Services.BookRepository;
using BookStoreRL.Services.UserRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserRLL.Utilities;
using System.Text;
using BookStoreRL.Interfaces.CardRepository;
using BookStoreRL.Services.CardRepository;
using BookStoreBL.CartService;
using BookStoreRL.Interfaces.CartRepository;
using BookStoreRL.Services.CartRepository;
using BookStoreRL.Interfaces.WishListRepository;
using BookStoreRL.Services.WishListRepository;
using BookStoreBL.WishListService;
using BookStoreRL.Services;
using BookStoreRL.Interfaces.CustomerDetailsRepository;
using BookStoreRL.Services.CustomerDetailsRepository;
using BookStoreBL.CustomerDetailsService;
using BookStoreRL.Interfaces.OrderRepository;
using BookStoreRL.Services.OrderRepository;
using BookStoreBL.OrderService;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("BookStoreDbConnection")));
        
        // Register Repositories
        builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();

        builder.Services.AddScoped<IBookCommandRepository, BookCommandRepository>();   
        builder.Services.AddScoped<IBookQueryRepository, BookQueryRepository>();    

        builder.Services.AddScoped<ICartCommandRepository, CartCommandRepository>(); 
        builder.Services.AddScoped<ICartQueryRepository, CartQueryRepository>();

        builder.Services.AddScoped<IWishListCommandRepository, WishListCommandRepository>();
        builder.Services.AddScoped<IWishListQueryRepository, WishListQueryRepository>();

        builder.Services.AddScoped<ICustomerDetailsCommandRepository, CustomerDetailsCommandRepository>();
        builder.Services.AddScoped<ICustomerDetailsQueryRepostiory,CustomerDetailsQueryRepository>(); 

        builder.Services.AddScoped<IOrderCommandRepository,OrderCommandRepository>();
        builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();  

        builder.Services.AddScoped<EmailSender>();

        // Mediator service
        builder.Services.AddMediatR(typeof(Program).Assembly);

        builder.Services.AddMediatR(typeof(InsertUserCommandHandler).Assembly);

        // Business layer services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<ICartService, CartService>();    
        builder.Services.AddScoped<IWishListService, WishlistService>();
        builder.Services.AddScoped<ICustomerDetailsService, CustomerDetailsService>();
        builder.Services.AddScoped<IOrderService, OrderService>();  

        // utility services
        builder.Services.AddScoped<JwtTokenGenerator>();


        // JWT Configurations
        // Add Appsettings Configuration Builder 
        builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Getting Values from AppSettings.json
        var config = builder.Configuration;
        var secretKey = Environment.GetEnvironmentVariable("SecretKey");
        var issuer = config["Jwt:ValidIssuer"];
        var audience = config["Jwt:ValidAudience"];

        builder.Services.AddAuthentication().AddJwtBearer("AdminScheme", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
            };
        })
        .AddJwtBearer("UserScheme", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
            };
        })
        .AddJwtBearer("UserValidationScheme", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
            };
        })
        .AddJwtBearer("EmailVerificationScheme", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
            };
        });



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}