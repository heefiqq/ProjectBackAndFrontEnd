using ProjectBackAndFrontend.Core.Models;
using System;
using System.Collections.Generic;

namespace ProjectBackAndFrontend.Core.Service
{
    public interface ICustomerService
    {
        void Create(Customer customer);

        Customer Get(Guid Id);

        List<Customer> GetAll();

        void Edit(Customer customer);

        void Delete(Guid Id);
    }
}
