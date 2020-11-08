using ProjectBackAndFrontend.Core.Models;
using System.Collections.Generic;

namespace ProjectBackAndFrontend.Core.Service
{
    public interface IProductService
    {
        void Create(Product product);

        Product Get(int Id);

        List<Product> GetAll();

        void Update(Product product);

        void Delete(int Id);
    }
}
