using LivePolls.Web.Hubs;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    var connection = builder.Configuration.GetConnectionString("Redis");
//    options.Configuration = connection;
//});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=polls.db"));


builder.Services.AddControllers();
builder.Services.AddSignalR();

//builder.Services.AddOpenApi();

var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

app.MapHub<VoteHub>("/voting");

//app.UseCors();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
