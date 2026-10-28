using Incidentapi_mounaa.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IncidentsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IncidentsConnection")));
var app = builder.Build();
 

    
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();