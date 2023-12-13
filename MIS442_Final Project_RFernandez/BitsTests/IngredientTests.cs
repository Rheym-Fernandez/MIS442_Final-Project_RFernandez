using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BitsEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BitsTests
{
    [TestFixture]
    public class IngredientTests
    {
        BitsContext dbContext;
        Ingredient? i;
        List<Ingredient>? ingredients;


        [Test]
        public void GetAllTest()
        {
            //This asserts that when retrieving data, there should be a total of 1149 results for the ingredient id.
            ingredients = dbContext.Ingredients.OrderBy(i => i.IngredientId).ToList();
            Assert.AreEqual(1149, ingredients.Count);
            Assert.AreEqual(1, ingredients[0].IngredientId);
            PrintAll(ingredients);
        }

        [Test]
        public void CreateTest()
        { 
            i = new Ingredient();
            i.Name = "Test";
            i.Version = 1;
            i.IngredientTypeId = 3;
            i.OnHandQuantity = 0;
            i.UnitTypeId = 3;
            i.UnitCost = 0;
            i.ReorderPoint = 0;
            i.Notes = "Test";
            dbContext.Ingredients.Add(i);
            dbContext.SaveChanges();
            Assert.NotNull(dbContext.Ingredients.Where(i => i.Name == "Test" && i.Version == 1));
        }

        [Test]
        public void UpdateTest()
        {
            c = dbContext.Customers.Find(2);
            c.Address = "Test";
            dbContext.Customers.Update(c);
            dbContext.SaveChanges();
            c = dbContext.Customers.Find(2);
            Assert.AreEqual("Test", c.Address);
        }

        [Test]
        public void DeleteTest()
        {
            //First finds the IngredientId of 1149 then removes it from the Ingredients table
            i = dbContext.Ingredients.Find(1149);
            dbContext.Ingredients.Remove(i);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Ingredients.Find(1149));
        }

        /* [Test]
         public void GetByPrimaryKeyTest()
         {
             //This code gets the product that has a code of A4VB
             //Assert.AreEqual("Murach's ASP.NET 4 Web Programming with VB 2010", p.Description) means that
             //the code "A4VB" is equivalent to the product description of "Murach's ASP.NET 4 Web
             //Programming with VB 2010"
             p = dbContext.Products.Find("A4VB");
             Assert.IsNotNull(p);
             Assert.AreEqual("Murach's ASP.NET 4 Web Programming with VB 2010", p.Description);
             Console.WriteLine(p);
         }

         [Test]
         public void GetUsingWhere()
         {
             //Get a list of all of the products that have a unit price of 56.50
             //First line gets the code of all products that have a cost of 56.50; then outputting it to a list.
             //Based on the MYSQL query, I am asserting that there should be 7 products returned with a price of 56.5
             products = dbContext.Products.Where(p => p.UnitPrice.Equals(56.5m)).OrderBy(p => p.ProductCode).ToList();
             Assert.AreEqual(7, products.Count);
             Assert.AreEqual("A4CS", products[0].ProductCode);
             PrintAll(products);
         }

         [Test]
         public void GetWithCalculatedFieldTest()
         {
             // get a list of objects that include the productcode, unitprice, quantity and inventoryvalue
             var products = dbContext.Products.Select(
             p => new { p.ProductCode, p.UnitPrice, p.OnHandQuantity, Value = p.UnitPrice * p.OnHandQuantity }).
             OrderBy(p => p.ProductCode).ToList();
             Assert.AreEqual(16, products.Count);
             foreach (var p in products)
             {
                 Console.WriteLine(p);
             }
         }


         [Test]
         public void DeleteTest()
         {   //NEEDS TO BE FIXED BECAUSE IT CANNOT WORK WIRH FOREIGN KEY CONSTRAINTS.
             //This deletes the Product with the ProductCode A4VB
             //Save Changes is necessary
             //Issue with the foreign key constraints
             p = dbContext.Products.Find("ABCD");
             dbContext.Products.Remove(p);
             dbContext.SaveChanges();
             Assert.IsNull(dbContext.Products.Find("ABCD"));
         }

         [Test]
         public void CreateTest()
         {
             //Must add first, then save.
             //Then Assert that Product is not null, and look for it to make sure
             //it was created.
             p = new Product();
             p.ProductCode = "ABCD";
             p.Description = "New Product";
             dbContext.Products.Add(p);
             dbContext.SaveChanges();
             Assert.IsNotNull(dbContext.Products.Find("ABCD"));
         }

         [Test]
         public void UpdateTest()
         {
             p = dbContext.Products.Find("A4CS");
             p.Description = "Test";
             dbContext.Products.Update(p);
             dbContext.SaveChanges();
             p = dbContext.Products.Find("A4CS");
             Assert.AreEqual("Test", p.Description);

         }
        */
        public void PrintAll(List<Ingredient> ingredients)
        {
            foreach (Ingredient i in ingredients)
            {
                Console.WriteLine(i);
            }
        }

    }
}