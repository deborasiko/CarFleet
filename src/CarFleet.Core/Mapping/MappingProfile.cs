using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;

namespace CarFleet.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Vehicle mappings
        CreateMap<Vehicle, VehicleResponseDto>();
        CreateMap<VehicleCreateDto, Vehicle>();
        CreateMap<VehicleUpdateDto, Vehicle>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Driver mappings
        CreateMap<Driver, DriverResponseDto>();
        CreateMap<DriverCreateDto, Driver>();
        CreateMap<DriverUpdateDto, Driver>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // VehicleAssignment mappings
        CreateMap<VehicleAssignment, VehicleAssignmentResponseDto>();
        CreateMap<VehicleAssignmentCreateDto, VehicleAssignment>();
        CreateMap<VehicleAssignmentUpdateDto, VehicleAssignment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // FuelLog mappings
        CreateMap<FuelLog, FuelLogResponseDto>();
        CreateMap<FuelLogCreateDto, FuelLog>();
        CreateMap<FuelLogUpdateDto, FuelLog>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ServiceRecord mappings
        CreateMap<ServiceRecord, ServiceRecordResponseDto>();
        CreateMap<ServiceRecordCreateDto, ServiceRecord>();
        CreateMap<ServiceRecordUpdateDto, ServiceRecord>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Trip mappings
        CreateMap<Trip, TripResponseDto>();
        CreateMap<TripCreateDto, Trip>();
        CreateMap<TripUpdateDto, Trip>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Expense mappings
        CreateMap<Expense, ExpenseResponseDto>();
        CreateMap<ExpenseCreateDto, Expense>();
        CreateMap<ExpenseUpdateDto, Expense>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Document mappings
        CreateMap<Document, DocumentResponseDto>();
        CreateMap<DocumentCreateDto, Document>();
        CreateMap<DocumentUpdateDto, Document>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // FleetLocation mappings
        CreateMap<FleetLocation, FleetLocationResponseDto>();
        CreateMap<FleetLocationCreateDto, FleetLocation>();
        CreateMap<FleetLocationUpdateDto, FleetLocation>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // User mappings
        CreateMap<User, UserResponseDto>();
    }
}
