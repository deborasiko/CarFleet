# Car Fleet Management System

A comprehensive ASP.NET Core 8.0 REST API for managing a company's fleet operations, including vehicles, drivers, assignments, fuel tracking, maintenance, trips, expenses, and documents.

## 🎯 Features

### Core Functionality
- **Vehicle Management**: Register, track, and manage fleet vehicles
- **Driver Management**: Manage driver profiles and licenses
- **Vehicle Assignments**: Assign vehicles to drivers with date tracking
- **Fuel Tracking**: Log fuel purchases and track consumption
- **Maintenance**: Record service history and schedule maintenance
- **Trip Logging**: Track vehicle trips with location and distance
- **Expense Management**: Track operational costs
- **Document Management**: Store and track vehicle documents (insurance, registration, etc.)
- **Fleet Locations**: Manage geographic locations for vehicles

### Technical Features
- Clean Architecture with Repository & Service patterns
- Entity Framework Core with PostgreSQL
- AutoMapper for DTO mapping
- Dependency Injection
- Comprehensive API documentation (Swagger)
- Unit tests with Moq
- RESTful API design

## 🏗️ System Architecture

```
CarFleet.Core/
├── Models/           # Database entities
├── DTOs/            # Data transfer objects
├── Repositories/    # Data access layer
├── Services/        # Business logic
├── Validators/      # Input validation
├── Mapping/         # AutoMapper configuration
└── Data/            # DbContext

CarFleet.Api/
├── Controllers/     # HTTP endpoints
└── Program.cs       # Configuration & DI

tests/
└── CarFleet.Tests/  # Unit tests
```

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL 12+
- Visual Studio Code or Visual Studio 2022

### Installation

1. **Clone the repository**
```bash
git clone <repository-url>
cd CarFleetManagement
```

2. **Configure Database Connection**

Edit `src/CarFleet.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=carfleetdb;Username=postgres;Password=postgres"
  }
}
```

3. **Build the Solution**
```bash
dotnet build
```

4. **Apply Migrations**
```bash
cd src/CarFleet.Api
dotnet ef database update
```

5. **Run the Application**
```bash
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger UI at `https://localhost:5001/swagger`.

## 📚 API Documentation

### Base URL
```
https://localhost:5001/api
```

### Endpoints Overview

#### Vehicles (`/vehicles`)
- `GET /` - Get all vehicles
- `GET /{id}` - Get vehicle by ID
- `GET /search?make=...&year=...` - Search vehicles
- `POST /` - Create vehicle
- `PUT /{id}` - Update vehicle
- `DELETE /{id}` - Delete vehicle

#### Drivers (`/drivers`)
- `GET /` - Get all drivers
- `GET /{id}` - Get driver by ID
- `GET /search?searchTerm=...` - Search drivers
- `GET /expired-licenses` - Get drivers with expired licenses
- `POST /` - Create driver
- `PUT /{id}` - Update driver
- `DELETE /{id}` - Delete driver

#### Vehicle Assignments (`/vehicle-assignments`)
- `GET /` - Get all assignments
- `GET /active` - Get active assignments
- `GET /vehicle/{vehicleId}/active` - Get current assignment
- `GET /vehicle/{vehicleId}/history` - Get assignment history
- `POST /` - Create assignment
- `POST /{id}/end` - End assignment

#### Fuel Logs (`/fuel-logs`)
- `GET /vehicle/{vehicleId}` - Get fuel logs for vehicle
- `GET /vehicle/{vehicleId}/cost?startDate=...&endDate=...` - Get total fuel cost  
- `GET /vehicle/{vehicleId}/average-consumption` - Get fuel efficiency
- `POST /` - Create fuel log

#### Service Records (`/service-records`)
- `GET /vehicle/{vehicleId}` - Get service records
- `GET /overdue` - Get overdue maintenance
- `GET /vehicle/{vehicleId}/total-cost` - Get total maintenance cost
- `POST /` - Create service record

#### Trips (`/trips`)
- `GET /vehicle/{vehicleId}` - Get trips for vehicle
- `GET /driver/{driverId}` - Get trips for driver
- `GET /vehicle/{vehicleId}/distance?startDate=...&endDate=...` - Get total distance
- `POST /` - Create trip
- `POST /{id}/end` - End trip

#### Expenses (`/expenses`)
- `GET /vehicle/{vehicleId}` - Get expenses for vehicle
- `GET /vehicle/{vehicleId}/total?startDate=...&endDate=...` - Get total expenses
- `POST /` - Create expense

#### Documents (`/documents`)
- `GET /vehicle/{vehicleId}` - Get documents for vehicle
- `GET /expiring?daysUntilExpiry=30` - Get expiring documents
- `POST /` - Create document

#### Fleet Locations (`/fleet-locations`)
- `GET /` - Get all locations
- `GET /active` - Get active locations
- `POST /` - Create location

See [API_DOCUMENTATION.md](API_DOCUMENTATION.md) for detailed endpoint specifications and request/response examples.

## 🧪 Testing

### Run All Tests
```bash
dotnet test
```

### Run Specific Test File
```bash
dotnet test tests/CarFleet.Tests/ServiceTests.cs
```

## 🔄 Dependency Injection

Services are automatically registered in `Program.cs`:

```csharp
// Repositories registered as Scoped
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
// ... more repositories

// Services registered as Scoped
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IDriverService, DriverService>();
// ... more services

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));
```

## 📦 Project Structure

```
CarFleetManagement/
├── src/
│   ├── CarFleet.Core/
│   │   ├── Models/           (10 entity classes)
│   │   ├── DTOs/            (10 DTO sets)
│   │   ├── Repositories/     (10 repositories)
│   │   ├── Services/         (10 services)
│   │   ├── Mapping/          (AutoMapper profile)
│   │   ├── Data/             (DbContext)
│   │   └── Migrations/       (EF migrations)
│   │
│   └── CarFleet.Api/
│       ├── Controllers/      (9 controllers)
│       ├── appsettings.json  (Configuration)
│       └── Program.cs        (Startup & DI)
│
├── tests/
│   └── CarFleet.Tests/
│       ├── ServiceTests.cs   (Service unit tests)
│       └── VehicleTests.cs   (Model tests)
│
├── API_DOCUMENTATION.md  (Detailed API docs)
└── README.md            (This file)
```

## 🛠️ Technologies Used

- **ASP.NET Core 8.0** - Web framework
- **Entity Framework Core 8.0** - ORM
- **PostgreSQL** - Database
- **AutoMapper 12.0.1** - Object mapping
- **xUnit** - Unit testing framework
- **Moq** - Mocking library
- **Swagger/OpenAPI** - API documentation

## 🎓 Design Patterns Used

1. **Repository Pattern** - Data access abstraction
2. **Service Layer Pattern** - Business logic separation
3. **Dependency Injection** - Loose coupling
4. **DTO Pattern** - Data transformation
5. **AutoMapper** - Object mapping
6. **Generic Repository** - Code reusability
7. **Clean Architecture** - Separation of concerns

## 📈 Completed Implementation

✅ **Data Models**: 10 entity classes with proper relationships  
✅ **Database**: PostgreSQL DbContext with migrations  
✅ **Repositories**: 1 generic + 10 specialized repositories  
✅ **Services**: 10 business logic services  
✅ **DTOs**: 30 data transfer objects (Create/Update/Response)  
✅ **Controllers**: 9 API controllers with CRUD endpoints  
✅ **Mapping**: AutoMapper configuration for all entities  
✅ **Tests**: Unit tests for services with Moq mocking  
✅ **Documentation**: Comprehensive API documentation  

## 📈 Future Enhancements

### Phase 2: Authentication & Authorization
- JWT token implementation
- Role-based access control
- User login/registration
- Token refresh mechanism

### Phase 3: Reporting & Analytics
- Fuel consumption reports
- Maintenance cost analysis
- Vehicle utilization metrics
- Driver performance metrics

### Phase 4: Notifications
- Email notifications
- License expiration alerts
- Document expiration alerts
- Background job scheduler

### Phase 5: Advanced Features
- Document file upload
- Spatial queries for locations
- Trip cost calculations
- Bulk import/export functionality

## 🤝 Contributing

1. Follow Clean Code principles
2. Write unit tests for new features
3. Update API documentation
4. Use meaningful commit messages
5. Keep code maintainable and scalable

## 📄 License

Internal Use Only

## 📞 Support

For issues or questions, please contact the development team.

---

**Created on**: March 10, 2026  
**Framework**: ASP.NET Core 8.0  
**Database**: PostgreSQL  
**Status**: Core Implementation Complete ✅
