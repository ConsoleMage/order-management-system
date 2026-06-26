public class InventoryItem
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Location { get; set; }

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

