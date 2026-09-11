using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace LogiTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InventoryController : ControllerBase
{
    private const string CacheKey = "inventory-items";
    private readonly LogiTrackContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(
        LogiTrackContext context,
        IMemoryCache cache,
        ILogger<InventoryController> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    // GET: api/inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
    {
        var stopwatch = Stopwatch.StartNew();

        if (_cache.TryGetValue(CacheKey, out List<InventoryItem>? cachedItems))
        {
            stopwatch.Stop();
            Response.Headers["X-Cache"] = "HIT";
            Response.Headers["X-Cache-Elapsed-Milliseconds"] = stopwatch.Elapsed.TotalMilliseconds.ToString("F3");
            _logger.LogInformation(
                "Inventory cache hit returned {ItemCount} items in {ElapsedMilliseconds:F3} ms",
                cachedItems?.Count ?? 0,
                stopwatch.Elapsed.TotalMilliseconds);
            return cachedItems ?? new List<InventoryItem>();
        }

        var items = await _context.InventoryItems.ToListAsync();

        _cache.Set(CacheKey, items, TimeSpan.FromSeconds(30));
        stopwatch.Stop();
        Response.Headers["X-Cache"] = "MISS";
        Response.Headers["X-Cache-Elapsed-Milliseconds"] = stopwatch.Elapsed.TotalMilliseconds.ToString("F3");
        _logger.LogInformation(
            "Inventory cache miss loaded {ItemCount} items in {ElapsedMilliseconds:F3} ms",
            items.Count,
            stopwatch.Elapsed.TotalMilliseconds);

        return items;
    }

    // POST: api/inventory
    [HttpPost]
    public async Task<ActionResult<InventoryItem>> PostInventoryItem(InventoryItem item)
    {
        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync();
        _cache.Remove(CacheKey);

        return CreatedAtAction(nameof(GetInventoryItems), new { id = item.ItemId }, item);
    }

    // DELETE: api/inventory/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteInventoryItem(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();
        _cache.Remove(CacheKey);

        return NoContent();
    }
}
