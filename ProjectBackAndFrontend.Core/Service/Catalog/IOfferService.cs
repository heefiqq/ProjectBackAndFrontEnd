using ProjectBackAndFrontend.Core.Models;
using System.Collections.Generic;

namespace ProjectBackAndFrontend.Core.Service
{
    public interface IOfferService
    {
        void Create(Offer offer);

        Offer Get(int Id);

        List<Offer> GetAll();

        void Update(Offer offer);

        void Delete(int Id);
    }
}
