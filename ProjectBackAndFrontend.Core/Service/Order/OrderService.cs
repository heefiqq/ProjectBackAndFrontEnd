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

        public void Create(Order order)
        {
            db.Order.Add(order);
        }

        public Order Get(int Id)
        {
            return db.Order.AsNoTracking().Include(x => x.Offer).Include(x => x.Customer).FirstOrDefault(x => x.Id == Id);
        }

        public List<Order> GetAll()
        {
            return db.Order.AsNoTracking().Include(x => x.Offer).Include(x => x.Customer).ToList();
        }

        public void Update(Order order)
        {
            var orderDb = db.Order.AsNoTracking().FirstOrDefault(x => x.Id == order.Id);

            if (orderDb == null)
                return;

            orderDb.Number = order.Number;
            orderDb.PaymentDate = DateTime.Now;
            orderDb.Status = order.Status;
            orderDb.Sum = order.Sum;

            db.Entry(orderDb).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var offerDb = db.Offer.AsNoTracking().FirstOrDefault(x => x.Id == Id);

            if (offerDb == null)
                return;

            db.Offer.Remove(offerDb);
            db.SaveChanges();
        }
    }
}
