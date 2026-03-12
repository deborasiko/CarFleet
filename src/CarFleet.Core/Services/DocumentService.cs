using AutoMapper;
using CarFleet.Core.DTOs;
using CarFleet.Core.Models;
using CarFleet.Core.Repositories;

namespace CarFleet.Core.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IMapper _mapper;

    public DocumentService(IDocumentRepository documentRepository, IMapper mapper)
    {
        _documentRepository = documentRepository;
        _mapper = mapper;
    }

    public async Task<DocumentResponseDto?> GetDocumentByIdAsync(int id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        return document == null ? null : _mapper.Map<DocumentResponseDto>(document);
    }

    public async Task<IEnumerable<DocumentResponseDto>> GetAllDocumentsAsync()
    {
        var documents = await _documentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DocumentResponseDto>>(documents);
    }

    public async Task<IEnumerable<DocumentResponseDto>> GetDocumentsForVehicleAsync(int vehicleId)
    {
        var documents = await _documentRepository.GetDocumentsForVehicleAsync(vehicleId);
        return _mapper.Map<IEnumerable<DocumentResponseDto>>(documents);
    }

    public async Task<IEnumerable<DocumentResponseDto>> GetExpiringDocumentsAsync(int daysUntilExpiry)
    {
        var documents = await _documentRepository.GetExpiringDocumentsAsync(daysUntilExpiry);
        return _mapper.Map<IEnumerable<DocumentResponseDto>>(documents);
    }

    public async Task<DocumentResponseDto> CreateDocumentAsync(DocumentCreateDto documentDto)
    {
        var document = _mapper.Map<Document>(documentDto);
        document.UploadedAt = DateTime.UtcNow;
        await _documentRepository.AddAsync(document);
        await _documentRepository.SaveChangesAsync();
        return _mapper.Map<DocumentResponseDto>(document);
    }

    public async Task<DocumentResponseDto?> UpdateDocumentAsync(int id, DocumentUpdateDto documentDto)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document == null) return null;

        _mapper.Map(documentDto, document);
        _documentRepository.Update(document);
        await _documentRepository.SaveChangesAsync();
        return _mapper.Map<DocumentResponseDto>(document);
    }

    public async Task<bool> DeleteDocumentAsync(int id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document == null) return false;

        _documentRepository.Remove(document);
        await _documentRepository.SaveChangesAsync();
        return true;
    }
}
