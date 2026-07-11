using System.ComponentModel.DataAnnotations;
public class InventoryItem
{
    [Key]
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Location { get; set; } = string.Empty;

    public void DisplayInfo()
    {
        Console.WriteLine($"Item: {Name} | Quantity: {Quantity} | Location: {Location}");
    }

    public void AddItem(InventoryItem item)
    {
        // Logic to add item to inventory
    }

    public void RemoveItem(int itemId)
    {
        // Logic to remove item from inventory
    }

}