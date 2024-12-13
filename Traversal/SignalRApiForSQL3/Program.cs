using Microsoft.EntityFrameworkCore;
using SignalRApiForSQL3.DAL;
using SignalRApiForSQL3.Hubs;
using SignalRApiForSQL3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<VisitorService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
	builder =>
	{
		builder.AllowAnyHeader()
			   .AllowAnyMethod()
			   .SetIsOriginAllowed((host) => true)
			   .AllowCredentials();
	}));

builder.Services.AddControllers();

builder.Services.AddDbContext<Context>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHub<VisitorHub>("/VisitorHub");

app.Run();
