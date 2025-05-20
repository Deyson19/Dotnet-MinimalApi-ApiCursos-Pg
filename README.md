# Minimal API - .NET 9 y PostgreSQL

## Descripción

Este proyecto es una API minimal desarrollada en .NET 9 utilizando PostgreSQL como base de datos. Proporciona un punto de partida para construir APIs RESTful escalables y eficientes.

## Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Entity Framework Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Configuración

### 1. Configurar la cadena de conexión

Edita el archivo `appsettings.json` con tus credenciales de PostgreSQL:

### 2. Ejecutar migraciones

Para aplicar las migraciones y crear la base de datos:

#### En Visual Studio:

1. Abre la Consola del Administrador de Paquetes
2. Ejecuta:
```
update-database
```

#### En línea de comandos:

```
dotnet ef database update
```

La API estará disponible en:

- [ HTTPS ](https://localhost:7288)
- [ HTTP ](http://localhost:5274)

## Endpoints Disponibles

| Método | Endpoint         | Descripción                |
| ------ | ---------------- | -------------------------- |
| GET    | /api/cursos      | Obtener todos los cursos   |
| GET    | /api/cursos/{id} | Obtener un item específico |
| POST   | /api/cursos      | Crear un nuevo item        |
| PATCH  | /api/cursos/{id} | Actualizar un item         |
| DELETE | /api/cursos/{id} | Eliminar un item           |

## Licencia

Este proyecto está bajo la licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.
