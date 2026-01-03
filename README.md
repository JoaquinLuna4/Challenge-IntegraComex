# Challenge IntegraComex - Gestión de Clientes

Este proyecto es una aplicación web desarrollada en ASP.NET Core MVC como solución al challenge técnico propuesto por IntegraComex. La aplicación permite la gestión completa de una entidad de "Clientes", siguiendo las mejores prácticas de arquitectura de software y proporcionando una experiencia de usuario moderna.

## Características Implementadas

- **Gestión CRUD Completa:**
  - **Crear:** Dar de alta nuevos clientes.
  - **Leer:** Listar clientes y ver sus detalles.
  - **Actualizar:** Editar la información de clientes existentes.
  - **Eliminar:** Borrado lógico de clientes (se marcan como inactivos en lugar de ser eliminados físicamente).

- **Arquitectura Profesional por Capas:**
  - **Capa de `Services`:** Lógica de negocio aislada para una fácil mantención y reutilización.
  - **`API Controller` Dedicado:** Un endpoint de API específico para manejar la validación de CUIT y la consulta a servicios externos, separando las responsabilidades de la UI.
  - **Controladores MVC Limpios:** Controladores enfocados exclusivamente en la gestión de vistas.

- **Validaciones Avanzadas:**
  - **Validación Remota de CUIT Único:** El sistema verifica en tiempo real contra la base de datos si un CUIT ya existe, previniendo duplicados antes de enviar el formulario.
  - **Autocompletado de Razón Social:** Al validar un CUIT, una llamada AJAX a un servicio externo obtiene y autocompleta el campo de Razón Social.
  - **Mejora de UX:** Se utiliza un botón "Validar" explícito para una interacción más intuitiva del usuario.

- **Filtrado de Vistas:**
  - La vista principal permite alternar entre "Clientes Activos" y un "Histórico de Clientes" para visualizar los registros marcados como inactivos.

- **Interfaz de Usuario:**
  - Estilo limpio y responsivo implementado con **Bootstrap**.

## Arquitectura

El proyecto sigue una arquitectura de N-Capas para promover la separación de responsabilidades:

- **`Models`:** Contiene las clases de dominio (ej. `Cliente.cs`) y sus validaciones de datos.
- **`Data`:** Responsable del acceso a la base de datos a través de Entity Framework Core (`ApplicationDbContext`).
- **`Services`:** Contiene la lógica de negocio central (ej. cómo se obtienen, crean o validan los clientes).
- **`Controllers`:** Controladores MVC que gestionan el flujo de la interfaz de usuario, devolviendo vistas.
- **`ApiControllers`:** Controladores de API que exponen endpoints de datos (JSON) para ser consumidos por scripts del lado del cliente (AJAX).
- **`Views`:** Archivos `.cshtml` que componen la interfaz de usuario renderizada.

## Tecnologías Utilizadas

- **Backend:** .NET 6, ASP.NET Core MVC, C#
- **ORM:** Entity Framework Core
- **Base de Datos:** SQL Server (configurado a través de `appsettings.json`)
- **Frontend:** HTML, CSS, JavaScript, Bootstrap 5, jQuery & jQuery Unobtrusive AJAX

## Cómo Ejecutar el Proyecto

### Prerrequisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) o superior.
- Un servidor de base de datos SQL Server (LocalDB, Express, etc.).

### 1. Configuración de la Base de Datos

1.  Abre el archivo `appsettings.json`.
2.  Modifica la `ConnectionString` "DefaultConnection" para que apunte a tu instancia de SQL Server.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=TU_SERVIDOR;Database=ChallengeIntegraDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

### 2. Aplicar Migraciones

Abre una terminal en la raíz del proyecto y ejecuta el siguiente comando para crear la base de datos y la tabla `Clientes`:

```bash
dotnet ef database update
```

*(Si no tienes `dotnet-ef` instalado, puedes instalarlo globalmente con `dotnet tool install --global dotnet-ef`)*

### 3. Ejecutar la Aplicación

Una vez configurada la base de datos, ejecuta la aplicación desde la terminal:

```bash
dotnet run
```

La aplicación estará disponible en la URL que se indique en la terminal (generalmente `https://localhost:7xxx` o `http://localhost:5xxx`).
