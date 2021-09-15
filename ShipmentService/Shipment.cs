using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentService.UnitOfWork;

namespace ShipmentService
{
    public class ShipmentRepository
    {
        UnitOfWork.UnitOfWork work = new UnitOfWork.UnitOfWork();

        Validation validation = new Validation();

        Customer c = new Customer();

        Product p = new Product();

        Order o = new Order();

        string status = null;

        private Product GetProduct(string UPCCode)
        {
            p = work.productRepository.Get(code => code.UPC == UPCCode);

            return p;
        }

        private Product AddIfProductExists(OrderInfo info)
        {
            GetProduct(info.UPCCode);

            if (p.UPC == null)
            {
                p.UPC = info.UPCCode;

                work.productRepository.Add(p);

                work.SaveChanges();
            }

            return p;
        }

        private Customer GetCustomerByUserName(string UserName)
        {
            c = work.customerRepository.Get(customer => customer.UserName == UserName);

            return c;
        }

        private IEnumerable<Customer> GetCustomerByCompanyName(string CompanyName)
        {
            var c = work.customerRepository.GetAll(customer => customer.UserName == CompanyName);

            return c.Cast<Customer>();
        }

        private Customer GetCustomer(string CompanyName, string UserName)
        {
            c = work.customerRepository.Get(customer => customer.UserName == UserName && customer.CompanyName == CompanyName);

            return c;
        }

        private Customer AddIfCustomerExists(OrderInfo info)
        {

                c = GetCustomer(info.CompanyName, info.UserName);

                if (c.CompanyName == null && c.UserName == null)
                {
                    c.CompanyName = info.CompanyName;
                    c.UserName = info.UserName;
                    work.customerRepository.Add(c);

                    work.SaveChanges();
                }
            return c;
        }

        private Order AddOrder(Product p, Customer c, OrderInfo info)
        {
            o.ProductID = p.id;
            o.CustomerID = c.id;
            o.PackingDate = info.PackingDate.Value.ToUniversalTime();

            work.orderRepository.Add(o);

            work.SaveChanges();

            return o;
        }

        public string AddOrderDetails(OrderInfo info)
        {
            if (validation.IsValidEmail(info.UserName) && validation.IsValidUPC(info.UPCCode) && validation.IsValidPurchaseDate(info.PackingDate))
            {
                var customer = AddIfCustomerExists(info);

                var product = AddIfProductExists(info);

                var order = AddOrder(product, customer, info);

                if (order.id != 0)
                {
                    status = "Successful";
                }
                else
                {
                    status = "NotSuccessful";
                }
            }
            return status;
        }

        public string DeleteOrder(string companyname)
        {
            try
            {
                var customerOrders = work.customerRepository.GetAll(x => x.CompanyName == companyname);

                foreach(Customer c in customerOrders)
                {
                    var OrdersToRemove = work.orderRepository.Get(x => x.CustomerID == c.id);
                    work.orderRepository.Remove(OrdersToRemove);
                }

                work.SaveChanges();

                status = "Order Deleted Successfully";
            }
            catch(Exception e)
            {
                status = e.Message;
            }
            return status;
        }

        public string DeleteProducts(string upccode)
        {
            try
            {
                var productstodelete = work.productRepository.Get(p => p.UPC == upccode);
                work.productRepository.Remove(productstodelete);
                work.SaveChanges();
                status = "Product Deleted Successfully";
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }

        private IEnumerable<Order> GetOrderByCompanyName(string CompanyName)
        {
            var o = work.orderRepository.GetAll(w => w.Customer.CompanyName == CompanyName);

            return o.Cast<Order>();
        }

        private IEnumerable<Order> GetOrderByUserName(string UserName)
        {
            var o = work.orderRepository.GetAll(w => w.Customer.UserName == UserName);

            return o.Cast<Order>();
        }

        public List<OrderInfo> GetOrderDetailsByCompanyName(string companyname)
        {
            List<OrderInfo> orders = null;

            var order = GetOrderByCompanyName(companyname);
            
            var res = from o in order
                      select new OrderInfo() { CompanyName = o.Customer.CompanyName, UserName = o.Customer.UserName, UPCCode = o.Product.UPC, PackingDate = o.PackingDate };

            orders = res.ToList();

            return orders;
        }

        public List<OrderInfo> GetOrderDetailsByUserName(string username)
        {
            List<OrderInfo> orders = null;

            var order =  GetOrderByUserName(username);

            var res = from o in order
                      select new OrderInfo() { CompanyName = o.Customer.CompanyName, UserName = o.Customer.UserName, UPCCode = o.Product.UPC, PackingDate = o.PackingDate };

            orders = res.ToList();

            return orders;
        }
    }
}
