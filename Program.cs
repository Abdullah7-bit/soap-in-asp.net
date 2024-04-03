using SoapCore;
using SOAP_Services;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // Add XML output formatter
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    // Optionally, remove the default JSON output formatter if you only want XML responses
    //options.OutputFormatters.RemoveType<JsonOutputFormatter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding soap services
builder.Services.AddSingleton<IAuthorService, AuthorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// SOAP Endpoint 
app.UseSoapEndpoint<IAuthorService>("/Service.asmx", new SoapEncoderOptions());

app.UseAuthorization();

app.MapControllers();

app.Run();
