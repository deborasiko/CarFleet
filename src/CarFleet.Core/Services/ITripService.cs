using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface ITripService
{
    Task<TripResponseDto?> GetTripByIdAsync(int id);
    Task<IEnumerable<TripResponseDto>> GetAllTripsAsync();
    Task<IEnumerable<TripResponseDto>> GetTripsForVehicleAsync(int vehicleId);
    Task<IEnumerable<TripResponseDto>> GetTripsForDriverAsync(int driverId);
    Task<decimal> GetTotalDistanceAsync(int vehicleId, DateTime startDate, DateTime endDate);
    Task<TripResponseDto> CreateTripAsync(TripCreateDto tripDto);
    Task<TripResponseDto?> UpdateTripAsync(int id, TripUpdateDto tripDto);
    Task<bool> DeleteTripAsync(int id);
    Task<TripResponseDto?> EndTripAsync(int id);
}
