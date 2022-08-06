using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EFMinimalApi;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(c => c.UseInMemoryDatabase("TasksDB"));
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("stringConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//app.MapGet("/dbConnection", async ([FromServices] TasksContext dbContext) => {
//    dbContext.Database.EnsureCreated();
//    return Results.Ok("In Memory DB: " + dbContext.Database.IsInMemory());
//});

app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) =>
{
    return Results.Ok(dbContext.Tasks);
});

app.MapPost("/api/task", async ([FromServices] TasksContext dbContext, EFMinimalApi.Models.Task task) => {
    return Results.Ok(dbContext.Tasks.Where(x => x.TaskId.Equals(task.TaskId)));
});

app.MapPost("/api/CreateTask", async ([FromServices] TasksContext dbContext, EFMinimalApi.Models.Task task) => {

    try
    {
        task.TaskId = Guid.NewGuid();
        task.CreationDateTime = DateTime.Now;
        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();
        return Results.Ok(task);
    }
    catch (Exception err)
    {
        throw new Exception("Task not created. Error: " + err);
    }

});

app.MapPut("/api/ModifyTask/{Id}", async ([FromServices] TasksContext dbContext, EFMinimalApi.Models.Task task, [FromRoute] Guid Id) => {

    try
    {
        var originalTask = dbContext.Tasks.Find(Id);

        if(originalTask != null)
        {
            originalTask.Title = task.Title;
            originalTask.Description = task.Description;
            originalTask.CategoryId = task.CategoryId;

            await dbContext.SaveChangesAsync();
            return Results.Ok(task);
        } 
        else
        {
            return Results.Ok("No existe la tarea ...");
        }
        
    }
    catch (Exception err)
    {
        throw new Exception("Task not created. Error: " + err);
    }

});

app.MapDelete("/api/DeleteTask/{Id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid Id) => {

    try
    {
        var task = dbContext.Tasks.Find(Id);

        if (task != null)
        {
            dbContext.Remove(task);
            await dbContext.SaveChangesAsync();
            return Results.Ok("Deleted!");
        }
        else
        {
            return Results.Ok("No existe la tarea ...");
        }

    }
    catch (Exception err)
    {
        throw new Exception("Task not created. Error: " + err);
    }

});


app.Run();
