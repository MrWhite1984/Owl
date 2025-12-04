using ConfHub.Core.Application.ChatMessages.Interfaces;
using ConfHub.Core.Application.ChatMessages.Services;
using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Application.Conferences.Services;
using ConfHub.Core.Application.ConferenceSettings.Interfaces;
using ConfHub.Core.Application.ConferenceSettings.Services;
using ConfHub.Core.Application.News.Interfaces;
using ConfHub.Core.Application.News.Services;
using ConfHub.Core.Application.Notifications.Interfaces;
using ConfHub.Core.Application.Notifications.Services;
using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Application.Persons.Services;
using ConfHub.Core.Application.ProjectParticipants.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.Services;
using ConfHub.Core.Application.Projects.Interfaces;
using ConfHub.Core.Application.Projects.Services;
using ConfHub.Core.Application.Sections.Interfaces;
using ConfHub.Core.Application.Sections.Services;
using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Application.Users.Services;
using ConfHub.Core.Infrastructure.Persistence;
using ConfHub.Core.Infrastructure.Persistence.Repositories;
using ConfHub.Core.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork>(provider =>
    provider.GetRequiredService<AppDbContext>());

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<IConferenceService, ConferenceService>();
builder.Services.AddScoped<IConferenceSettingsService, ConferenceSettingsService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IProjectParticipantService, ProjectParticipantService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISectionService, SectionService>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();
builder.Services.AddScoped<IConferenceSettingsRepository, ConferenceSettingsRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IProjectParticipantRepository, ProjectParticipantRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ConfHub.Core.Api", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "¬ведите JWT-токен в формате: Bearer {токен}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();    
app.UseAuthentication();       
app.UseAuthorization();

app.MapControllers();

app.Run();

