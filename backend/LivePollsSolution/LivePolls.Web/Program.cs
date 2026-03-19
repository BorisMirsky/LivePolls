using LivePolls.Web.Hubs;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    var connection = builder.Configuration.GetConnectionString("Redis");
//    options.Configuration = connection;
//});


builder.Services.AddControllers();

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

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
