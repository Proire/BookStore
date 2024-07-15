using BookStoreBL;
using BookStoreRL;
using BookStoreRL.CQRS.Handlers.UserHandlers;
using BookStoreRL.Interfaces;
using BookStoreRL.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

        builder.Services.AddScoped<IBookCommandRepository, BookCommandRepository>();   
        builder.Services.AddScoped<IBookQueryRepository, BookQueryRepository>();    

        // Mediator service
        builder.Services.AddMediatR(typeof(Program).Assembly);

        builder.Services.AddMediatR(typeof(InsertUserCommandHandler).Assembly);

        // Business layer services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBookService, BookService>();    

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