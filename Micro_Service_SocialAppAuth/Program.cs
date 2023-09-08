using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialAppAuthentication.Data;
using SocialAppAuthentication.Extensions;
using SocialAppAuthentication.Services;
using SocialAppAuthentication.Services.IServices;
using SocialAppAuthentication.Utilities;

var builder = WebApplication.CreateBuilder(args);
//Register IdentityUser

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Db Connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

//Register Identity Framework

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// Register Services
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IJWtTokenGenerator, JWTServices>();

//Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configure JWtOptions 
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Run any Pending Migrations
app.UseMigration();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
