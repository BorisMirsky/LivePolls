using LivePolls.Application.Services;
using LivePolls.DataAccess;
//using LivePolls.Web.Hubs;
using LivePolls.DataAccess.Repo;
using LivePolls.Web.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    var connection = builder.Configuration.GetConnectionString("Redis");
//    options.Configuration = connection;
//});



string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));


builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//builder.Services.AddSignalR();

builder.Services.AddScoped<IPollsService, PollsService>();
builder.Services.AddScoped<IPollsRepo, PollsRepo>();


builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthorization();



var app = builder.Build();


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.WithOrigins("http://localhost:5063")  //3000")
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//    });
//});

//builder.Services.AddSignalR();
//app.MapHub<VoteHub>("/voting");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors();
//app.UseHttpsRedirection();
//app.UseAuthorization();

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:5063");   //   "h ttp://localhost:3000"
    x.WithMethods().AllowAnyMethod();
});

app.MapControllers();
app.Run();

