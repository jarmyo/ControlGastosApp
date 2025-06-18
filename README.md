# ControlGastosApp

**Aplicación web en ASP NET Core 8 + Razor Pages para gestionar gastos e ingresos recurrentes.**

## Tecnologías

- .NET 8 (ASP NET Core)
- Razor Pages
- Entity Framework Core + SQLite
- Bootstrap 5
- xUnit + EF InMemory + WebApplicationFactory para pruebas

## Estructura de la solución

```
ControlGastosApp/
├── src/
│ ├── ControlGastos.Domain/ ← Entidades, Value Objects, Interfaces
│ ├── ControlGastos.Application/ ← Lógica de aplicación (Commands/Queries, DTOs)
│ ├── ControlGastos.Infrastructure/ ← DbContext, Repositorios, Servicios, Migraciones
│ └── ControlGastos.WebUI/ ← Razor Pages, configuración, Program.cs
└── tests/
├── ControlGastos.Infrastructure.Tests
└── ControlGastos.WebUI.Tests
```

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- (Opcional) Visual Studio 2022 / VS Code

## Configuración y arranque

1. **Clonar el repositorio**  
```
git clone <repo-url> ControlGastosApp
cd ControlGastosApp
```

2. Aplicar migraciones y crear la base de datos
La primera vez (y en cada arranque), el sistema aplica automáticamente las migraciones gracias a db.Database.Migrate() en Program.cs.

3. Ejecutar la aplicación

```
dotnet run --project src/ControlGastos.WebUI
```

4. Luego abre en el navegador:

```
https://localhost:5001/
```

## Ejecutar pruebas

```
dotnet test
```

## Uso
Al entrar a / serás redirigido al Dashboard.

Gastos: CRUD de gastos fijos y aproximados.

Ingresos: CRUD de ingresos fijos.

Pagos: Listado de pagos pendientes y marcado de pagos.

Recordatorios: Cada día a la hora configurada escribe en log los pendientes.

## Personalización
Cadenas de conexión en src/ControlGastos.WebUI/appsettings.json.

Hora de recordatorio bajo "Reminder": { "Hour": 8, "Minute": 0 }.

Colores y tipografía en wwwroot/css/site.css.