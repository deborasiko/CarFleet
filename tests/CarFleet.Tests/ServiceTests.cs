using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using CarFleet.Core.Models;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;
using CarFleet.Core.Repositories;

#nullable enable

namespace CarFleet.Tests;

public class VehicleServiceTests
{
    private readonly Mock<IVehicleRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly VehicleService _service;

    public VehicleServiceTests()
    {
        _mockRepository = new Mock<IVehicleRepository>();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Vehicle, VehicleResponseDto>();
            cfg.CreateMap<VehicleCreateDto, Vehicle>();
            cfg.CreateMap<VehicleUpdateDto, Vehicle>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });
        _mapper = mapperConfig.CreateMapper();
        
        _service = new VehicleService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetVehicleByIdAsync_WithValidId_ReturnsVehicle()
    {
        // Arrange
        var vehicleId = 1;
        var vehicle = new Vehicle 
        { 
            Id = vehicleId, 
            Make = "Toyota", 
            Model = "Corolla",
            Year = 2020,
            Vin = "VIN123",
            LicensePlate = "ABC-123",
            Status = VehicleStatus.Active
        };
        _mockRepository.Setup(r => r.GetByIdAsync(vehicleId))
            .ReturnsAsync(vehicle);

        // Act
        var result = await _service.GetVehicleByIdAsync(vehicleId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vehicleId, result.Id);
        Assert.Equal("Toyota", result.Make);
        _mockRepository.Verify(r => r.GetByIdAsync(vehicleId), Times.Once);
    }

    [Fact]
    public async Task GetVehicleByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var vehicleId = 999;
        _mockRepository.Setup(r => r.GetByIdAsync(vehicleId))
            .ReturnsAsync((Vehicle?)null);

        // Act
        var result = await _service.GetVehicleByIdAsync(vehicleId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateVehicleAsync_WithValidData_CreatesVehicle()
    {
        // Arrange
        var createDto = new VehicleCreateDto
        {
            Make = "Honda",
            Model = "Civic",
            Year = 2023,
            Vin = "VIN456",
            LicensePlate = "XYZ-789",
            Color = "Blue",
            FuelType = FuelType.Petrol,
            PurchaseDate = DateTime.Now
        };

        var vehicleCapture = new Vehicle();
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Vehicle>()))
            .Callback<Vehicle>(v => vehicleCapture = v)
            .Returns(Task.CompletedTask);
        _mockRepository.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateVehicleAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Honda", result.Make);
        Assert.Equal(VehicleStatus.Active, result.Status);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Vehicle>()), Times.Once);
        _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task SearchVehiclesAsync_WithSearchTerm_ReturnsMatchingVehicles()
    {
        // Arrange
        var searchTerm = "Toyota";
        var vehicles = new List<Vehicle>
        {
            new Vehicle { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020, Status = VehicleStatus.Active },
            new Vehicle { Id = 2, Make = "Toyota", Model = "Camry", Year = 2021, Status = VehicleStatus.Active }
        };
        
        _mockRepository.Setup(r => r.SearchVehiclesAsync(searchTerm, null, null, null))
            .ReturnsAsync(vehicles);

        // Act
        var result = await _service.SearchVehiclesAsync(searchTerm, null, null, null);

        // Assert
        Assert.NotNull(result);
        var vehicleList = result.ToList();
        Assert.Equal(2, vehicleList.Count);
        Assert.All(vehicleList, v => Assert.Equal("Toyota", v.Make));
    }

    [Fact]
    public async Task UpdateVehicleAsync_WithValidData_UpdatesVehicle()
    {
        // Arrange
        var vehicleId = 1;
        var existingVehicle = new Vehicle 
        { 
            Id = vehicleId, 
            Make = "Toyota", 
            Status = VehicleStatus.Active 
        };
        var updateDto = new VehicleUpdateDto { Status = VehicleStatus.Inactive };

        _mockRepository.Setup(r => r.GetByIdAsync(vehicleId))
            .ReturnsAsync(existingVehicle);
        _mockRepository.Setup(r => r.Update(It.IsAny<Vehicle>()));
        _mockRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.UpdateVehicleAsync(vehicleId, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(VehicleStatus.Inactive, result.Status);
        _mockRepository.Verify(r => r.Update(It.IsAny<Vehicle>()), Times.Once);
    }

    [Fact]
    public async Task DeleteVehicleAsync_WithValidId_DeletesVehicle()
    {
        // Arrange
        var vehicleId = 1;
        var vehicle = new Vehicle { Id = vehicleId };
        _mockRepository.Setup(r => r.GetByIdAsync(vehicleId))
            .ReturnsAsync(vehicle);
        _mockRepository.Setup(r => r.Remove(vehicle));
        _mockRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.DeleteVehicleAsync(vehicleId);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(r => r.Remove(vehicle), Times.Once);
        _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteVehicleAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var vehicleId = 999;
        _mockRepository.Setup(r => r.GetByIdAsync(vehicleId))
            .ReturnsAsync((Vehicle?)null);

        // Act
        var result = await _service.DeleteVehicleAsync(vehicleId);

        // Assert
        Assert.False(result);
        _mockRepository.Verify(r => r.Remove(It.IsAny<Vehicle>()), Times.Never);
    }
}

public class DriverServiceTests
{
    private readonly Mock<IDriverRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly DriverService _service;

    public DriverServiceTests()
    {
        _mockRepository = new Mock<IDriverRepository>();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Driver, DriverResponseDto>();
            cfg.CreateMap<DriverCreateDto, Driver>();
            cfg.CreateMap<DriverUpdateDto, Driver>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });
        _mapper = mapperConfig.CreateMapper();
        
        _service = new DriverService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task CreateDriverAsync_WithValidData_CreatesDriver()
    {
        // Arrange
        var createDto = new DriverCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            LicenseNumber = "DL123456",
            LicenseExpiryDate = DateTime.Now.AddYears(1),
            Phone = "555-0123",
            Email = "john@example.com",
            HireDate = DateTime.Now
        };

        var driverCapture = new Driver();
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Driver>()))
            .Callback<Driver>(d => driverCapture = d)
            .Returns(Task.CompletedTask);
        _mockRepository.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateDriverAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal(DriverStatus.Active, result.Status);
    }

    [Fact]
    public async Task GetExpiredLicenseDriversAsync_ReturnsExpiredDrivers()
    {
        // Arrange
        var expiredDrivers = new List<Driver>
        {
            new Driver 
            { 
                Id = 1, 
                FirstName = "Jane", 
                LastName = "Smith",
                LicenseExpiryDate = DateTime.Now.AddDays(-30)
            }
        };

        _mockRepository.Setup(r => r.GetExpiredLicenseDriversAsync())
            .ReturnsAsync(expiredDrivers);

        // Act
        var result = await _service.GetExpiredLicenseDriversAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        var driver = result.First();
        Assert.Equal("Jane", driver.FirstName);
        Assert.True(driver.DaysUntilLicenseExpiry < 0);
    }
}

public class FuelLogServiceTests
{
    private readonly Mock<IFuelLogRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly FuelLogService _service;

    public FuelLogServiceTests()
    {
        _mockRepository = new Mock<IFuelLogRepository>();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FuelLog, FuelLogResponseDto>();
            cfg.CreateMap<FuelLogCreateDto, FuelLog>();
            cfg.CreateMap<FuelLogUpdateDto, FuelLog>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });
        _mapper = mapperConfig.CreateMapper();
        
        _service = new FuelLogService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task CreateFuelLogAsync_CalculatesTotalCost()
    {
        // Arrange
        var createDto = new FuelLogCreateDto
        {
            VehicleId = 1,
            DriverId = 1,
            FuelDate = DateTime.Now,
            Liters = 50m,
            PricePerLiter = 1.50m,
            Odometer = 15000,
            FuelStation = "Shell"
        };

        var fuelLogCapture = new FuelLog();
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<FuelLog>()))
            .Callback<FuelLog>(fl => fuelLogCapture = fl)
            .Returns(Task.CompletedTask);
        _mockRepository.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateFuelLogAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(75m, result.TotalCost); // 50 * 1.50
    }

    [Fact]
    public async Task GetTotalFuelCostAsync_ReturnsCorrectSum()
    {
        // Arrange
        var vehicleId = 1;
        var startDate = DateTime.Now.AddMonths(-1);
        var endDate = DateTime.Now;
        var expectedCost = 500m;

        _mockRepository.Setup(r => r.GetTotalFuelCostAsync(vehicleId, startDate, endDate))
            .ReturnsAsync(expectedCost);

        // Act
        var result = await _service.GetTotalFuelCostAsync(vehicleId, startDate, endDate);

        // Assert
        Assert.Equal(expectedCost, result);
    }
}

public class ServiceRecordServiceTests
{
    private readonly Mock<IServiceRecordRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly ServiceRecordService _service;

    public ServiceRecordServiceTests()
    {
        _mockRepository = new Mock<IServiceRecordRepository>();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ServiceRecord, ServiceRecordResponseDto>();
            cfg.CreateMap<ServiceRecordCreateDto, ServiceRecord>();
            cfg.CreateMap<ServiceRecordUpdateDto, ServiceRecord>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        });
        _mapper = mapperConfig.CreateMapper();
        
        _service = new ServiceRecordService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetOverdueServiceRecordsAsync_ReturnsOverdueRecords()
    {
        // Arrange
        var overdueRecords = new List<ServiceRecord>
        {
            new ServiceRecord
            {
                Id = 1,
                VehicleId = 1,
                ServiceType = ServiceType.Maintenance,
                NextServiceDue = DateTime.Now.AddDays(-10)
            }
        };

        _mockRepository.Setup(r => r.GetOverdueServiceRecordsAsync())
            .ReturnsAsync(overdueRecords);

        // Act
        var result = await _service.GetOverdueServiceRecordsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetTotalMaintenanceCostAsync_ReturnsCorrectSum()
    {
        // Arrange
        var vehicleId = 1;
        var expectedCost = 1500m;

        _mockRepository.Setup(r => r.GetTotalMaintenanceCostAsync(vehicleId))
            .ReturnsAsync(expectedCost);

        // Act
        var result = await _service.GetTotalMaintenanceCostAsync(vehicleId);

        // Assert
        Assert.Equal(expectedCost, result);
    }
}
