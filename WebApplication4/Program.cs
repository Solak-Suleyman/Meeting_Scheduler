using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Text;
using WebApplication4.Models.Context;

using WebApplication4.Models.Entity;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<MeetingSchedulerContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("MeetingScheduler")));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddControllers().AddNewtonsoftJson();
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
        app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
        );

        //using (var ctx = new MeetingSchedulerContext())
        //{
        //    var user1 = new User() { 
        //        id = 1,
        //        user_name = "suleymansolak",
        //        name="Süleyman",
        //        surname="Solak",
        //        password= Convert.ToBase64String(SHA.ComputeHash(Encoding.UTF8.GetBytes("1234"))
        //    };
        //    ctx.Users.Add(user1);
        //    ctx.SaveChanges();

        //}
        app.Run();

       
    }
}