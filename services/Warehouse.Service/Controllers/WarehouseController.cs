using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Warehouse.Service.Models;

namespace AIEnterpriseOS.Warehouse.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private static readonly List<WarehouseItem> _items = new();
    private static readonly List<WarehouseStock> _stocks = new();
    private static readonly List<WarehouseMovement> _movements = new();
    private static readonly List<Supplier> _suppliers = new();
    private static readonly List<PurchaseOrder> _orders = new();

    [HttpPost("items")]
    public IActionResult UpsertItem([FromBody] WarehouseItem item)
    {
        var existing = _items.FirstOrDefault(i => i.Id == item.Id || i.Sku == item.Sku);
        if (existing is null)
        {
            item.Id = string.IsNullOrWhiteSpace(item.Id) ? Guid.NewGuid().ToString() : item.Id;
            _items.Add(item);
        }
        else
        {
            existing.Description = item.Description;
            existing.UnitOfMeasure = item.UnitOfMeasure;
        }

        return Ok(item);
    }

    [HttpGet("items")]
    public IActionResult GetItems() => Ok(_items);

    [HttpPost("suppliers")]
    public IActionResult UpsertSupplier([FromBody] Supplier supplier)
    {
        var existing = _suppliers.FirstOrDefault(s => s.Id == supplier.Id);
        if (existing is null)
        {
            supplier.Id = string.IsNullOrWhiteSpace(supplier.Id) ? Guid.NewGuid().ToString() : supplier.Id;
            _suppliers.Add(supplier);
        }
        else
        {
            existing.Name = supplier.Name;
            existing.VatNumber = supplier.VatNumber;
            existing.Country = supplier.Country;
        }

        return Ok(supplier);
    }

    [HttpGet("suppliers")]
    public IActionResult GetSuppliers() => Ok(_suppliers);

    [HttpPost("movements")]
    public IActionResult RegisterMovement([FromBody] WarehouseMovement movement)
    {
        movement.Id = string.IsNullOrWhiteSpace(movement.Id) ? Guid.NewGuid().ToString() : movement.Id;
        _movements.Add(movement);

        var stock = _stocks.FirstOrDefault(s => s.ItemId == movement.ItemId && s.Location == "MAIN");
        if (stock is null)
        {
            stock = new WarehouseStock { ItemId = movement.ItemId, Location = "MAIN", Quantity = 0 };
            _stocks.Add(stock);
        }

        if (movement.Type == "IN")
            stock.Quantity += movement.Quantity;
        else if (movement.Type == "OUT")
            stock.Quantity -= movement.Quantity;

        return Ok(new { movement.Id, stock.Quantity });
    }

    [HttpGet("stock")]
    public IActionResult GetStock() => Ok(_stocks);

    [HttpPost("orders")]
    public IActionResult CreateOrder([FromBody] PurchaseOrder order)
    {
        order.Id = string.IsNullOrWhiteSpace(order.Id) ? Guid.NewGuid().ToString() : order.Id;
        _orders.Add(order);
        return Ok(order);
    }

    [HttpGet("orders")]
    public IActionResult GetOrders() => Ok(_orders);
}
