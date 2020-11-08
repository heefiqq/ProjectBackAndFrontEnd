using ProjectBackAndFrontend.Core.Models;
using System.Collections.Generic;

namespace ProjectBackAndFrontend.Core.Service
{
    public interface IOrderService
    {
        void Create(Order order);

        Order Get(int Id);

        List<Order> GetAll();

        void Update(Order order);

        void Delete(int Id);
    }
}
