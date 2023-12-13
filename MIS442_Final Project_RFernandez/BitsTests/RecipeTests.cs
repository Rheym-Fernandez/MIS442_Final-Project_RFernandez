using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BitsEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BitsTests
{
    public class RecipeTests
    {
        BitsContext dbContext;
        Recipe? r;
        List<Recipe>? recipes;

        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            //This asserts that when retrieving data, there should be a total of 1149 results for the ingredient id.
            recipes = dbContext.Recipes.OrderBy(r => r.RecipeId).ToList();
            Assert.AreEqual(4, recipes.Count);
            Assert.AreEqual(1, recipes[0].RecipeId);
            PrintAll(recipes);
        }

        [Test]
        public void CreateTest()
        {
            r = new Recipe();
            r.Name = "Test Recipe";
            r.Version = 1;
            r.Date = null;
            r.StyleId = 1000;
            r.Volume = 30;
            r.Brewer = "Test Brewer";
            r.BoilTime = "Test Brewer";
            r.BoilVolume = "Test Brewer";
            r.Efficiency = "Test Brewer";
            r.FermentationStages = "Test Brewer";
            r.EstimatedOg = "Test Brewer";
            r.EstimatedFg = "Test Brewer";
            r.EstimatedColor = "Test Brewer";
            r.EstimatedAbv = "Test Brewer";
            r.ActualEfficiency = "Test Brewer";
            r.EquipmentId = "Test Brewer";
            r.CarbonationUsed = "Test Brewer";
            r.ForcedCarbonation = "Test Brewer";
            r.KegPrimingFactor = "Test Brewer";
            r.CarbonationTemp = "Test Brewer";
            r.MashId = "Test Brewer";
            dbContext.Recipes.Add(r);
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

        public void PrintAll(List<Recipe> recipes)
        {
            foreach (Recipe r in recipes)
            {
                Console.WriteLine(r);
            }
        }
    }
}
