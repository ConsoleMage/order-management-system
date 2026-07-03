using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly LogiTrackContext _context;

    public InventoryController(LogiTrackContext context)
    {
        _context = context;
    }

    // GET: api/inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
    {
        return await _context.InventoryItems.ToListAsync();
    }

    // POST: api/inventory
    [HttpPost]
    public async Task<ActionResult<InventoryItem>> PostInventoryItem(InventoryItem item)
    {
        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInventoryItems), new { id = item.ItemId }, item);
    }

    // DELETE: api/inventory/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventoryItem(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
