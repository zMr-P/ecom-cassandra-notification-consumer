using Cassandra.Mapping;
using Domain.Entities;

namespace Infrastructure.Mapping;

public static class CassandraMapping
{
    public static MappingConfiguration GetMappings()
    {
        var mappingConfig = new MappingConfiguration();

        mappingConfig.Define(
            new Map<User>()
                .TableName("users")
                .PartitionKey(u => u.Id)
                .Column(u => u.Name, cm => cm.WithName("name"))
                .Column(u => u.Email, cm => cm.WithName("email"))
                .Column(u => u.Role, cm => cm.WithName("role"))
                .Column(u => u.CreatedAt, cm => cm.WithName("created_at"))
                .Column(u => u.UpdatedAt, cm => cm.WithName("updated_at"))
                .Column(u => u.Addresses, cm => cm.WithName("addresses"))
        );

        mappingConfig.Define(
            new Map<Order>()
                .TableName("orders")
                .PartitionKey(o => o.Id)
                .Column(o => o.UserId, cm => cm.WithName("user_id"))
                .Column(o => o.Status, cm => cm.WithName("status"))
                .Column(o => o.TotalAmount, cm => cm.WithName("total_amount"))
                .Column(o => o.CreatedAt, cm => cm.WithName("created_at"))
                .Column(o => o.UpdatedAt, cm => cm.WithName("updated_at"))
                .Column(o => o.ShippingAddress, cm => cm.WithName("shipping_address"))
        );

        mappingConfig.Define(
            new Map<OrderItem>()
                .TableName("order_items")
                .Column(oi => oi.OrderId, cm => cm.WithName("order_id"))
                .Column(oi => oi.ProductId, cm => cm.WithName("product_id"))
                .Column(oi => oi.Quantity, cm => cm.WithName("quantity"))
                .Column(oi => oi.UnitPrice, cm => cm.WithName("unit_price"))
        );

        mappingConfig.Define(
            new Map<Category>()
                .TableName("categories")
                .PartitionKey(c => c.Id)
                .Column(c => c.Name, cm => cm.WithName("name"))
                .Column(c => c.Description, cm => cm.WithName("description"))
        );

        mappingConfig.Define(
            new Map<Product>()
                .TableName("products")
                .PartitionKey(p => p.Id)
                .Column(p => p.CategoryId, cm => cm.WithName("category_id"))
                .Column(p => p.Name, cm => cm.WithName("name"))
                .Column(p => p.Description, cm => cm.WithName("description"))
                .Column(p => p.Price, cm => cm.WithName("price"))
                .Column(p => p.StockQuantity, cm => cm.WithName("stock_quantity"))
                .Column(p => p.CreatedAt, cm => cm.WithName("created_at"))
                .Column(p => p.UpdatedAt, cm => cm.WithName("updated_at"))
        );

        return mappingConfig;
    }
}