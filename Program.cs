using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EFMinimalApi;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(c => c.UseInMemoryDatabase("TasksDB"));
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("stringConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbConnection", async ([FromServices] TasksContext dbContext) => {
    dbContext.Database.EnsureCreated();
    return Results.Ok("In Memory DB: " + dbContext.Database.IsInMemory());
});

app.Run();
