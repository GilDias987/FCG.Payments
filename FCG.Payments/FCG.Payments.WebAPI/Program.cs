using FCG.Payments.Application.Interface.Repository.Base;
using FCG.Payments.Application.UseCases.Registration;
using FCG.Payments.Infrastructure.Context;
using FCG.Payments.Infrastructure.Repository;
using FCG.Payments.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Api Payments - Fiap Cloud Game";
    options.Version = "1.0";
    options.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Description = "Bearer token authorization header",
        Type = NSwag.OpenApiSecuritySchemeType.Http,
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer"
    });

    options.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings"));
}, ServiceLifetime.Scoped);

Console.WriteLine(configuration.GetConnectionString("ConnectionStrings"));

#region [JWT]

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

#endregion

#region Exception Global
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion

#region Repository
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
#endregion

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddProblemDetails();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMINISTRADOR", policy => policy.RequireRole("ADMINISTRADOR"));
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
