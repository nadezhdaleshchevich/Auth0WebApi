using DataAccess.Extensions;
using DataAccess.Services.Extensions;
using Web.Services.Extensions;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options => options.LoadSwaggerGenOptions());

builder.Services
    .AddAuthentication(options => options.LoadAuthenticationOptions())
    .AddJwtBearer(options => options.LoadJwtBearerOptions(builder.Configuration));

builder.Services.AddControllers();

builder.Services.LoadDataAccessTypes();
builder.Services.LoadDataAccessServicesTypes();
builder.Services.LoadWebServicesTypes();
builder.Services.LoadWebApiServicesTypes();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }