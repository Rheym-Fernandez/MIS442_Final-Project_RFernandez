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

        public void PrintAll(List<Ingredient> ingredients)
        {
            foreach (Ingredient i in ingredients)
            {
                Console.WriteLine(i);
            }
        }

    }
}