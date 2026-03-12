using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public TripService(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public async Task<TripResponseDto?> GetTripByIdAsync(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        return trip == null ? null : _mapper.Map<TripResponseDto>(trip);
    }

    public async Task<IEnumerable<TripResponseDto>> GetAllTripsAsync()
    {
        var trips = await _tripRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TripResponseDto>>(trips);
    }

    public async Task<IEnumerable<TripResponseDto>> GetTripsForVehicleAsync(int vehicleId)
    {
        var trips = await _tripRepository.GetTripsForVehicleAsync(vehicleId);
        return _mapper.Map<IEnumerable<TripResponseDto>>(trips);
    }

    public async Task<IEnumerable<TripResponseDto>> GetTripsForDriverAsync(int driverId)
    {
        var trips = await _tripRepository.GetTripsForDriverAsync(driverId);
        return _mapper.Map<IEnumerable<TripResponseDto>>(trips);
    }

    public async Task<decimal> GetTotalDistanceAsync(int vehicleId, DateTime startDate, DateTime endDate)
    {
        return await _tripRepository.GetTotalDistanceAsync(vehicleId, startDate, endDate);
    }

    public async Task<TripResponseDto> CreateTripAsync(TripCreateDto tripDto)
    {
        var trip = _mapper.Map<Trip>(tripDto);
        trip.DistanceKm = 0;
        await _tripRepository.AddAsync(trip);
        await _tripRepository.SaveChangesAsync();
        return _mapper.Map<TripResponseDto>(trip);
    }

    public async Task<TripResponseDto?> UpdateTripAsync(int id, TripUpdateDto tripDto)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        if (trip == null) return null;

        _mapper.Map(tripDto, trip);
        _tripRepository.Update(trip);
        await _tripRepository.SaveChangesAsync();
        return _mapper.Map<TripResponseDto>(trip);
    }

    public async Task<bool> DeleteTripAsync(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        if (trip == null) return false;

        _tripRepository.Remove(trip);
        await _tripRepository.SaveChangesAsync();
        return true;
    }

    public async Task<TripResponseDto?> EndTripAsync(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        if (trip == null) return null;

        trip.EndTime = DateTime.Now;
        _tripRepository.Update(trip);
        await _tripRepository.SaveChangesAsync();
        return _mapper.Map<TripResponseDto>(trip);
    }
}
