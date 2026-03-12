using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;

    public DriverService(IDriverRepository driverRepository, IMapper mapper)
    {
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public async Task<DriverResponseDto?> GetDriverByIdAsync(int id)
    {
        var driver = await _driverRepository.GetByIdAsync(id);
        return driver == null ? null : _mapper.Map<DriverResponseDto>(driver);
    }

    public async Task<IEnumerable<DriverResponseDto>> GetAllDriversAsync()
    {
        var drivers = await _driverRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DriverResponseDto>>(drivers);
    }

    public async Task<IEnumerable<DriverResponseDto>> SearchDriversAsync(string? searchTerm, DriverStatus? status)
    {
        var drivers = await _driverRepository.SearchDriversAsync(searchTerm, status);
        return _mapper.Map<IEnumerable<DriverResponseDto>>(drivers);
    }

    public async Task<IEnumerable<DriverResponseDto>> GetExpiredLicenseDriversAsync()
    {
        var drivers = await _driverRepository.GetExpiredLicenseDriversAsync();
        return _mapper.Map<IEnumerable<DriverResponseDto>>(drivers);
    }

    public async Task<DriverResponseDto> CreateDriverAsync(DriverCreateDto driverDto)
    {
        var driver = _mapper.Map<Driver>(driverDto);
        driver.Status = DriverStatus.Active;
        await _driverRepository.AddAsync(driver);
        await _driverRepository.SaveChangesAsync();
        return _mapper.Map<DriverResponseDto>(driver);
    }

    public async Task<DriverResponseDto?> UpdateDriverAsync(int id, DriverUpdateDto driverDto)
    {
        var driver = await _driverRepository.GetByIdAsync(id);
        if (driver == null) return null;

        _mapper.Map(driverDto, driver);
        _driverRepository.Update(driver);
        await _driverRepository.SaveChangesAsync();
        return _mapper.Map<DriverResponseDto>(driver);
    }

    public async Task<bool> DeleteDriverAsync(int id)
    {
        var driver = await _driverRepository.GetByIdAsync(id);
        if (driver == null) return false;

        _driverRepository.Remove(driver);
        await _driverRepository.SaveChangesAsync();
        return true;
    }
}
