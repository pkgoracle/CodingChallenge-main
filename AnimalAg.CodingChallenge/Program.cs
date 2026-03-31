using AnimalAg.CodingChallenge.EF;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Read connection string from configuration (appsettings.json or environment)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddEF(connectionString);

builder.Services.AddApiVersioning(options =>
{
    // Default version to use if none is specified
    options.DefaultApiVersion = new ApiVersion(1, 0);
    // Use the default version if the client doesn't specify one
    options.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise supported versions in the response headers
    options.ReportApiVersions = true;
    // Choose how to read the version (e.g., from the URL segment)
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc()
.AddApiExplorer(options =>
{
    // Format the version as 'v'major[.minor] (e.g., v1.0)
    options.GroupNameFormat = "'v'VVV";
    // Substitute the version in the URL route
    options.SubstituteApiVersionInUrl = true;
});

// OpenAPI documents per version
builder.Services.AddOpenApi("v1", options => { new OpenApiInfo { Title = "My API V1", Version = "v1", Description = "API version 1.0" }; });
builder.Services.AddOpenApi("v2", options => { new OpenApiInfo { Title = "My API V2", Version = "v2", Description = "API version 2.0" }; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
        options.SwaggerEndpoint("/openapi/v2.json", "My API V2");
        options.RoutePrefix = "swagger";
    });
}

app.Run();
