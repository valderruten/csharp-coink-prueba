# CsharpCoink – Prueba Técnica Backend (.NET / PostgreSQL)


Este proyecto es una pequeña API en .NET 9 (Minimal API) que utiliza PostgreSQL como base de datos. Permite registrar un usuario con su información personal y la ubicación a la que pertenece (país, departamento, municipio), cumpliendo las reglas de validación y relaciones definidas.

## 1. Tecnologías utilizadas

- .NET 9 (Minimal API)
- PostgreSQL 16
- PL/pgSQL (para la función de inserción)
- Npgsql (driver oficial para PostgreSQL)
- Swagger / OpenAPI

## 2. Estructura del proyecto

Estructura principal dentro de `CsharpCoink.Api`:

```
CsharpCoink.Api/
│
├── Endpoints/
├── Models/
├── Repositories/
├── Validators/
├── sql/
│     ├─ 01_create_tables.sql
│     ├─ 02_insert_data.sql
│     └── 03_fn_registrar_usuario.sql
│
├── Program.cs
├── appsettings.json
└── README.md
```

Los folders están separados según la responsabilidad para mantener el código simple de entender y modificar.

## 3. Ajuste de conexión

Modifica la cadena de conexión a PostgreSQL dentro del fichero `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "PostgresConnection": "Host=localhost;Port=5432;Database=csharp_coink;Username=postgres;Password=EL_PASSWORD"
  }
}
```

Reemplaza `EL_PASSWORD` por la contraseña de tu usuario `postgres`.

## 4. Ficheros SQL incluidos

En el directorio `sql/` encontrarás los scripts para preparar la base de datos:

- `01_create_tables.sql`  
  Genera las tablas: `pais`, `departamento`, `municipio`, `usuario` e incluye las relaciones necesarias (pais → departamento → municipio).

- `02_insert_data.sql`  
  Inserta datos de ejemplo iniciales.

- `03_fn_registrar_usuario.sql`  
  Rutina PL/pgSQL que:
  - Confirma la existencia de país, departamento y municipio.
  - Verifica la correspondencia correcta entre ellos.
  - Inserta el nuevo usuario y devuelve el ID creado.
  - Emplea `RAISE EXCEPTION` para reportar fallos evidentes.

## 5. Pasos para iniciar el programa

1. Crear la base de datos (ejecutar en psql o en una consola con privilegios):

```sql
CREATE DATABASE csharp_coink WITH ENCODING 'UTF8';
```

2. Aplicar los scripts SQL en el siguiente orden:
   - `01_create_tables.sql`
   - `02_insert_data.sql`
   - `03_fn_registrar_usuario.sql`

3. Configurar la cadena de conexión en `appsettings.json`.

4. Ejecutar la API:

```powershell
dotnet run
```

Swagger (OpenAPI) se activa automáticamente en el entorno `Development` y quedará disponible en:
`https://localhost:xxxx/swagger`

## 6. Ejemplo para registrar un usuario

POST `/usuarios`

Request (JSON):

```json
{
  "nombre": "Carlos Perez",
  "telefono": "3001234567",
  "direccion": "Tres Jardines, Tulua",
  "paisId": 1,
  "departamentoId": 1,
  "municipioId": 1
}
```

Respuesta exitosa:

```json
{
  "id": 1,
  "mensaje": "Usuario registrado exitosamente."
}
```

## 7. Validaciones incluidas

Validaciones realizadas por la API:

- `nombre`: mínimo 3 caracteres.
- `telefono`: solo numérico, entre 7 y 15 dígitos.
- `direccion`: mínimo 5 caracteres.
- `paisId`, `departamentoId`, `municipioId`: deben ser mayores que cero.

Validaciones realizadas por la base de datos (función PL/pgSQL):

- El `departamento` pertenece al `pais` indicado.
- El `municipio` pertenece al `departamento` indicado.

Estas reglas se distribuyen entre la API y la función en PostgreSQL para garantizar integridad antes del guardado.

## 8. Manejo de errores

Errores por relaciones mal definidas (ejemplo):

```json
{
  "error": "El departamento 10 no pertenece al pais 1"
}
```

Errores internos inesperados (ejemplo):

```json
{
  "title": "Error interno del servidor",
  "detail": "Mensaje técnico..."
}
```

## 9. Decisiones técnicas

- Minimal API: elección para una estructura limpia y directa.
- Uso de funciones en PostgreSQL: facilita retorno del ID y manejo de transacciones.
- Patrón Repository: separa la lógica de acceso a datos de los endpoints.
- Sin Entity Framework: todas las operaciones se manejan mediante stored procedures/funciones (sin ORM), tal como indica la prueba.

## 10. Estado actual del proyecto

- ✔ Modelo relacional completo
- ✔ Función en PL/pgSQL para insertar datos con validaciones
- ✔ API con validaciones, inyección de dependencias y estructura por capas
- ✔ Manejo adecuado de errores
- ✔ Proyecto listo para ejecutarse y ser revisado

---

Si quieres, puedo:
- Reemplazar el archivo `Readme.md` en el repositorio con este contenido.
- Generar una versión en inglés.

He reemplazado el contenido en español. ¿Deseas que haga el commit y push de este cambio?
