using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Application;
using Application.Common.Interfaces;
using Application.System.Command;
using FluentValidation.AspNetCore;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using WebUI.Services;


var builder = WebApplication.CreateBuilder(args);
//Service Registration
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthHelper, AuthHelper>();

//Add layer registration
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);


builder.Services.AddHealthChecks()
    //Check the health of database connection 
    .AddDbContextCheck<SocialNetworkApiContext>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson()
    .AddFluentValidation(fv=>
        fv.RegisterValidatorsFromAssemblyContaining<ISocialNetworkDbContext>());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//To use the NSwage razor page output
builder.Services.AddRazorPages();

builder.Services.Configure<ApiBehaviorOptions>(apiopt =>
{
    //Add the invalid model state check filter
    apiopt.SuppressModelStateInvalidFilter = true;
});

//Todo:Add spa from NSwage
// In production, the Angular files will be served from this directory
//services.AddSpaStaticFiles(configuration =>
//{
//    configuration.RootPath = "ClientApp/dist";
//});

//To human readable persian and unicode character
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));


builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Clean Architecture of Social Network Api";
});



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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    RegisteredServicesPages(app);
}

void RegisteredServicesPages(WebApplication application)
{
    application.Map("/services", appBuilder =>
    {
        var sb = new StringBuilder();
        sb.Append("<h1>Registered Services</h1>");
        sb.Append("<table><thead>");
        sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
        sb.Append("</thead><tbody>");
        foreach (var service in )
        {
            
        }
    });
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
});

app.MapGet("/", () => "Hello World!");

app.Run();
