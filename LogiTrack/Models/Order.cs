public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime DatePlaced { get; set; }
    public List<InventoryItem> Items { get; set; }

    public void GetOrderSummary()
    {
        Console.WriteLine($"Order ID: {Id} for {CustomerName} | Items: {Items.Count} | Date: {DatePlaced}");
    }

    
}