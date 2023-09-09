using Microsoft.EntityFrameworkCore;
using SocialApp_Posts.Data;
using SocialApp_Posts.Extensions;
using SocialApp_Posts.Services;
using SocialApp_Posts.Services.IServices;

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
var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();
dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

//Register Services
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
