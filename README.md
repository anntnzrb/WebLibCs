# WebLibCs

Sistema web para gestión de biblioteca con ASP.NET Core 8 y Entity Framework Core.

### 1. Restaurar dependencias & compilar
```bash
dotnet build WebLibCs.sln
```

### 2. Aplicar migraciones
```bash
dotnet ef database update --project WebLibCs.Infrastructure --startup-project WebLibCs.Web
```

### 3. Ejecutar la aplicación
```bash
dotnet run --project WebLibCs.Web
```

La aplicación estará disponible en `https://localhost:5109`

## Estructura del Proyecto

- **WebLibCs.Core**: Entidades, DTOs, interfaces y lógica de negocio
- **WebLibCs.Infrastructure**: Acceso a datos, Entity Framework y servicios externos
- **WebLibCs.Web**: Interfaz web con ASP.NET Core MVC
