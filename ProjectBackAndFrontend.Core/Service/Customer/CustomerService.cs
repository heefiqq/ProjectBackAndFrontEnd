using ProjectBackAndFrontend.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectBackAndFrontend.Core.Service
{
    public class CustomerService : ICustomerService, IDisposable
    {
        private ProjectBackAndFrontendEntities db = new ProjectBackAndFrontendEntities();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Create(Customer customer)
        {
            db.Customer.Add(customer);
            db.SaveChanges();
        }

        public Customer Get(Guid Id)
        {
            return db.Customer.AsNoTracking().Include(x => x.Order).FirstOrDefault(x => x.Id == Id);
        }

        public List<Customer> GetAll()
        {
            return db.Customer.AsNoTracking().Include(x => x.Order).ToList();
        }

        public void Edit(Customer customer)
        {
            var customerDb = db.Customer.AsNoTracking().FirstOrDefault(x => x.Id == customer.Id);

            if (customerDb == null)
                return;

            customerDb.FirstName = customer.FirstName;
            customerDb.LastName = customer.LastName;
            customerDb.Password = customer.Password;
            customerDb.Email = customer.Email;
            customerDb.StandartPhone = customer.StandartPhone;

            db.Entry(customerDb).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var customerDb = db.Customer.AsNoTracking().FirstOrDefault(x => x.Id == Id);

            if (customerDb == null)
                return;

            db.Customer.Remove(customerDb);
            db.SaveChanges();
        }
    }
}
