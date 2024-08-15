using domain.DTO;
using domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddProductToShoppingCart(string userId, AddToShoppingCartDto model);
        AddToShoppingCartDto getProductInfo(Guid Id);
        ShoppingCartDto getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
        Boolean orderProducts(string userId);
        List<ProductInOrder> getProductInOrderList(Guid id);
    }
}
