using System.ComponentModel.DataAnnotations;
using domain.Identity;


namespace domain.Models
{
    public class Order : BaseEntity
    {
        
        public string? OwnerId { get; set; }
        public PetAdoptionCenterUser? Owner { get; set; }

        public ICollection<ProductInOrder>? ProductInOrders { get; set; }

    }
}
