using Microsoft.EntityFrameworkCore;
using SocialApp_EmailService.Data;
using SocialApp_EmailService.Extension;
using SocialApp_EmailService.Massaging;
using SocialApp_EmailService.Messaging;
using SocialApp_EmailService.Services;

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
builder.Services.AddSingleton(new EmailService(dbContextBuilder.Options));

//Register Service
builder.Services.AddSingleton<IAzureMessageBusConsumer, AzureMessageBusConsumer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.useAzure();

app.UseMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
