using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_2
{
    public class ShopReportService
    {
        private readonly IRepository<Category> _categories;
        private readonly IRepository<Product> _products;
        private readonly IRepository<Customer> _customers;
        private readonly IRepository<Order> _orders;

        public ShopReportService(IRepository<Category> c, IRepository<Product> p,
                                 IRepository<Customer> cu, IRepository<Order> o)
        { _categories = c; _products = p; _customers = cu; _orders = o; }
        public void PrintCatalog()
        {
            Console.WriteLine("Категорії та товари (1:N)");
            foreach (var c in _categories.GetAll())
            {
                Console.WriteLine($"{c.Name}:");
                foreach (var p in _products.GetAll().Where(p => p.CategoryId == c.Id))
                    Console.WriteLine($"  - {p.Name}, {p.Price:F2} грн");
            }
        }

        public void PrintCustomersWithCards()
        {
            Console.WriteLine("\n Клієнти та картки (1:1)");
            foreach (var cu in _customers.GetAll())
                Console.WriteLine($"{cu.FullName} -> картка {cu.Card?.CardNumber ?? "немає"} " +
                                  $"(знижка {cu.Card?.DiscountPercent ?? 0}%)");
        }

        public void PrintOrder(int orderId)
        {
            var o = _orders.GetById(orderId);
            if (o == null) { Console.WriteLine("Замовлення не знайдено"); return; }
            var cu = _customers.GetById(o.CustomerId);
            int discount = cu?.Card?.DiscountPercent ?? 0;

            Console.WriteLine($"\nЗамовлення №{o.Id} від {o.OrderDate:dd.MM.yyyy} ({cu?.FullName})");
            decimal total = 0;
            foreach (var i in o.Items)
            {
                var sum = i.Product!.Price * i.Quantity;
                total += sum;
                Console.WriteLine($"  {i.Product.Name} x{i.Quantity} = {sum:F2} грн");
            }
            Console.WriteLine($"Разом: {total:F2} грн, зі знижкою {discount}%: {total * (100 - discount) / 100:F2} грн");
        }

        public void PrintProductsInOrders()
        {
            Console.WriteLine("\nТовари та замовлення (M:N)");
            foreach (var p in _products.GetAll())
            {
                var ids = _orders.GetAll()
                    .Where(o => o.Items.Any(i => i.ProductId == p.Id))
                    .Select(o => $"№{o.Id}");
                Console.WriteLine($"{p.Name} -> замовлення: {(ids.Any() ? string.Join(", ", ids) : "немає")}");
            }
        }
    }


}
