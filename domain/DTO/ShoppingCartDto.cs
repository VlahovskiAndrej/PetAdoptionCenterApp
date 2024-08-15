
using domain.Models;

namespace domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ProductInShoppingCart>? AllProducts { get; set; }
        public double TotalPrice { get; set; }

    }
}
