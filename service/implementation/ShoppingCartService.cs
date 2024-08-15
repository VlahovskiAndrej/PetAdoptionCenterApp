using domain.DTO;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace service.implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;


        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }
        public ShoppingCart AddProductToShoppingCart(string userId, AddToShoppingCartDto model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var selectedProduct = _productRepository.Get(model.ProductId);

                if (selectedProduct != null && userCart != null)
                {
                    userCart?.ProductInShoppingCarts?.Add(new ProductInShoppingCart
                    {
                        Product = selectedProduct,
                        ProductId = selectedProduct.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity
                    });

                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;

        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {

            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);


                var product_to_delete = loggedInUser?.UserCart?.ProductInShoppingCarts.First(z => z.ProductId == Id);

                loggedInUser?.UserCart?.ProductInShoppingCarts?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser?.UserCart);


                return true;

            }
            return false;
        }

        public AddToShoppingCartDto getProductInfo(Guid Id)
        {
            var selectedProduct = _productRepository.Get(Id);
            if (selectedProduct != null)
            {
                var model = new AddToShoppingCartDto
                {
                    SelectedProductName = selectedProduct.ProductName,
                    ProductId = selectedProduct.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;

        }

        public List<ProductInOrder> getProductInOrderList(Guid id)
        {
            return _productInOrderRepository.GetAll().Where(x=>x.OrderId == id).ToList();
        }

        public ShoppingCartDto getShoppingCartDetails(string userId)
        {
           

            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);
              

                var allProducts = loggedInUser?.UserCart?.ProductInShoppingCarts?.ToList();

                var totalPrice = 0.0;

                foreach (var item in allProducts)
                {
                    totalPrice += Double.Round((item.Quantity * item.Product.Price), 2);
                }

                var model = new ShoppingCartDto
                {
                    AllProducts = allProducts,
                    TotalPrice = totalPrice
                };

                return(model);

            }

            return new ShoppingCartDto
            {
                AllProducts = new List<ProductInShoppingCart>(),
                TotalPrice = 0.0
            };

        }

        public bool orderProducts(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(userOrder);

                List<ProductInOrder> productsInOrder = new List<ProductInOrder>();
                var productInOrders = userCart?.ProductInShoppingCarts?.Select(z => new ProductInOrder
                {
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    ProductId = z.ProductId,
                    OrderedProduct = z.Product,
                    Quantity = z.Quantity
                }).ToList();

                productsInOrder.AddRange(productInOrders);
                foreach (var product in productsInOrder)
                {
                    _productInOrderRepository.Insert(product);
                }

               /* _productInOrderRepository.InsertMany(productInOrders);*/

               

                userCart?.ProductInShoppingCarts.Clear();

                _shoppingCartRepository.Update(userCart);


                return true;
            }
            return false;
        }
    }
    
}
