using Application;
using Infrastructure;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddTransient<ValidationExceptionMiddleware>();
builder.Services.AddTransient<InvalidOperationExceptionMiddleware>();
builder.Services.AddTransient<NotFoundExceptionMiddleware>();

// Inject Application Layers
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cors
app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyHeader());
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<InvalidOperationExceptionMiddleware>();
app.UseMiddleware<NotFoundExceptionMiddleware>();

app.MapControllers();

app.Run();