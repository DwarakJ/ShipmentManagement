using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Repository
{
    class OrderRepository : IShipment<Order>
    {
        RetailAppEntities ship = new RetailAppEntities();

        public OrderRepository(RetailAppEntities _entities)
        {
            ship = _entities;
        }
        public void Add(Order entity)
        {
            ship.Orders.Add(entity);
        }

        public Order Get(Func<Order, bool> predicate)
        {
            return ship.Orders.FirstOrDefault(predicate);
        }

        public IEnumerable GetAll(Func<Order, bool> predicate = null)
        {
            if (predicate != null)
            {
                return ship.Orders.Where(predicate);
            }

            return ship.Orders;
        }

        public void Remove(Order entity)
        {
            ship.Orders.Remove(entity);
        }

        public void Update(Order entity)
        {
            ship.Orders.Attach(entity);
        }
    }
}
