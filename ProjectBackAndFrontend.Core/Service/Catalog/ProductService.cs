using ProjectBackAndFrontend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ProjectBackAndFrontend.Core.Service
{
    public class ProductService : IProductService, IDisposable
    {
        private ProjectBackAndFrontendEntities db = new ProjectBackAndFrontendEntities();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Create(Product product)
        {
            db.Product.Add(product);
            db.SaveChanges();
        }

        public Product Get(int Id)
        {
            return db.Product.AsNoTracking().Include(x => x.Offer).FirstOrDefault(x => x.Id == Id);
        }

        public List<Product> GetAll()
        {
            return db.Product.AsNoTracking().Include(x => x.Offer).ToList();
        }

        public void Update(Product product)
        {
            var productDb = db.Product.AsNoTracking().FirstOrDefault(x => x.Id == product.Id);

            if (productDb == null)
                return;

            productDb.Name = product.Name;
            productDb.ArtNo = product.ArtNo;
            productDb.Enabled = product.Enabled;
            productDb.Modified = DateTime.Now;

            db.Entry(productDb).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var productDb = db.Product.FirstOrDefault(x => x.Id == Id);

            db.Offer.RemoveRange(productDb.Offer);
            db.Product.Remove(productDb);
            db.SaveChanges();
        }
    }
}
