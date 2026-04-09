using LivePolls.Application.Services;
using LivePolls.DataAccess;
using LivePolls.Web.Hubs;
using LivePolls.DataAccess.Repo;
using LivePolls.Domain.Abstractions;
using LivePolls.Web.Controllers;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);


string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));


builder.Services.AddControllers();
builder.Services.AddScoped<IVoteHubService, VoteHubService>();
builder.Services.AddScoped<IVoteHubRepository, VoteHubRepository>();
builder.Services.AddScoped<IPollsService, PollsService>();
builder.Services.AddScoped<IPollsRepo, PollsRepo>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddSignalR();

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles(); 
app.UseStaticFiles();

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
