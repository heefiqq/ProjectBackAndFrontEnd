using ProjectBackAndFrontend.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectBackAndFrontend.Core.Service
{
    public class OfferService : IOfferService, IDisposable
    {
        private ProjectBackAndFrontendEntities db = new ProjectBackAndFrontendEntities();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Create(Offer offer)
        {
            db.Offer.Add(offer);
            db.SaveChanges();
        }

        public Offer Get(int Id)
        {
            return db.Offer.AsNoTracking().Include(x => x.Product).SingleOrDefault(x => x.Id == Id);
        }

        public List<Offer> GetAll()
        {
            return db.Offer.AsNoTracking().Include(x => x.Product).ToList();
        }

        public void Update(Offer offer)
        {
            var offerDb = db.Offer.AsNoTracking().FirstOrDefault(x => x.Id == offer.Id);

            if (offerDb == null)
                return;

            offerDb.Main = offer.Main;
            offerDb.ArtNo = offer.ArtNo;
            offerDb.Amount = offer.Amount;
            offerDb.Price = offer.Price;

            db.Entry(offerDb).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var offerDb = db.Offer.AsNoTracking().Single(x => x.Id == Id);

            db.Offer.Attach(offerDb);
            db.Offer.Remove(offerDb);
            db.SaveChanges();
        }
    }
}
