using LivePolls.Application.Services;
using LivePolls.DataAccess;
//using LivePolls.Web.Hubs;
using LivePolls.DataAccess.Repo;
using LivePolls.Domain.Abstractions;
using LivePolls.Web.Controllers;
using LivePolls.Web.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
builder.Services.AddScoped<IVoteHubService, VoteHubService>();
builder.Services.AddScoped<IVoteHubRepository, VoteHubRepository>();
builder.Services.AddScoped<IPollsService, PollsService>();
builder.Services.AddScoped<IPollsRepo, PollsRepo>();

builder.Services.AddSignalR();

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthorization();



var app = builder.Build();


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.WithOrigins("http://localhost:5063")
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//    });
//});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "TestPages")),
    RequestPath = "/tests"
});

//app.UseHttpsRedirection();
//app.UseAuthorization();


// for swagger
app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:5063");
    x.WithMethods().AllowAnyMethod();
});


app.UseCors("SignalRPolicy");
app.MapHub<VoteHub>("/VoteHub");
app.MapControllers();

app.Run();

