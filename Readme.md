CsharpCoink – Prueba Técnica Backend (.NET / PostgreSQL)

Este proyecto se trata de una pequeña API para .NET 9 a la que se le ha hecho uso del Minimal API y de PostgreSQL como BD, donde hay que registrar un usuario junto a su información personal y la ubicación a la que pertenece (país, departamento, municipio), cumpliendo así con las reglas de validación y relación que trata la prueba técnica.

1. Tecnologías utilizadas

.NET 9 (Minimal API)

PostgreSQL 16

PL/pgSQL (para la función de inserción)

Npgsql (driver oficial para PostgreSQL)

Swagger / OpenAPI

El proyecto no es complejo, es sencillo y directo, a la par con que está organizado de forma que resulte fácil de revisar y de ejecutar.

2. Estructura del proyecto
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


Los folders están separados de acuerdo con la responsabilidad que tiene cada uno para que el código resulte simple de entender y de modificar.

3. Ajuste de Enlace

Modifica el enlace con PostgreSQL dentro del fichero appsettings.json:

{
  "ConnectionStrings": {
    "PostgresConnection": "Host=localhost;Port=5432;Database=csharp_coink;Username=postgres;Password=EL_PASSWORD"
  }
}

4. Ficheros SQL Incluidos

En el directorio /sql hallarás los comandos requeridos para establecer la base de datos:

01_create_tables.sql

Genera las tablas:

pais

departamento

municipio

usuario

Incluyendo los vínculos precisos para asegurar la concordancia entre país → departamento → municipio.

02_insert_data.sql

Introduce información de muestra inicial.

03_fn_registrar_usuario.sql

Una rutina PL/pgSQL que:

confirma la existencia de país, departamento y municipio

verifica su correcta correspondencia

añade el nuevo usuario

devuelve el identificador creado

emplea RAISE EXCEPTION para reportar fallos evidentes

5. Pasos para Iniciar el Programa

Primero, haz la base de datos:

CREATE DATABASE csharp_coink WITH ENCODING 'UTF8';


Luego, aplica los tres ficheros SQL en esta secuencia:

01_create_tables.sql

02_insert_data.sql

03_fn_registrar_usuario.sql

Inicia la API con:

dotnet run


Accede a Swagger (se activa sola en el entorno Development):

https://localhost:xxxx/swagger

6. Ejemplo para registrar un usuario
POST /usuarios
{
  "nombre": "Carlos Perez",
  "telefono": "3001234567",
  "direccion": "Tres Jardines, Tulua",
  "paisId": 1,
  "departamentoId": 1,
  "municipioId": 1
}


Respuesta:

{
  "id": 1,
  "mensaje": "Usuario registrado exitosamente."
}

7. Validaciones incluidas

El API se encarga de validar lo siguiente:

Que el nombre tenga al menos 3 caracteres.

Que el teléfono sea numérico y tenga entre 7 y 15 dígitos.

Que la dirección tenga mínimo 5 caracteres.

Que los IDs sean mayores a cero.

Por su parte, la base de datos comprueba:

Que el departamento realmente pertenezca al país indicado.

Que el municipio esté dentro del departamento correspondiente.

Estas reglas están distribuidas entre el API y una función en PostgreSQL para garantizar que la información sea válida antes de guardarse.

8. Manejo de errores

Cuando hay errores por relaciones mal definidas, se devuelve un mensaje parecido a este:

{
  "error": "El departamento 10 no pertenece al pais 1"
}


Un error inesperado devuelve:

{
  "title": "Error interno del servidor",
  "detail": "Mensaje técnico..."
}

9. Decisiones técnicas

Minimal API: Se optó por este enfoque porque permite una estructura limpia y directa, ideal para este tipo de pruebas técnicas.

Uso de funciones en PostgreSQL: Ayuda a retornar directamente el ID del usuario y facilita el manejo de transacciones de forma más fluida.

Patrón "Repository": Se implementó para mantener la lógica de acceso a datos separada de los endpoints, lo que mejora la organización del código.

Sin Entity Framework: Tal como lo indica la prueba, todas las operaciones se manejan mediante stored procedures o funciones, sin uso de ORMs.

10. Estado actual del proyecto

Actualmente, el sistema cumple con lo siguiente:

✔ Modelo relacional completo
✔ Función en PL/pgSQL para insertar datos con validaciones
✔ API con validaciones, inyección de dependencias y estructura por capas
✔ Manejo adecuado de errores
✔ Proyecto listo para ejecutarse y ser revisado