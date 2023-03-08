using Backend;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BackendDataContext>(options => 
    options.UseSqlServer("Server=localhost;User ID=sa;Password=sa;database=Ejercicio;TrustServerCertificate=true;")
    );



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

async Task<List<Metas>> GetAllMetas(BackendDataContext context) => await context.dsMetas.ToListAsync();
async Task<List<Tareas>> GetTareasByMetaId(BackendDataContext context, int id) => await context.dsTareas.ToListAsync();

app.MapGet("/metas", async (BackendDataContext context) => 
    await context.dsMetas.ToListAsync());
app.MapGet("/metas/{id}", async (BackendDataContext context, int id) =>
    await context.dsMetas.FindAsync(id) is Metas meta ?
        Results.Ok(meta) :
        Results.NotFound("Not found"));
app.MapPost("/metas/create", async(BackendDataContext context, Metas meta) =>
{
    meta.FechaCreacion = DateTime.Now;
    context.dsMetas.Add(meta);
    await context.SaveChangesAsync();
    return Results.Ok(GetAllMetas(context));
});

app.MapPut("/metas/update/{id}", async(BackendDataContext context, Metas meta, int id) =>
{
    var dbmeta = await context.dsMetas.FindAsync(id);
    if(dbmeta == null) return Results.NotFound("Not found");
    
    dbmeta.Nombre = meta.Nombre;
    dbmeta.PorcentajeTarea = meta.PorcentajeTarea;
    dbmeta.TotalTareas = meta.TotalTareas;

    await context.SaveChangesAsync();
    return Results.Ok(GetAllMetas(context));
});

app.MapDelete("/metas/{id}", async(BackendDataContext context, int id) =>
{
    var dbmeta = await context.dsMetas.FindAsync(id);
    if(dbmeta == null) return Results.NotFound("Not found");
    
    context.dsMetas.Remove(dbmeta);
    await context.SaveChangesAsync();

    return Results.Ok(GetAllMetas(context));
});


app.MapGet("/tareas/{id}", async (BackendDataContext context, int id) =>
    await context.dsTareas.FindAsync(id) is Tareas tarea ?
        Results.Ok(tarea) :
        Results.NotFound("Not found"));
app.MapPost("/tareas", async(BackendDataContext context, Tareas tarea) =>
{
    tarea.FechaCreada = DateTime.Now;
    context.dsTareas.Add(tarea);
    await context.SaveChangesAsync();
    return Results.Ok(GetTareasByMetaId(context, tarea.MetaId));
});

app.MapPut("/tareas/{id}", async(BackendDataContext context, Tareas tarea, int id) =>
{
    var dbtarea = await context.dsTareas.FindAsync(id);
    if(dbtarea == null) return Results.NotFound("Not found");
    
    dbtarea.Nombre = tarea.Nombre;
    dbtarea.Abierta = tarea.Abierta;

    await context.SaveChangesAsync();
    return Results.Ok(GetTareasByMetaId(context, tarea.MetaId));
});

app.MapDelete("/tareas/{id}", async(BackendDataContext context, int id) =>
{
    var dbtarea = await context.dsTareas.FindAsync(id);
    if(dbtarea == null) return Results.NotFound("Not found");
    
    context.dsTareas.Remove(dbtarea);
    await context.SaveChangesAsync();

    return Results.Ok(GetTareasByMetaId(context, dbtarea.MetaId));
});

app.Run();
