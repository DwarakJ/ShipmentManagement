using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Repository
{
    public class CustomerRepository: IShipment<Customer>
    {

        RetailAppEntities ship = new RetailAppEntities();

        public CustomerRepository(RetailAppEntities _entities)
        {
            ship = _entities;
        }
        public void Add(Customer entity)
        {
            ship.Customers.Add(entity);
        }

        public Customer Get(Func<Customer, bool> predicate)
        {
            return ship.Customers.FirstOrDefault(predicate);
        }

        public IEnumerable GetAll(Func<Customer, bool> predicate = null)
        {
            if (predicate != null)
            {
                return ship.Customers.Where(predicate);
            }

            return ship.Customers;
        }

        public void Remove(Customer entity)
        {
            ship.Customers.Remove(entity);
        }

        public void Update(Customer entity)
        {
            ship.Customers.Attach(entity);
        }
    }
}
