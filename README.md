# core-api

## Requisitos Previos

- **SDK de .NET**: .NET 10.0
- **PostgreSQL**: Versión 16

### Backend

#### 1. Configurar la Conexión a la Base de Datos

El archivo `TestDevCore/TestDevCore.Api/appsettings.json` contiene la configuración de la DB,
reempleza con los datos correspondientes.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=sol_db;Username=postgres;Password=root"
  }
}
```

#### 3. Aplicar Migraciones y Semilla de Datos

Navega a la carpeta del backend:

```bash
cd TestDevCore
```

Restaura las dependencias de NuGet:

```bash
dotnet restore
```

Aplica las migraciones a la base de datos:

```bash
# Desde la carpeta TestDevCore
dotnet ef database update --project ./TestDevCore.Infrastructure/TestDevCore.Infrastructure.csproj --startup-project ./TestDevCore.Api/TestDevCore.Api.csproj --context DomainDBContext
```

Importa los datos iniciales ejecutando el script `seed.sql`,
desde pgAdmin o el editor a eleccion, abre el archivo `seed.sql` y ejecútalo,
este se encuentra en la raiz de `TestDevCore`.

### Usando Docker

#### 1. Crear .env

Crea un .env con las variables detalladas en el .env.example.
```json
DB_USER=
DB_PASSWORD=
DB_NAME=
```

#### 2. Correr los servicios

Corre el seguiente comando en la raiz del proyecto para levantar el contenendor de la API y base de datos:

```bash
docker compose up -d --build  
```

### Decisiones técnicas

Diseño Guiado por el Dominio (DDD): Se opto por la aplicacion de DDD debido a que los requerimientos de la prueba involucraban reglas de negocio estrictas para cada modelo.

Estrategia de resiliencia de datos: Con el objetivo de garantizar la disponibilidad del sistema, se implemento un mecanismo de contingencia. Cada vez que se consulta el tipo de cambio al proveedor externo, los datos no solo se almacenan en cache, sino que tambien se persisten en una tabla llamada ExchangeRate. Esta tabla sirve como ultimo recurso, si los datos de la caché se invalidan y el proveedor externo se encuentra caido, el sistema utilizara el ultimo tipo de cambio registrado con exito.

### API publica

http://206.189.239.30:5000/swagger/index.html

#### API Pública (Swagger)
La documentación interactiva de la API está disponible en el siguiente enlace:
* **Swagger UI:** http://206.189.239.30:5000/swagger/index.html

#### Conexión a la Base de Datos
Para conectarse a la base de datos de manera externa, utilice los siguientes parámetros:

| Parámetro | Valor |
| :--- | :--- |
| **Host** | `206.189.239.30` |
| **Port** | `5442` |
| **User** | `postgres` |
| **Password** | `root` |