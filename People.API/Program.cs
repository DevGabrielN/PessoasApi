using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using People.Data.Data;
using People.Domain.Commands.Create;
using People.Domain.Entities;
using People.Domain.Interfaces;
using People.Infra.Data;
using People.Infra.Repositories;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeopleDBContext>(options =>
                options.UseLazyLoadingProxies().UseInMemoryDatabase("PESSOAS"));

builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseLazyLoadingProxies().UseInMemoryDatabase("USUARIOS"));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreatePersonCommand>());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.TryAddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "PessoasAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer Token para autenticação",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Description = "Insira o Token JWT",
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header
    };
    var requiremente = new OpenApiSecurityRequirement
    {
        {
            scheme, new List<string>()
        }
    };
    opts.AddSecurityRequirement(requiremente);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
