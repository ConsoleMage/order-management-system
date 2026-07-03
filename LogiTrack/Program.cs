// // Test block: Create order, add/remove items, print summary
// Console.WriteLine("=== Order Management Test Block ===\n");

// // Create a new order
// var testOrder = new Order
// {
//     Id = 1001,
//     CustomerName = "John Doe",
//     DatePlaced = DateTime.Now,
//     Items = new List<InventoryItem>()
// };

// // Add items to the order
// var item1 = new InventoryItem
// {
//     ItemId = 1,
//     Name = "Laptop",
//     Quantity = 2,
//     Location = "Warehouse A"
// };

// var item2 = new InventoryItem
// {
//     ItemId = 2,
//     Name = "Monitor",
//     Quantity = 3,
//     Location = "Warehouse B"
// };

// var item3 = new InventoryItem
// {
//     ItemId = 3,
//     Name = "Keyboard",
//     Quantity = 5,
//     Location = "Warehouse A"
// };

// testOrder.Items.Add(item1);
// testOrder.Items.Add(item2);
// testOrder.Items.Add(item3);

// Console.WriteLine("Items added to order:");
// foreach (var item in testOrder.Items)
// {
//     item.DisplayInfo();
// }

// // Remove an item from the order
// Console.WriteLine("\nRemoving item with ID 2...");
// testOrder.Items.RemoveAll(item => item.ItemId == 2);

// // Print the order summary
// Console.WriteLine("\n");
// testOrder.GetOrderSummary();

// Console.WriteLine("\nFinal order items:");
// foreach (var item in testOrder.Items)
// {
//     item.DisplayInfo();
// }

// Console.WriteLine("\n=== Test Block Complete ===\n");

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
