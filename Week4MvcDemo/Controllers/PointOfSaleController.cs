using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreDatabase.Data;
using StoreDatabase.Entities;
using Week4MvcDemo.Models;

namespace Week4MvcDemo.Controllers
{
    public class PointOfSaleController : Controller
    {
        StoreContext _context;

        public PointOfSaleController(StoreContext context)
        {
            _context = context; 
        }

        public async Task<IActionResult> Index(int customerId)
        {
            Customer customer = await _context.Customers.FirstAsync(c => c.CustomerId == customerId);
            List<Product> products = await _context.Products.ToListAsync();

            OrderInProgress orderInProgress = new OrderInProgress
            {
                Customer = customer,
                Products = products
            };

            return View(orderInProgress);
        }

        public async Task<IActionResult> AddItem(int customerId, int orderId, int productId)
        {
            Order? order;

            if(orderId==0)
            {
                order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else 
            {
                order = await _context.Orders.Where(o => o.OrderId == orderId).FirstAsync();
            }

            var product = await _context.Products.Where(p =>  p.ProductId == productId).FirstAsync();

            var orderItem = new OrderLineItem
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = 1,
                Price = (int)product.ProductPrice,
                Product = product
            };

            order.OrderLineItems.Add(orderItem);
            _context.SaveChanges();

            Customer customer = await _context.Customers.FirstAsync(c => c.CustomerId == customerId);
            List<Product> products = await _context.Products.ToListAsync();

            OrderInProgress orderInProgress = new OrderInProgress
            {
                Customer = customer,
                Products = products,
                Order = order
            };

            return View("Index",orderInProgress);
        }
    }
}
