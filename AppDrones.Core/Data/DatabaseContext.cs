using AppDrones.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppDrones.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Drone> Drone { get; set; }
        public string DbPath { get; }

        public DatabaseContext()
        {
            // var folder = Environment.SpecialFolder.ApplicationData;
            var path = Directory.GetParent(Environment.CurrentDirectory)!.FullName;
            DbPath = System.IO.Path.Join(path, "drones.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

    }
}
