using System.ComponentModel.DataAnnotations;
using domain.Identity;


namespace domain.Models
{
    public class ShoppingCart : BaseEntity
    {
       
        public string? OwnerId { get; set; }
        public PetAdoptionCenterUser? Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }

    }
}
