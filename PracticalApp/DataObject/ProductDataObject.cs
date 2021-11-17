//using PracticalApp.Class;
using PracticalApp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.DataObject
{
    public class ProductDataObject
    {
        public List<Product> GetAll()
        {    
            List<Product> product = new List<Product>();
            using (var context = new DemoAppContext())
            {
                product = context.Products.Select(x => x).ToList();
            }

            return product;
        }

        public long Insert(Product product)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                context.Products.Add(product);
                Id = context.SaveChanges();
            }

            return Id;
        }

        public long Update(Product product)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                var productData = context.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                productData.ProductName = product.ProductName;
                productData.Quantity = product.Quantity;
                productData.Price = product.Price;
                productData.isDeleted = false;
                Id = context.SaveChanges();
            }

            return Id;
        }

        public long Delete(long ProductId)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                var productData = context.SignInDetails.Where(x => x.id == ProductId).FirstOrDefault();
                productData.isDeleted = true;
                Id = context.SaveChanges();
            }

            return Id;
        }
    }
}
