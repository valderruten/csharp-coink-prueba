using CsharpCoink.Api.Models;

namespace CsharpCoink.Api.Repositories;

public interface IUsuarioRepository
{
    Task<int> RegistrarUsuarioAsync(UsuarioRequest req);
    Task<IEnumerable<object>> ObtenerUsuariosAsync();
}
