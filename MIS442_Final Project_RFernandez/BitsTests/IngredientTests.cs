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

        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            //dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

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
            i.IngredientTypeId = 4;
            i.OnHandQuantity = 10;
            i.UnitTypeId = 3;
            i.UnitCost = 5;
            i.ReorderPoint = 0;
            i.Notes = "Test";
            dbContext.Ingredients.Add(i);
            dbContext.SaveChanges();
            Assert.NotNull(dbContext.Ingredients.Where(i => i.Name == "Test" && i.Version == 1));
        }

        [Test]
        public void UpdateTest()
        {
            i = dbContext.Ingredients.Find(1149);
            i.Version = 1;
            dbContext.Ingredients.Update(i);
            dbContext.SaveChanges();
            i = dbContext.Ingredients.Find(1149);
            Assert.AreEqual(1, i.Version);
        }

        [Test]
        public void DeleteTest()
        {
            //First finds the IngredientId of 1151 then removes it from the Ingredients table
            i = dbContext.Ingredients.Find(1152);
            dbContext.Ingredients.Remove(i);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Ingredients.Find(1152));
        }

        [Test]
        public void GetWithCalculatedFieldTest()
        {
            //This will get me the IngredientId and how many on hand I have for each ingredient; furthermore, it will also
            //get me the total unit cost for each group of ingredients.
            var ingredients = dbContext.Ingredients.Select(
            i => new { i.IngredientId, i.OnHandQuantity, i.UnitCost, Value = i.UnitCost * i.OnHandQuantity }).
            OrderBy(i => i.IngredientId).ToList();
            Assert.AreEqual(1149, ingredients.Count);
            foreach (var i in ingredients)
            {
                Console.WriteLine(i);
            }
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