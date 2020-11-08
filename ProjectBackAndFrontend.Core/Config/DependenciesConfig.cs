using Ninject.Modules;
using ProjectBackAndFrontend.Core.Service;

namespace ProjectBackAndFrontend.Core.Config
{
    public class DependenciesConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerService>().To<CustomerService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IOfferService>().To<OfferService>();
            Bind<IOrderService>().To<OrderService>();
        }
    }
}
