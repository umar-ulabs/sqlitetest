using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (var ctx = new ProductDbContext())
            {
                
                var products = ctx.Products.ToListAsync().Result;
                var toDelete = products.FirstOrDefault(x=>x.Id==1);
                ctx.Products.Remove(toDelete);
                ctx.SaveChanges();
              
            }
            using (var ctx = new ProductDbContext())
            {
                
                var products = ctx.Products.ToListAsync().Result;
                
            }
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class ProductDbContext : DbContext
    {
        #region Constructor
        public ProductDbContext() : base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public DbSet<Product> Products { get; set; }
        #endregion

        #region Overridden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(GetProducts());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Product.db");
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Private methods
        private Product[] GetProducts()
        {
            return new Product[]
            {
            new Product { Id = 1, Name = "TShirt"},
            new Product { Id = 2, Name = "Shirt"},
            new Product { Id = 3, Name = "Socks"},
            new Product { Id = 4, Name = "Tshirt" }
            };
        }
        #endregion
    }



}
