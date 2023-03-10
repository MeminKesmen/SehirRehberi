using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SehirRehberi.Api.Helpers;
using SehirRehberi.Business.Abstract;
using SehirRehberi.Business.Concrete;
using SehirRehberi.DataAccess;
using SehirRehberi.DataAccess.Abstract;
using SehirRehberi.DataAccess.Concrete;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddScoped<ICityDal, EfCityDal>();
builder.Services.AddScoped<ICityService, CityManager>();

builder.Services.AddScoped<IPhotoDal, EfPhotoDal>();
builder.Services.AddScoped<IPhotoService, PhotoManager>();

builder.Services.AddScoped<IAuthService,AuthManager>();

builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySetting"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy =>
//policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
policy.AllowAnyHeader().SetIsOriginAllowed(origin=>true).AllowAnyMethod().AllowCredentials()
));
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey=new SymmetricSecurityKey(key),
        ValidateIssuer=false,
        ValidateAudience=false,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
