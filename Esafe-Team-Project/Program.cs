using Esafe_Team_Project.Data;
using Esafe_Team_Project.Helpers;
using Esafe_Team_Project.Middleware;
using Esafe_Team_Project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.FileProviders;
using Quartz;
using Quartz.AspNetCore;
using Esafe_Team_Project.Jobs;
using Esafe_Team_Project.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Service API", Version = "v1" });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB"))
    );

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();



    q.ScheduleJob<ClientUpdateBalanceJob>(trigger => trigger
           .WithIdentity("updating balance", "update Cron Job Group")
           .StartNow()
           //.WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
           .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
           .WithDescription("updating balance every 10 sec ")
       );
});

builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});



builder.Services.Configure<Jwt>(
        builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<MailerConfiguration>(
        builder.Configuration.GetSection("MailerConfiguration"));

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AdminServices>();
builder.Services.AddScoped<ISuperAdminServices, SuperAdminServices>();
builder.Services.AddScoped<IEmailClient, EmailClient>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();





var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Public")),
    RequestPath = "/Public"
});


app.UseMiddleware<Middleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
