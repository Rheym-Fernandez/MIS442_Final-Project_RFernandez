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
           // dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
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
            //Creates a new recipe named test
            r = new Recipe();
            r.Name = "Test Recipe";
            r.Version = 1;
            r.Date = null;
            r.StyleId = 70;
            r.Volume = 30;
            r.Brewer = "Test Brewer";
            r.BoilTime = 60;
            r.BoilVolume = 45;
            r.Efficiency = 70;
            r.FermentationStages = 1;
            r.EstimatedOg = 20;
            r.EstimatedFg = 15;
            r.EstimatedColor = 8;
            r.EstimatedAbv = 7;
            r.ActualEfficiency = 50;
            r.EquipmentId = 1;
            r.CarbonationUsed =  null;
            r.ForcedCarbonation = null;
            r.KegPrimingFactor = null;
            r.CarbonationTemp = null;
            r.MashId = 15;
            dbContext.Recipes.Add(r);
            dbContext.SaveChanges();
            Assert.NotNull(dbContext.Ingredients.Where(r => r.Name == "Test" && r.Version == 1));
        }

        [Test]
        public void UpdateTest()
        {
            r = dbContext.Recipes.Find(1);
            r.Name = "Fuzzy Tales Juicy IPA";
            dbContext.Recipes.Update(r);
            dbContext.SaveChanges();
            r = dbContext.Recipes.Find(1);
            Assert.AreEqual("Fuzzy Tales Juicy IPA", r.Name);
        }

        [Test]
        public void DeleteTest()
        {
            //First finds the RecipeId of 8 then removes it from the Recipes table
            r = dbContext.Recipes.Find(9);
            dbContext.Recipes.Remove(r);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Recipes.Find(9));
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
