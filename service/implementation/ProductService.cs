using domain.Models;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }


        public Product CreateNewProduct(string userId, Product product)
        {
            return this._productRepository.Insert(product);
        }

        public Product DeleteProduct(Guid id)
        {
            return _productRepository.Delete(GetProductById(id));
        }

        public Product GetProductById(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product UpdateProduct(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
