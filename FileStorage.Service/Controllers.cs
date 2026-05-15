using Microsoft.AspNetCore.Mvc;
using FileStorage.Service.Core;

namespace FileStorage.Service.Controllers;

[ApiController]
[Route("filestorage")]
public class FileStorageController : ControllerBase
{
    private readonly FileStorageCore _core = new();

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var result = await _core.Upload(stream, file.FileName, file.ContentType);
        return Ok(result);
    }
}