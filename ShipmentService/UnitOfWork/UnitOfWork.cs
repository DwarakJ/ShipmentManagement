using ShipmentService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        RetailAppEntities ship = null;
        public UnitOfWork()
        {
            ship = new RetailAppEntities();
        }

        IShipment<Customer> customer = null;
        IShipment<Product> product = null;
        IShipment<Order> order = null;

        public IShipment<Customer> customerRepository
        {
            get
            {
                if (customer == null)
                {
                    customer = new CustomerRepository(ship);
                }
                return customer;
            }
        }

        public IShipment<Product> productRepository
        {
            get
            {
                if (product == null)
                {
                    product = new ProductRepository(ship);
                }
                return product;
            }
        }

        public IShipment<Order> orderRepository
        {
            get
            {
                if (order == null)
                {
                    order = new OrderRepository(ship);
                }
                return order;
            }
        }

        public void SaveChanges()
        {
            ship.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ship.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
