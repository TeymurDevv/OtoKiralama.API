using OtoKiralama.Application;
using OtoKiralama.Presentation;
using OtoKiralama.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Register(builder.Configuration);
builder.Services.AddApplicationServices();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
