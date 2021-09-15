using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Repository
{
    class ProductRepository : IShipment<Product>
    {
        RetailAppEntities ship = new RetailAppEntities();

        public ProductRepository(RetailAppEntities _entities)
        {
            ship = _entities;
        }
        public void Add(Product entity)
        {
            ship.Products.Add(entity);
        }

        public Product Get(Func<Product, bool> predicate)
        {
            return ship.Products.FirstOrDefault(predicate);
        }

        public IEnumerable GetAll(Func<Product, bool> predicate = null)
        {
                if (predicate != null)
                {
                    return ship.Products.Where(predicate);
                }

            return ship.Products;
        }

        public void Remove(Product entity)
        {
            ship.Products.Remove(entity);
        }

        public void Update(Product entity)
        {
            ship.Products.Attach(entity);
        }
    }
}
