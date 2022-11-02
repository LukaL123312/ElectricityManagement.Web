using ElectricityManagement.Api.DataSets;
using ElectricityManagement.Api.Middlewares;
using ElectricityManagement.Application;
using ElectricityManagement.Infrastructure;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddWatchDogServices(options =>
{
    options.IsAutoClear = false;
    options.SetExternalDbConnString = builder.Configuration.GetConnectionString("ElectricityManagementConnectionString");
    options.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.MSSQL;
});

var app = builder.Build();

ElectricityMigration.Migration(builder.Services, app.Environment.ContentRootPath);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDogExceptionLogger();

app.UseWatchDog(options =>
{
    options.WatchPageUsername = "admin";
    options.WatchPagePassword = "12345678";
});

app.Run();
