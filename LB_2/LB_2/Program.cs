using System.Text;
using LB_2;

Console.OutputEncoding = Encoding.UTF8;

var categories = new InMemoryRepository<Category>(x => x.Id);
var products = new InMemoryRepository<Product>(x => x.Id);
var customers = new InMemoryRepository<Customer>(x => x.Id);
var orders = new InMemoryRepository<Order>(x => x.Id);

var milk = new Category { Id = 1, Name = "Молочні продукти" };
var bread = new Category { Id = 2, Name = "Хліб та випічка" };
categories.Add(milk); categories.Add(bread);

var p1 = new Product { Id = 1, Name = "Молоко 2.5%", Price = 42.50m, CategoryId = 1, Category = milk };
var p2 = new Product { Id = 2, Name = "Сир кисломолочний", Price = 68m, CategoryId = 1, Category = milk };
var p3 = new Product { Id = 3, Name = "Батон", Price = 28m, CategoryId = 2, Category = bread };
products.Add(p1); products.Add(p2); products.Add(p3);

var cust = new Customer { Id = 1, FullName = "Шеренок Олександр" };
cust.Card = new LoyaltyCard { Id = 1, CustomerId = 1, CardNumber = "LC-0001", DiscountPercent = 10 };
customers.Add(cust);
customers.Add(new Customer { Id = 2, FullName = "Таракан Родион" });

var order2 = new Order { Id = 2, CustomerId = 2, OrderDate = new DateTime(2026, 10, 6) };
order2.Items.Add(new OrderItem { Id = 3, OrderId = 2, ProductId = 1, Product = p1, Quantity = 1 });
order2.Items.Add(new OrderItem { Id = 4, OrderId = 2, ProductId = 2, Product = p2, Quantity = 3 });
orders.Add(order2);

var order = new Order { Id = 1, CustomerId = 1, OrderDate = new DateTime(2026, 10, 5) };
order.Items.Add(new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Product = p1, Quantity = 2 });
order.Items.Add(new OrderItem { Id = 2, OrderId = 1, ProductId = 3, Product = p3, Quantity = 1 });
orders.Add(order);

var service = new ShopReportService(categories, products, customers, orders);
service.PrintCatalog();
service.PrintCustomersWithCards();
service.PrintOrder(1);

service.PrintProductsInOrders();
service.PrintOrder(1);
service.PrintOrder(2);