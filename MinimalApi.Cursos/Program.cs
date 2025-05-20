using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Cursos.Data;
using MinimalApi.Cursos.Data.Entities;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("PostgreSQL") ?? "";
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOutputCache();

builder.Services.AddCors(c =>
{
    c.AddPolicy("myCors", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<CursosDbContext>(op =>
{
    try
    {
        op.UseNpgsql(connectionString);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("No hay string de conexion para la app.");
        Console.WriteLine(ex.Message);
        Console.WriteLine("===========================================");
        throw;
    }
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


//**API para Cursos
//Get All

app.MapGet("/cursos", async (CursosDbContext dbContext) =>
{
    var cursos = await dbContext.Cursos.ToListAsync();
    if (cursos.Any())
    {
        return Results.Ok(new
        {
            message = "Listado de cursos",
            data = cursos
        });
    }
    return Results.Ok(new
    {
        message = "No hay resultados",
    });
});

//*Get By Id

app.MapGet("/cursos/{id}", async (CursosDbContext dbContext, string id) =>
{

    try
    {
        if (!Guid.TryParse(id, out Guid validGuidId))
        {
            throw new InvalidCastException("NO se pudo realizar la conversion del id");
        }
        var curso = await dbContext.Cursos.FindAsync(validGuidId);

        if (curso != null)
        {
            return Results.Ok(new
            {
                message = "Detalles del curso",
                data = curso
            });
        }
        return Results.NotFound(new
        {
            message = "No hay curso disponible"
        });
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(new
        {
            message = "No se pudo realizar la solicitud",
            error = ex.Message.ToString()
        });
        throw;
    }
});

app.MapPost("/cursos", async (CursosDbContext dbContext, [FromBody] CursoDto model) =>
{
    if (model is null)
    {
        return Results.BadRequest(new
        {
            message = "Los campos no son correctos"
        });
    }
    var newCurso = new Curso
    {
        Name = model.Name,
        Description = model.Description,
        Instructor = model.Instructor,
        Price = model.Price,
        Technologies = model.Technologies,
    };

    try
    {
        await dbContext.Cursos.AddAsync(newCurso);
        await dbContext.SaveChangesAsync();

        return Results.Ok(new
        {
            message = "Datos guardados correctamente"
        });
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(new
        {
            error = ex.Message,
            message = "NO se pudo realizar la solicitud"
        });
        throw;
    }
});

app.MapDelete("/cursos/{id}", async (CursosDbContext dbContext, string id) =>
{
    try
    {
        if (!Guid.TryParse(id, out Guid newId))
        {
            throw new InvalidCastException("NO se pudo realizar la conversion del id");
        }
        var curso = await dbContext.Cursos.FindAsync(newId);
        if (curso != null)
        {
            dbContext.Cursos.Remove(curso);
            await dbContext.SaveChangesAsync();
            return Results.Ok(new
            {
                message = "Registro eliminado"
            });
        }
        return Results.BadRequest(new
        {
            message = "NO se pudo eliminar el curso"
        });
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(new
        {
            error = ex.Message,
            message = "NO se pudo realizar la solicitud"
        });
        throw;
    }
});


app.MapPatch("/cursos/{id}", async (CursosDbContext context, [FromBody] CursoDto model, string id) =>
{
    if (!Guid.TryParse(id, out Guid newId))
    {
        throw new InvalidCastException("NO se pudo realizar la conversion");
    }
    try
    {
        var curso = await context.Cursos.FindAsync(newId);

        if (curso == null)
        {
            return Results.NotFound(new
            {
                message = "No se encontró el curso"
            });
        }
        curso.Name = model.Name;
        curso.Description = model.Description;
        curso.Instructor = model.Instructor;
        curso.Price = model.Price;
        curso.Technologies = model.Technologies;

        await context.SaveChangesAsync();
        return Results.Ok(new
        {
            message = "Registro actualizado"
        });
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(new
        {
            message = "No se pudo editar el curso",
            error = ex.Message
        });
        throw;
    }
});

app.UseOutputCache();
app.UseResponseCaching();

app.UseCors("myCors");
app.UseHttpsRedirection();


app.Run();
