# Car Fleet Management System - API Documentation & Implementation Guide

## Project Overview

A comprehensive ASP.NET Core 8.0 backend for managing vehicle fleet operations. The system tracks vehicles, drivers, assignments, fuel usage, maintenance, trips, expenses, and documents.

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 8.0
- **Dependency Injection**: Microsoft.DependencyInjection
- **Mapping**: AutoMapper
- **Validation**: FluentValidation 
- **Testing**: xUnit
- **API Documentation**: Swagger/OpenAPI

## Architecture

The application follows **Clean Architecture** principles:

```
CarFleet.Core/
├── Models/              # Domain entities
├── DTOs/               # Data Transfer Objects
├── Repositories/       # Data access layer (Repository Pattern)
├── Services/           # Business logic layer
├── Validators/         # Input validation
├── Mapping/            # AutoMapper profiles
└── Data/               # DbContext

CarFleet.Api/
├── Controllers/        # HTTP endpoints
└── Program.cs          # DI & middleware configuration
```

## Database Schema

### Core Entities

**Vehicles**
- Vehicle inventory management
- Status tracking (Active, Inactive, UnderMaintenance, Retired)
- Relationship: Assignments, FuelLogs, ServiceRecords, Trips, Expenses, Documents

**Drivers** 
- Driver management
- License tracking with expiration dates
- Status tracking (Active, Inactive, OnLeave, Retired)
- Relationship: VehicleAssignments, Trips, FuelLogs

**VehicleAssignments**
- Many-to-many relationship between Vehicles and Drivers
- Start/End dates for assignment periods
- Prevents multiple active assignments per vehicle
- Relationship: Vehicles, Drivers

**FuelLogs**
- Fuel purchase tracking
- Cost and consumption calculation
- Odometer reading tracking
- Relationship: Vehicles, Drivers

**ServiceRecords**
- Maintenance and repair tracking
- Service provider information
- Next service due date scheduling
- Relationship: Vehicles

**Trips**
- Trip logging with location and distance tracking
- Duration calculation from start/end times
- Relationship: Vehicles, Drivers

**Expenses**
- Non-fuel expense tracking (insurance, tolls, parking, etc.)
- Categorized by type
- Relationship: Vehicles

**Documents**
- Document metadata storage (insurance, registration, etc.)
- Expiration date tracking
- Relationship: Vehicles

**Users & Roles**
- Authentication support
- Role-based access control (Admin, FleetManager, Driver)
- Relationship: Drivers (optional - drivers can have user accounts)

**FleetLocations**
- Geographic location management for vehicles
- Latitude/Longitude support
- Relationship: Vehicles

## API Endpoints

### Vehicles (`/api/vehicles`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all vehicles |
| GET | `/{id}` | Get vehicle by ID |
| GET | `/search?params` | Search vehicles (make, model, year, status) |
| POST | `/` | Create new vehicle |
| PUT | `/{id}` | Update vehicle |
| DELETE | `/{id}` | Delete vehicle |

**Example: Create Vehicle**
```json
POST /api/vehicles
{
  "make": "Toyota",
  "model": "Corolla",
  "vin": "JTDXX1234567890",
  "year": 2023,
  "licensePlate": "ABC-123",
  "color": "Silver",
  "fuelType": 0,
  "purchaseDate": "2023-01-15",
  "fleetLocationId": 1
}
```

### Drivers (`/api/drivers`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all drivers |
| GET | `/{id}` | Get driver by ID |
| GET | `/search?params` | Search drivers (name, status) |
| GET | `/expired-licenses` | Get drivers with expired licenses |
| POST | `/` | Create new driver |
| PUT | `/{id}` | Update driver |
| DELETE | `/{id}` | Delete driver |

**Example: Create Driver**
```json
POST /api/drivers
{
  "firstName": "John",
  "lastName": "Doe",
  "licenseNumber": "DL123456",
  "licenseExpiryDate": "2026-12-31",
  "phone": "+1-555-0123",
  "email": "john.doe@example.com",
  "hireDate": "2023-06-01"
}
```

### Vehicle Assignments (`/api/vehicle-assignments`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all assignments |
| GET | `/{id}` | Get assignment by ID |
| GET | `/active` | Get active assignments (no end date) |
| GET | `/vehicle/{vehicleId}/active` | Get current assignment for vehicle |
| GET | `/vehicle/{vehicleId}/history` | Get assignment history |
| POST | `/` | Create new assignment |
| PUT | `/{id}` | Update assignment |
| POST | `/{id}/end` | End assignment |
| DELETE | `/{id}` | Delete assignment |

**Example: Create Assignment**
```json
POST /api/vehicle-assignments
{
  "vehicleId": 1,
  "driverId": 1,
  "startDate": "2026-01-01",
  "assignedBy": "admin",
  "notes": "Primary vehicle assignment"
}
```

### Fuel Logs (`/api/fuel-logs`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all fuel logs |
| GET | `/{id}` | Get fuel log by ID |
| GET | `/vehicle/{vehicleId}` | Get logs for vehicle |
| GET | `/vehicle/{vehicleId}/cost?startDate&endDate` | Get total cost |
| GET | `/vehicle/{vehicleId}/average-consumption` | Get avg consumption (km/liter) |
| POST | `/` | Create fuel log |
| PUT | `/{id}` | Update fuel log |
| DELETE | `/{id}` | Delete fuel log |

**Example: Create Fuel Log**
```json
POST /api/fuel-logs
{
  "vehicleId": 1,
  "driverId": 1,
  "fuelDate": "2026-03-10",
  "liters": 45.5,
  "pricePerLiter": 1.50,
  "odometer": 15250,
  "fuelStation": "Shell Station #123"
}
```

### Service Records (`/api/service-records`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all service records |
| GET | `/{id}` | Get record by ID |
| GET | `/vehicle/{vehicleId}` | Get records for vehicle |
| GET | `/overdue` | Get overdue maintenance |
| GET | `/vehicle/{vehicleId}/total-cost` | Get total maintenance cost |
| POST | `/` | Create service record |
| PUT | `/{id}` | Update record |
| DELETE | `/{id}` | Delete record |

**Example: Create Service Record**
```json
POST /api/service-records
{
  "vehicleId": 1,
  "serviceType": 0,
  "description": "Oil change and filter replacement",
  "serviceDate": "2026-03-10",
  "odometer": 15250,
  "cost": 85.00,
  "serviceProvider": "ABC Automotive",
  "nextServiceDue": "2026-06-10"
}
```

### Trips (`/api/trips`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all trips |
| GET | `/{id}` | Get trip by ID |
| GET | `/vehicle/{vehicleId}` | Get trips for vehicle |
| GET | `/driver/{driverId}` | Get trips for driver |
| GET | `/vehicle/{vehicleId}/distance?startDate&endDate` | Get total distance |
| POST | `/` | Create trip |
| PUT | `/{id}` | Update trip |
| POST | `/{id}/end` | End trip |
| DELETE | `/{id}` | Delete trip |

**Example: Create Trip**
```json
POST /api/trips
{
  "vehicleId": 1,
  "driverId": 1,
  "startTime": "2026-03-10T08:00:00Z",
  "startLocation": "Office Downtown",
  "endLocation": "Client Site A",
  "purpose": "Client meeting"
}
```

### Expenses (`/api/expenses`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all expenses |
| GET | `/{id}` | Get expense by ID |
| GET | `/vehicle/{vehicleId}` | Get expenses for vehicle |
| GET | `/vehicle/{vehicleId}/total?startDate&endDate` | Get total expenses |
| POST | `/` | Create expense |
| PUT | `/{id}` | Update expense |
| DELETE | `/{id}` | Delete expense |

**Example: Create Expense**
```json
POST /api/expenses
{
  "vehicleId": 1,
  "expenseType": 0,
  "amount": 250.00,
  "expenseDate": "2026-03-10",
  "vendor": "State Insurance Co",
  "description": "quarterly insurance payment"
}
```

### Documents (`/api/documents`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all documents |
| GET | `/{id}` | Get document by ID |
| GET | `/vehicle/{vehicleId}` | Get docs for vehicle |
| GET | `/expiring?daysUntilExpiry` | Get expiring docs |
| POST | `/` | Create document |
| PUT | `/{id}` | Update document |
| DELETE | `/{id}` | Delete document |

**Example: Create Document**
```json
POST /api/documents
{
  "vehicleId": 1,
  "documentType": 0,
  "filePath": "/documents/insurance_2026.pdf",
  "issueDate": "2025-01-01",
  "expiryDate": "2026-12-31"
}
```

### Fleet Locations (`/api/fleet-locations`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get all locations |
| GET | `/{id}` | Get location by ID |
| GET | `/active` | Get active locations |
| POST | `/` | Create location |
| PUT | `/{id}` | Update location |
| DELETE | `/{id}` | Delete location |

## Running the Application

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL 12+
- Visual Studio Code or Visual Studio 2022

### Setup & Running

1. **Configure Database**
   - Update connection string in `appsettings.json`
   - Default: `Host=localhost;Port=5432;Database=carfleetdb;Username=postgres;Password=postgres`

2. **Create Database & Apply Migrations**
   ```bash
   cd src/CarFleet.Api
   dotnet ef database update
   ```

3. **Run Application**
   ```bash
   dotnet run
   ```

4. **Access Swagger UI**
   - Open: `http://localhost:5000/swagger`

## Data Models Overview

### Status Enums

**VehicleStatus**
- Active (0)
- Inactive (1)
- UnderMaintenance (2)
- Retired (3)

**DriverStatus**
- Active (0)
- Inactive (1)
- OnLeave (2)
- Retired (3)

**FuelType**
- Diesel (0)
- Petrol (1)
- Electric (2)

**ServiceType**
- Maintenance (0)
- Repair (1)
- Inspection (2)
- Tire (3)
- Oil (4)
- Transmission (5)
- Engine (6)
- Other (7)

**ExpenseType**
- Insurance (0)
- Maintenance (1)
- Tolls (2)
- Parking (3)
- Repairs (4)
- Cleaning (5)
- Registration (6)
- Other (7)

**DocumentType**
- Insurance (0)
- Registration (1)
- Inspection (2)
- EmissionTest (3)
- VehicleTitle (4)
- MaintenanceRecord (5)
- Other (6)

**RoleType**
- Admin (0)
- FleetManager (1)
- Driver (2)

## Key Features Implemented

### 1. Vehicle Management
- Full CRUD operations
- Search and filter by make, model, year, status
- Location tracking
- Status management

### 2. Driver Management
- Driver profiles with license tracking
- License expiration alerts
- Status tracking
- Driver-vehicle assignments

### 3. Vehicle Assignments
- Many-to-many assignment tracking
- Prevents duplicate active assignments
- Assignment history
- Start/end date management

### 4. Fuel Tracking
- Fuel purchase logging
- Cost and consumption calculations
- Average fuel efficiency metrics
- Odometer tracking

### 5. Maintenance Management
- Service record tracking
- Service provider management
- Maintenance cost aggregation
- Overdue service alerts

### 6. Trip Tracking
- Start/end location tracking
- Distance and duration calculation
- Driver assignment per trip
- Trip cost potential

### 7. Expense Management
- Categorized expense logging
- Total expense calculations
- Date range queries
- Vendor tracking

### 8. Document Management
- Document metadata storage
- Expiration date tracking
- Document type categorization
- Expiration alerts

### 9. Location Management
- Fleet location tracking
- Geographic coordinates (lat/long)
- Active status management
- Vehicle-location assignments

## Testing

Run unit tests:
```bash
dotnet test
```

## Project Structure

```
CarFleetManagement/
├── src/
│   ├── CarFleet.Core/              # Core business logic
│   │   ├── Models/                 # Entity models
│   │   ├── DTOs/                   # Data transfer objects
│   │   ├── Repositories/           # Data layer
│   │   ├── Services/               # Business logic
│   │   ├── Validators/             # FluentValidation rules
│   │   ├── Mapping/                # AutoMapper profiles
│   │   └── Data/                   # DbContext
│   │
│   └── CarFleet.Api/               # Web API
│       ├── Controllers/            # HTTP endpoints
│       ├── Middleware/             # Custom middleware
│       ├── appsettings.json       # Configuration
│       └── Program.cs              # Startup & DI
│
├── tests/
│   └── CarFleet.Tests/             # Unit tests
│
├── README.md                        # This file
└── CarFleetManagement.sln          # Solution file
```

## Repository Pattern Implementation

The system uses the Repository Pattern with a Generic Repository:

```csharp
// Generic Repository Interface
IRepository<T>
- GetByIdAsync(id)
- GetAllAsync()
- FindAsync(predicate)
- AddAsync(entity)
- Update(entity)
- Remove(entity)
- SaveChangesAsync()

// Entity-Specific Repositories
- IVehicleRepository (search, filtering)
- IDriverRepository (license expiry, search)
- IVehicleAssignmentRepository (active/history)
- IFuelLogRepository (consumption calculations)
- IServiceRecordRepository (overdue tracking)
- ITripRepository (distance calculations)
- And more...
```

## Service Layer

All business logic is encapsulated in services:
- Input validation
- Business rule enforcement
- Data transformation (DTO mapping)
- Repository orchestration

## Dependency Injection

Configured in `Program.cs`:
- Repositories registered as Scoped
- Services registered as Scoped
- AutoMapper configured globally
- DbContext with PostgreSQL provider

## Error Handling

Global error handling with appropriate HTTP status codes:
- 200/201: Success
- 204: No Content (Delete)
- 400: Bad Request (Validation)
- 404: Not Found
- 500: Server Error

## Future Enhancements

### Phase 2 - Authentication & Authorization
- JWT token implementation
- Role-based access control
- User login/registration
- Token refresh mechanism

### Phase 3 - Reporting & Analytics
- Fuel consumption reports
- Maintenance cost analysis
- Vehicle utilization metrics
- Driver performance metrics
- Dashboard endpoints

### Phase 4 - Notifications
- Email notifications for:
  - License expiration
  - Document expiration
  - Maintenance due dates
- Background job scheduler

### Phase 5 - Advanced Features
- Document file upload
- Spatial queries for locations
- Trip cost calculations
- Bulk import/export
- Data archival
- API versioning

## Contributing

1. Follow clean code principles
2. Write unit tests for new features
3. Update documentation
4. Use meaningful commit messages

## License

Internal Use Only

## Support

For issues or questions, contact the development team.
