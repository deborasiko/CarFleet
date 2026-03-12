using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IDocumentRepository : IRepository<Document>
{
    Task<IEnumerable<Document>> GetDocumentsForVehicleAsync(int vehicleId);
    Task<IEnumerable<Document>> GetExpiringDocumentsAsync(int daysUntilExpiry);
}
