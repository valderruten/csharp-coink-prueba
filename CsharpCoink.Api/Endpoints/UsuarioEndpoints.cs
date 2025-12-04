using CsharpCoink.Api.Models;
using CsharpCoink.Api.Repositories;
using CsharpCoink.Api.Validators;
using Npgsql;

namespace CsharpCoink.Api.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this WebApplication app)
    {
        app.MapPost("/usuarios", RegistrarUsuario);
        app.MapGet("/usuarios", ObtenerUsuarios);
    }

    private static async Task<IResult> RegistrarUsuario(UsuarioRequest req, IUsuarioRepository repo)
    {
        var error = UsuarioValidator.Validar(req);
        if (error != null)
            return Results.BadRequest(new { Error = error });

        try
        {
            var nuevoId = await repo.RegistrarUsuarioAsync(req);

            if (nuevoId <= 0)
                return Results.BadRequest(new { Error = "No fue posible registrar el usuario." });

            return Results.Ok(new
            {
                Id = nuevoId,
                Mensaje = "Usuario registrado exitosamente."
            });
        }
        catch (PostgresException pgEx)
        {
            return Results.BadRequest(new { Error = pgEx.MessageText });
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, title: "Error interno del servidor");
        }
    }

    private static async Task<IResult> ObtenerUsuarios(IUsuarioRepository repo)
    {
        var usuarios = await repo.ObtenerUsuariosAsync();
        return Results.Ok(usuarios);
    }
}
