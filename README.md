# Incident Management API

## Description
This project is a RESTful API for managing incidents. It allows users to create, track, update and filter incidents by status and severity, and stores them in a SQL Server database.

It was developed for a Cloud & DevOps course and covers the full DevOps loop: automated testing, containerization with Docker, and a CI/CD pipeline that builds, tests and publishes the Docker image.

## Features
- Create, view, update and delete incidents
- Update the status of an incident
- Filter incidents by status
- Filter incidents by severity
- Interactive API documentation with Swagger UI
- Unit and integration tests
- Automated CI/CD pipeline

## Technologies Used

### Backend
- C#
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (with migrations)
- Swagger / Swashbuckle

### Database
- SQL Server

### Testing
- .NET test project with Unit and Integration categories

### DevOps / Deployment
- Docker
- GitHub Actions
- Docker Hub

## Architecture
The application follows a layered REST API architecture:
- Controllers: expose the HTTP endpoints (`IncidentController` and `IncidentsDbController`)
- Models: the `Incident` entity and the `IncidentsDbContext` (Entity Framework Core)
- Migrations: versioned database schema
- Database: SQL Server

The `Incident` entity has the following fields: `id`, `title`, `description`, `status`, `severity` and `createdAt`.

## API Endpoints
The full documentation is available in Swagger UI at `/swagger`.

- `POST /api/Incident/create-incident`: create an incident
- `GET /api/Incident/get-all`: list all incidents
- `GET /api/Incident/getbyid/{id}`: get an incident by id
- `PUT /api/Incident/update-status/{id}`: update the status of an incident
- `DELETE /api/Incident/delete-incident/{id}`: delete an incident
- `GET /api/Incident/filter-by-status/{status}`: filter incidents by status
- `GET /api/Incident/filter-by-severity/{severity}`: filter incidents by severity

The `IncidentsDb` controller also exposes database-backed endpoints to get, update and delete an incident by id, and to filter incidents by status or severity.

## Installation

### Prerequisites
- .NET 8 SDK
- A SQL Server instance
- Docker (optional)

### Clone the repository
```bash
git clone https://github.com/mounaglila/Incident-Api.git
cd Incident-Api
```

### Configure the database
The API reads its connection string from the key `IncidentsConnection`. Set it in `Incident-api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "IncidentsConnection": "Server=localhost,1433;Database=IncidentsDb;User Id=sa;Password=<your-password>;TrustServerCertificate=True"
  }
}
```

Never commit real passwords.

### Run locally
```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project Incident-api
dotnet run --project Incident-api
```

Then open `/swagger` on the URL printed in the console (for example `http://localhost:5154/swagger`).

### Run with Docker
```bash
docker build -t incident-api .
docker run -p 8080:80 \
  -e "ConnectionStrings__IncidentsConnection=Server=host.docker.internal,1433;Database=IncidentsDb;User Id=sa;Password=<your-password>;TrustServerCertificate=True" \
  incident-api
```

Then open `http://localhost:8080/swagger`. The container listens on port 80. The endpoints that read or write incidents need a reachable SQL Server with the migrations applied.

### Run the tests
```bash
dotnet test
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"
```

## CI/CD Pipeline
Two GitHub Actions workflows run on every push:
- `tests-ci.yml`: restores, builds and tests the solution
- `tests-2jobs.yml`: runs the unit tests and the integration tests as separate jobs, then the `deploy` job builds the Docker image and pushes it to Docker Hub (`mounaglila/incidents-api`) only if both test jobs pass

```
git push ─┬─> unit-tests ───────┐
          │                     ├─> deploy (build and push Docker image)
          └─> integration-tests ┘
```

Docker Hub credentials are stored as GitHub Actions secrets, never in the code.

## Author
Mouna Glila, Master's student in Software Engineering at ISIMM (Tunisia)
