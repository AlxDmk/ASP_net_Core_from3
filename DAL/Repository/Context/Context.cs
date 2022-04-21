using Lesson3.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lesson3.DAL.Repository.Context
{
    public class Context:DbContext
    {
        public DbSet<PersonEntity> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source=mydb.db");

        internal object Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
