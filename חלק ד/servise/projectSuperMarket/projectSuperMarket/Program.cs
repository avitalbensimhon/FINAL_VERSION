using projectSuperMarket.Controllers;
using projectSuperMarket;
using projectSuperMarket.Servies.Servies;
using projectSuperMarket.core.Repository;
using projectSuperMarket.Data.Repository;
using projectSuperMarket.core.Servies;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt => opt.AddPolicy("MyPolicy", policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "projectSuperMarket", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "הכנס את ה-JWT עם המילה Bearer לפניו (Bearer token)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddAuthorization();
builder.Services.AddScoped<IGoodsServies,GoodsServies>();
builder.Services.AddScoped<IGoodsRepository, GoodsRepository>();
builder.Services.AddScoped<IOrderServies, OrderServies>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISupplersServies, SupplersServies>();
builder.Services.AddScoped<ISupplersRepository, SupplersRepository>();
builder.Services.AddScoped< AuthService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped< AutoOrderService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


builder.Services.AddDbContext<DataContext>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
