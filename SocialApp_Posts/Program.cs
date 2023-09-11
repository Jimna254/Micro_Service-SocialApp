using Microsoft.EntityFrameworkCore;
using SocialApp_Posts.Data;
using SocialApp_Posts.Extensions;
using SocialApp_Posts.Services;
using SocialApp_Posts.Services.IServices;
using SocialApp_Posts.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Register Services
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();


//custom builders
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();

// config user api
builder.Services.AddHttpClient("Comment", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// add all pending migrations to the  db
app.UseMigration();

// add swagger & Auth
 app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
