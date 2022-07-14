using machines;
using machines.DataBase;
using machines.Jobs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();

var connectionString = builder.Configuration.GetConnectionString("MachineDb");
builder.Services.AddDbContext<MachineDbContext>(options =>
{
    options.UseNpgsql(connectionString, builder => { builder.EnableRetryOnFailure(); });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MachineDbContext>();
    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();