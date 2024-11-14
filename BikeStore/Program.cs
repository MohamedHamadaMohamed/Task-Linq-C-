using BikeStore.Data;
using BikeStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BikeStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext DbContext = new ApplicationDbContext();

            //Using Linq &C# on BikeStoreDB:
            //Retrieve all categories from the production.categories table.
            Console.WriteLine("Categories : ");
            var categories = DbContext.Categories;
            foreach (var item in categories)
            {
                Console.WriteLine($"ID : {item.CategoryId} Name : {item.CategoryName} ");
            }

            //Retrieve the first product from the production.products table.
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("First Product : ");
            var firstProduct = DbContext.Products.First();
            Console.WriteLine($"ID: {firstProduct.ProductId} Name : {firstProduct.ProductName} " +
                $"price: {firstProduct.ListPrice} BrandId: {firstProduct.BrandId}");
            Console.WriteLine("-----------------------------------------------------");

            //Retrieve a specific product from the production.products table by ID.
            Console.WriteLine("production.products table by ID =4 ");
            var ID4 = DbContext.Products.Find(4);
            
            Console.WriteLine($"ID: {ID4.ProductId} Name : {ID4.ProductName} " +
                    $"price: {ID4.ListPrice} BrandId: {ID4.BrandId} ");
            
            Console.WriteLine("-----------------------------------------------------");
            // Retrieve all products from the production.products table with a certain model year.
            Console.WriteLine("production.products table by model year = 2017");

            var products = DbContext.Products.Where(e => e.ModelYear == 2017);
            foreach( var item in products)
            {
                Console.WriteLine($"ID: {item.ProductId} Name : {item.ProductName} " +
                $"price: {item.ListPrice} BrandId: {item.BrandId} model year : {item.ModelYear}");
            }
            Console.WriteLine("-----------------------------------------------------");

            //Retrieve a specific customer from the sales.customers table by ID.
            Console.WriteLine("specific customer from the sales.customers table by ID =4");
            var customer = DbContext.Customers.Find(4);

            Console.WriteLine($"ID: {customer.CustomerId} Name : {customer.FirstName} {customer.LastName} " +
                $"phone: {customer.Phone} Email: {customer.Email} Addess : {customer.Street} {customer.City} {customer.State} ");
            Console.WriteLine("-----------------------------------------------------");

            //Retrieve a list of product names and their corresponding brand names.
            Console.WriteLine("product names and their corresponding brand names : ");
            var productNameBrandNames = DbContext.Products.Include(e => e.Brand);
            foreach(var item in productNameBrandNames)
            {
                Console.WriteLine($"product ID : {item.ProductId} product Name : {item.ProductName} brand name : {item.Brand.BrandName}");
            }
            Console.WriteLine("-----------------------------------------------------");

            //Count the number of products in a specific category.
            var count = DbContext.Products.Where(e=>e.CategoryId == 5).Count();
            Console.WriteLine($"the number of products in a category=4 : {count}");
            Console.WriteLine("-----------------------------------------------------");

            //Calculate the total list price of all products in a specific category.
            var totalPriceOfcategory = DbContext.Products.Where(e => e.CategoryId == 5).Sum(e=>e.ListPrice);
            Console.WriteLine($"total Price Of category= 5 is {totalPriceOfcategory}$");
            Console.WriteLine("-----------------------------------------------------");

            //Calculate the average list price of products.
            var averagePriceOfProduct = DbContext.Products.Average(e => e.ListPrice);
            Console.WriteLine($"average price of products is {averagePriceOfProduct}$");
            Console.WriteLine("-----------------------------------------------------");

            //Retrieve orders that are completed.
            var ordersCompleted = DbContext.Orders.Where(e=>e.OrderStatus ==4);

            foreach(var item in ordersCompleted)
            {
                Console.WriteLine($"order ID:{item.OrderId} customer ID: {item.CustomerId} " +
                    $"order date: {item.OrderDate} ShippedDate:{item.ShippedDate} RequiredDate:{item.RequiredDate} " );
            }

        }
    }
}
