using BasicAccessAuthenticationApiServer.BusinessLogic;
using BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;
using BasicAccessAuthenticationApiServer.Executor;
using BasicAccessAuthenticationApiServer.Filters;
using BasicAccessAuthenticationApiServer.Mappers;
using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Models.Options;
using BasicAccessAuthenticationApiServer.Repositories;
using BasicAccessAuthenticationApiServer.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);


#region Business Logic
//a new instance is created for every http request.
builder.Services.AddScoped<IAccountInformationLogic, AccountInformationLogic>();
#endregion

#region DataBase
//A single instance is created and shared across all requests for the entire application's lifetime.
builder.Services.AddSingleton<IDataBaseExecutor, DataBaseExecutor>();
#endregion

#region Mappers
builder.Services.AddSingleton<IAccountInformationMapper, AccountInformationMapper>();
#endregion

#region Repository
builder.Services.AddSingleton<IAccountInformationRepository, AccountInformationRepository>();
#endregion



builder.Services.Configure<ConnectionOptions>(builder.Configuration.GetSection("ConnectionOptions"));

builder.Services.AddScoped<BasicAuthenticationFilter>();
#region BasicAuthenticationRegistration
//register the new created BasicAuthenticationFilter

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new BasicAuthenticationFilter());
});
#endregion

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

//app.UseAuthorization();

app.MapControllers();

app.Run();
