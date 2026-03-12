using CarFleet.Core.DTOs;

namespace CarFleet.Core.Services;

public interface IDocumentService
{
    Task<DocumentResponseDto?> GetDocumentByIdAsync(int id);
    Task<IEnumerable<DocumentResponseDto>> GetAllDocumentsAsync();
    Task<IEnumerable<DocumentResponseDto>> GetDocumentsForVehicleAsync(int vehicleId);
    Task<IEnumerable<DocumentResponseDto>> GetExpiringDocumentsAsync(int daysUntilExpiry);
    Task<DocumentResponseDto> CreateDocumentAsync(DocumentCreateDto documentDto);
    Task<DocumentResponseDto?> UpdateDocumentAsync(int id, DocumentUpdateDto documentDto);
    Task<bool> DeleteDocumentAsync(int id);
}
