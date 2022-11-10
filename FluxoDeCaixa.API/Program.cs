using FluxoDeCaixa.Consolidado.Service;
using FluxoDeCaixa.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => { builder.WithOrigins("http://localhost").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

BootStrapperConfig.RegisterServices(builder.Services);

builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fluxo de Caixa - V1", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


MapperConfig.Configure(builder.Services);
//JWT
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var key = Encoding.ASCII.GetBytes(configuration["JwtToken:SecretKey"]);
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = true;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuer = configuration["JwtToken:Issuer"]
        };
    });

builder.Services.AddHostedService<ConsolidadoWorkService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors(x => x
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader());

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
