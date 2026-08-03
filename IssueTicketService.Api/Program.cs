using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IssueTicketService.Application.Extensions;
using IssueTicketService.Infrastructure.Extensions;
using System.Security.Claims;
using IssueTicketService.Infrastructure.Seeder;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("../Logs/log-.txt",
             rollingInterval: RollingInterval.Day,
             retainedFileCountLimit: 7)
            .CreateLogger();

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Pass JWT in format: Bearer {jwt_token}"
    });

    opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
    
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCors", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,

        RoleClaimType = ClaimTypes.Role    
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var contextFactory = services.GetRequiredService<IDbContextFactory<IssueTicketServiceDbContext>>();
        
        using var context = contextFactory.CreateDbContext();

        context.Database.Migrate();

        TicketsDbSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Wystąpił błąd podczas aplikowania migracji lub seedowania bazy danych.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("FrontendCors");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();