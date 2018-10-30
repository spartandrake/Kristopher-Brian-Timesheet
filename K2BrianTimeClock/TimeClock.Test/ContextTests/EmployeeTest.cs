using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using K2BrianTimeClock.DAL.EF;
using TimeClock.Models.Entities;


namespace TimeClock.Test.ContextTests
{
    [Collection("K2BrianTimeClock.DAL")]
    public class EmployeeTest: IDisposable
    {
        private readonly ClockContext _db;
        public EmployeeTest()
        {
            _db = new ClockContext();
            CleanDatabase();
        }

        public void Dispose()
        {
            CleanDatabase();
            _db.Dispose();
        }

        private void CleanDatabase()
        {
            _db.Database.ExecuteSqlCommand("Delete from Store.Categories");
            _db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"Store.Categories\", RESEED, -1);");
        }

        [Fact]
        public void FirstTest()
        {
            Assert.True(true);
        }
    }
}
