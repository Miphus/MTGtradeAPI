using Microsoft.AspNetCore.Mvc;
using MTGtrade.API.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImportController : ControllerBase
{
    private readonly MtgJsonImporter _importer;

    public ImportController(MtgJsonImporter importer)
    {
        _importer = importer;
    }

    [HttpPost("run")]
    public async Task<IActionResult> RunImport()
    {
        var path = Path.Combine("..", "..", "Data", "AllPrintings.json");
        await _importer.ImportAllAsync(path);
        return Ok("Import completed.");
    }
}
