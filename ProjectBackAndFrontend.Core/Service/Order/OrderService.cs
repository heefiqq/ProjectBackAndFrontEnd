using ProjectBackAndFrontend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ProjectBackAndFrontend.Core.Service
{
    public class OrderService : IOrderService, IDisposable
    {
        private ProjectBackAndFrontendEntities db = new ProjectBackAndFrontendEntities();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Create(Order order, List<int> offerIds)
        {
            order.Offer.Clear();
            foreach (var offerId in offerIds)
            {
                var offer = db.Offer.SingleOrDefault(x => x.Id == offerId);
                order.Offer.Add(offer);
            }

            db.Order.Add(order);
            db.SaveChanges();
        }

        public Order Get(int Id)
        {
            return db.Order.Include(x => x.Offer).Include(x => x.Customer).SingleOrDefault(x => x.Id == Id);
        }

        public List<Order> GetAll()
        {
            return db.Order.Include(x => x.Offer).Include(x => x.Customer).ToList();
        }

        public void Edit(Order order, List<int> offerIds)
        {
            var orderDb = db.Order.Include(x => x.Offer).FirstOrDefault(x => x.Id == order.Id);

            orderDb.Number = order.Number;
            orderDb.PaymentDate = order.PaymentDate;
            orderDb.Status = order.Status;
            orderDb.Sum = order.Sum;

            orderDb.Offer.Clear();
            foreach (var offerId in offerIds)
            {
                var offer = db.Offer.SingleOrDefault(x => x.Id == offerId);
                orderDb.Offer.Add(offer);
            }

            db.Entry(orderDb).State = EntityState.Modified;
            db.SaveChanges();
        }


        public void Delete(int Id)
        {
            var orderDb = db.Order.FirstOrDefault(x => x.Id == Id);

            if (orderDb == null)
                return;

            orderDb.Offer.Clear();
            db.Order.Remove(orderDb);
            db.SaveChanges();
        }
    }
}
