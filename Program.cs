using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ezapiekanka.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ezapiekanka.JwtService;
using ezapiekanka.Repository.Interfaces;
using ezapiekanka.Repository.Services;
using ezapiekanka.Services.ProductService;
using ezapiekanka.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

//DbContexts
builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]!));
builder.Services.AddDbContext<ProductContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]!));

builder.Services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<UserContext>());
builder.Services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<ProductContext>());

//Add My Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IJwtAuth, JwtAuth>();



//Add builders Services
//Json Parser
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.WriteIndented = true;
}).AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

//Configuration
builder.Services.Configure<string>(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//Jwt tokens
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
          ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(JwtPolicies.Vendor, JwtPolicies.Policy(JwtPolicies.Vendor));
    config.AddPolicy(JwtPolicies.User, JwtPolicies.Policy(JwtPolicies.User));
});


var app = builder.Build();

app.MapGet("/", () => "Is Work!");

app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

//Add Authorization
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();