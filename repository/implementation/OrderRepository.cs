using domain.Models;
using Microsoft.EntityFrameworkCore;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        //string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }


        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z=>z.ProductInOrders)
                .Include("ProductInOrders.OrderedProduct")
                .Include(z=>z.Owner)
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(Guid id)
        {
            
            var tmp =  entities
                .Include(x => x.ProductInOrders)
                .Include("ProductInOrders.OrderedProduct")
                .Include(x => x.Owner)
                .SingleOrDefaultAsync(x => x.Id == id).Result;

            return tmp;


                /*entities
                .Include(z => z.ProductInOrders)
                .Include(z => z.Owner)
                .Include("ProductInOrders.OrderedProduct")
                .SingleOrDefaultAsync(z=>z.Id == model.Id).Result;*/
        }
    }
}
