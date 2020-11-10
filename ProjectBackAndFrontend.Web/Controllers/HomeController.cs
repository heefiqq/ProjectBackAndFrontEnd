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
        private readonly int _pageSize = 2;

        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public ActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            return View();
        }

        [HttpGet]
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
            }).ToPagedList(page, _pageSize);

            return PartialView("_CustomersList", model);
        }

        public PartialViewResult Create()
        {
            var model = new CustomerModel();
            return PartialView("AddEdit", model);
        }

        public PartialViewResult Edit(int id)
        {
            var customer = _customerService.Get(id);

            var model = new CustomerModel
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Password = customer.Password,
                StandardPhone = customer.StandartPhone,
            };

            return PartialView("AddEdit", model);
        }

        [HttpPost]
        public JsonResult AddEdit(CustomerModel model)
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
        public JsonResult Delete(int id)
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
    }
}