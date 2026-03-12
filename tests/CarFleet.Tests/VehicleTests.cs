using Xunit;
using CarFleet.Core.Models;

namespace CarFleet.Tests;

public class VehicleTests
{
    [Fact]
    public void CanCreateVehicle()
    {
        var v = new Vehicle { Make = "Toyota", Model = "Corolla", Year = 2020, Vin = "VIN123" };
        Assert.Equal("Toyota", v.Make);
        Assert.Equal(VehicleStatus.Active, v.Status);
    }
}