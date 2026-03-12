using Microsoft.AspNetCore.Mvc;
using CarFleet.Core.DTOs;
using CarFleet.Core.Services;

namespace CarFleet.Api.Controllers;

[ApiController]
[Route("api/documents")]
[Produces("application/json")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;
    private readonly ILogger<DocumentsController> _logger;

    public DocumentsController(IDocumentService documentService, ILogger<DocumentsController> logger)
    {
        _documentService = documentService;
        _logger = logger;
    }

    /// <summary>
    /// Get all documents
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentResponseDto>>> GetAllDocuments()
    {
        var documents = await _documentService.GetAllDocumentsAsync();
        return Ok(documents);
    }

    /// <summary>
    /// Get document by Id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DocumentResponseDto>> GetDocumentById(int id)
    {
        var document = await _documentService.GetDocumentByIdAsync(id);
        if (document == null)
            return NotFound(new { message = $"Document with id {id} not found" });
        return Ok(document);
    }

    /// <summary>
    /// Get documents for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentResponseDto>>> GetDocumentsForVehicle(int vehicleId)
    {
        var documents = await _documentService.GetDocumentsForVehicleAsync(vehicleId);
        return Ok(documents);
    }

    /// <summary>
    /// Get expiring documents (within specified days)
    /// </summary>
    [HttpGet("expiring")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentResponseDto>>> GetExpiringDocuments([FromQuery] int daysUntilExpiry = 30)
    {
        var documents = await _documentService.GetExpiringDocumentsAsync(daysUntilExpiry);
        return Ok(documents);
    }

    /// <summary>
    /// Create a new document
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DocumentResponseDto>> CreateDocument([FromBody] DocumentCreateDto documentDto)
    {
        try
        {
            var document = await _documentService.CreateDocumentAsync(documentDto);
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.Id }, document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating document");
            return BadRequest(new { message = "Error creating document", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a document
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DocumentResponseDto>> UpdateDocument(int id, [FromBody] DocumentUpdateDto documentDto)
    {
        try
        {
            var document = await _documentService.UpdateDocumentAsync(id, documentDto);
            if (document == null)
                return NotFound(new { message = $"Document with id {id} not found" });
            return Ok(document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating document");
            return BadRequest(new { message = "Error updating document", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a document
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var result = await _documentService.DeleteDocumentAsync(id);
        if (!result)
            return NotFound(new { message = $"Document with id {id} not found" });
        return NoContent();
    }
}
