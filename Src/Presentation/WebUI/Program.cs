using System.Text.Encodings.Web;
using System.Text.Unicode;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Persistence;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);
//Service Registeration

builder.Services.AddScoped<IAuthHelper, AuthHelper>();


//To human readable persian and unicode character
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

//Add layer registration
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);



var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.Run();
