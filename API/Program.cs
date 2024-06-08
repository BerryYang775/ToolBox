using API.Model;
using API.Model.DTO;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var CorsOrigins = "corsOrigin";
// Add services to the container.

builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultContext")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddProblemDetails(opts =>
{
    opts.Map<DataValidationException>(exception => new DataValidationProblemDetails()
    {
        Title = exception.Title,
        Detail = exception.Detail,
        Status = StatusCodes.Status400BadRequest,
        Type = exception.Type,
        Instance = exception.Instance,
        Reasons = exception.Reasons
    });
    opts.Map<APIOperationException>(exception => new APIOperationProblemDetails()
    {
        Title = exception.Title,
        Detail = exception.Detail,
        Status = StatusCodes.Status500InternalServerError,
        Type = exception.Type,
        Instance = exception.Instance,
        Process = exception.Process
    });
    opts.IncludeExceptionDetails = (c, e) => { return false; };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("V1", new OpenApiInfo
    {
        Title = "My Space API",
        Version = "v1"
    });
    //s.AddServer(new OpenApiServer() { Url = builder.Configuration["Host"] });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(o => o.SerializeAsV2 = true);
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V1/swagger.json", "My Space API V1");
    });
}

app.UseHttpsRedirection();

app.UseCors(CorsOrigins);

app.UseAuthorization();

app.MapControllers();
app.UseProblemDetails();

app.Run();
