using ProjectBackAndFrontend.Core;
using ProjectBackAndFrontend.Core.Models;
using ProjectBackAndFrontend.Core.Service;
using ProjectBackAndFrontend.Core.Extensions;
using ProjectBackAndFrontend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBackAndFrontend.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IOfferService _offerService;
        private readonly int _pageSize = 2;

        public HomeController(ICustomerService customerService, IOrderService orderService, IOfferService offerService)
        {
            _customerService = customerService;
            _orderService = orderService;
            _offerService = offerService;
        }

        public ActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            return View();
        }

        #region customer

        public PartialViewResult CreateCustomer()
        {
            var model = new CustomerModel();
            return PartialView("_AddEditCustomer", model);
        }

        public PartialViewResult EditCustomer(int id)
        {
            var customer = _customerService.Get(id);

            var model = new CustomerModel
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Password = customer.Password,
                StandardPhone = customer.StandartPhone
            };

            return PartialView("_AddEditCustomer", model);
        }

        [HttpPost]
        public JsonResult AddEditCustomer(CustomerModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { result = false, errorMessage = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            try
            {
                var customer = new Customer
                {
                    Id = model.CustomerId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    StandartPhone = model.StandardPhone
                };

                if (customer.Id == 0)
                {
                    customer.RegistrationDateTime = DateTime.Now;
                    _customerService.Create(customer);
                }
                else
                    _customerService.Edit(customer);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = "Ошбика при сохранении." });
            }
        }

        [HttpPost]
        public JsonResult DeleteCustomer(int id)
        {
            try
            {
                _customerService.Delete(id);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = "Ошибка при удалении." });
            }
        }


        public PartialViewResult GetCustomers(int page = 1)
        {
            var customers = _customerService.GetAll();
            var model = customers.Select(x => new CustomerModel
            {
                CustomerId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                StandardPhone = x.StandartPhone,
                RegistrationDate = x.RegistrationDateTime
            }).OrderBy(x => x.CustomerId).ToPagedList(page, _pageSize);

            return PartialView("_CustomersList", model);
        }

        #endregion

        #region orders

        public PartialViewResult CreateOrder(int CustomerId)
        {
            var model = new OrderModel()
            {
                CustomerId = CustomerId
            };

            var offers = _offerService.GetAll();
            model.OffersArtNo = offers.Select(x => new SelectListItem { Text = string.Format("{0} ({1})", x.Product.Name, x.ArtNo), Value = x.Id.ToString(), Selected = false }).ToList();

            return PartialView("_AddEditOrder", model);
        }

        public PartialViewResult EditOrder(int id)
        {
            var order = _orderService.Get(id);

            var model = new OrderModel
            {
                CustomerId = order.CustomerId,
                OrderId = order.Id,
                Number = order.Number,
                OrderDate = order.OrderDate,
                PaymentDate = order.PaymentDate,
                Status = order.Status,
                Sum = order.Sum,
            };

            var offers = _offerService.GetAll();
            model.OffersArtNo = offers.Select(x => new SelectListItem { Text = string.Format("{0} ({1})", x.Product.Name, x.ArtNo), Value = x.Id.ToString(), Selected = order.Offer.Any(y => y.Id == x.Id) }).ToList();

            return PartialView("_AddEditOrder", model);
        }

        [HttpPost]
        public JsonResult AddEditOrder(OrderModel model, List<int> offerListIds)
        {
            if (offerListIds == null || !offerListIds.Any())
                ModelState.AddModelError("offers", "Состав заказа не может быть пустым. ");
            if (!ModelState.IsValid)
                return Json(new { result = false, errorMessage = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            try
            {
                var order = new Order
                {
                    Id = model.OrderId,
                    Number = model.Number,
                    PaymentDate = model.PaymentDate,
                    Status = model.Status,
                    Sum = model.Sum,
                    CustomerId = model.CustomerId
                };
                if (model.OrderId == 0)
                {
                    order.OrderDate = DateTime.Now;
                    _orderService.Create(order, offerListIds);
                }
                else
                    _orderService.Edit(order, offerListIds);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = "Ошбика при сохранении." });
            }
        }

        [HttpPost]
        public JsonResult DeleteOrder(int id)
        {
            try
            {
                _orderService.Delete(id);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = "Ошибка при удалении." });
            }
        }

        public PartialViewResult GetOrders(int CustomerId, int page = 1)
        {
            var orders = _orderService.GetAll().Where(x => x.CustomerId == CustomerId);
            var model = orders.Select(x => new OrderModel
            {
                OrderId = x.Id,
                Number = x.Number,
                OrderDate = x.OrderDate,
                PaymentDate = x.PaymentDate,
                Status = x.Status,
                Sum = x.Sum,
                OffersArtNo = x.Offer.Select(y => new SelectListItem { Text = string.Format("{0} ({1})", y.Product.Name, y.ArtNo), Value = y.Id.ToString(), Selected = false }).ToList()
            }).OrderBy(x => x.OrderId).ToPagedList(page, _pageSize);

            return PartialView("_OrdersList", model);
        }

        #endregion
    }
}