using CsharpCoink.Api.Models;
using Npgsql;

namespace CsharpCoink.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IConfiguration _config;

    public UsuarioRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<int> RegistrarUsuarioAsync(UsuarioRequest req)
    {
        var connString = _config.GetConnectionString("PostgresConnection");

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(
            "SELECT fn_registrar_usuario(@nombre, @telefono, @direccion, @pais, @dep, @mpio);",
            conn
        );

        cmd.Parameters.AddWithValue("@nombre", req.Nombre);
        cmd.Parameters.AddWithValue("@telefono", req.Telefono);
        cmd.Parameters.AddWithValue("@direccion", req.Direccion);
        cmd.Parameters.AddWithValue("@pais", req.PaisId);
        cmd.Parameters.AddWithValue("@dep", req.DepartamentoId);
        cmd.Parameters.AddWithValue("@mpio", req.MunicipioId);

        var result = await cmd.ExecuteScalarAsync();
        return result is int id ? id : 0;
    }

    public async Task<IEnumerable<object>> ObtenerUsuariosAsync()
    {
        var lista = new List<object>();

        var connString = _config.GetConnectionString("PostgresConnection");
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(
            "SELECT id, nombre, telefono, direccion FROM usuario",
            conn
        );

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Telefono = reader.GetString(2),
                Direccion = reader.GetString(3)
            });
        }

        return lista;
    }
}
