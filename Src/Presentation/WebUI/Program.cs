using System.Text.Encodings.Web;
using System.Text.Unicode;
using Application;
using Application.Common.Interfaces;
using Application.System.Command;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);
//Service Registeration

builder.Services.AddScoped<IAuthHelper, AuthHelper>();
//Add layer registration
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);


//To human readable persian and unicode character
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();




var app = builder.Build();

using ( var scope= app.Services.CreateScope())
{
    var services= scope.ServiceProvider;
    try
    {
        var socialNetworkContext = services.GetService<SocialNetworkApiContext>();
        await socialNetworkContext?.Database.MigrateAsync()!;


        var mediatR = services.GetService<IMediator>();
        await mediatR.Send(new SeedSampleDataCommand(), CancellationToken.None);
    }
    catch (Exception ex)
    {
        var logger = services.GetService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
   
}


app.MapGet("/", () => "Hello World!");

app.Run();
