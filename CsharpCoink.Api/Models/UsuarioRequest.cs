namespace CsharpCoink.Api.Models;

public record UsuarioRequest(
    string Nombre,
    string Telefono,
    string Direccion,
    int PaisId,
    int DepartamentoId,
    int MunicipioId
);
