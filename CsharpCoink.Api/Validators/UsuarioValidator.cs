using CsharpCoink.Api.Models;

namespace CsharpCoink.Api.Validators;

public static class UsuarioValidator
{
    public static string? Validar(UsuarioRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Nombre) || req.Nombre.Length < 3)
            return "El nombre es obligatorio y debe tener al menos 3 caracteres.";

        if (string.IsNullOrWhiteSpace(req.Telefono))
            return "El teléfono es obligatorio.";

        if (!req.Telefono.All(char.IsDigit))
            return "El teléfono debe contener solo números.";

        if (req.Telefono.Length < 7 || req.Telefono.Length > 15)
            return "El teléfono debe tener entre 7 y 15 dígitos.";

        if (string.IsNullOrWhiteSpace(req.Direccion) || req.Direccion.Length < 5)
            return "La dirección es obligatoria y debe tener al menos 5 caracteres.";

        if (req.PaisId <= 0 || req.DepartamentoId <= 0 || req.MunicipioId <= 0)
            return "Los IDs país, departamento y municipio deben ser mayores a 0.";

        return null;
    }
}
