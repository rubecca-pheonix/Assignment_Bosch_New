using Assignment_Bosch.Integration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Assignment_Bosch.Services;
using Assignment_Bosch.Middlewares;
using Assignment_Bosch.SwaggerFilters;
using Assignment_Bosch.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApplicationConfigurations>(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<RabbitMqConnectionManager>();
builder.Services.AddSwaggerGen(config =>
{
    config.OperationFilter<HeaderFilter>();
});

builder.Services.AddTransient<AuthContext>();

builder.Services.AddScoped<IDbConnection>((service) =>

{
    var cns = builder.Configuration.GetValue<string>("ApplicationDbConnection");
    var connection = new SqlConnection(cns);
    connection.Open();
    return connection;

});


    


builder.Services.AddScoped<IDbTransaction>((serviceProvider) =>

{
    var connection = serviceProvider.GetRequiredService<IDbConnection>();
    return connection.BeginTransaction(IsolationLevel.ReadCommitted);

});




builder.Services.AddDbContext<AuthContext>((sp, optionsBuilder) =>

{
    var cn = (DbConnection)sp.GetRequiredService<IDbConnection>();
    //var cns = builder.Configuration.GetConnectionString("ApplicationDbConnection");
    object value = optionsBuilder.UseSqlServer(cn);

});

builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();
builder.Services.AddScoped<ValidateUserService>();
builder.Services.AddScoped<AuditService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<AuditLogMiddleware>();


app.Run();

