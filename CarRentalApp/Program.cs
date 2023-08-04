using CarRentalApp.Data;
using CarRentalApp.Logic.Behaviours;
using CarRentalApp.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
string connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connection));
builder.Services.AddScoped<IDataContext, DataContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    //{
    //    Description = "API Key is required",
    //    Type = SecuritySchemeType.ApiKey,
    //    Name = "XApiKey",
    //    In = ParameterLocation.Header,
    //    Scheme = "ApiKeyScheme"
    //});
    //var key = new OpenApiSecurityScheme()
    //{
    //    Reference = new OpenApiReference
    //    {
    //        Type = ReferenceType.SecurityScheme,
    //        Id = "ApiKey"
    //    },
    //    In = ParameterLocation.Header
    //};
    //var requirement = new OpenApiSecurityRequirement
    //                {
    //                         { key, new List<string>() }
    //                };
    //c.AddSecurityRequirement(requirement);
});
builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }); 
});
builder.Services.AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
//app.UseMiddleware<ApiKeyMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
