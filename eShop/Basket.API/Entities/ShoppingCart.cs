namespace Basket.API.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public ShoppingCart() { }
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}
public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; }
    public string ImageFile { get; set; }
    public string ProductName { get; set; }
}


