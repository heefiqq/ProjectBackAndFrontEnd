using ProjectBackAndFrontend.Core.Extensions;
using ProjectBackAndFrontend.Core.Models;
using ProjectBackAndFrontend.Core.Service;
using ProjectBackAndFrontend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBackAndFrontend.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOfferService _offerService;
        private readonly int _pageSize = 2;

        public ProductController(IProductService productService, IOfferService offerService)
        {
            _productService = productService;
            _offerService = offerService;
        }

        public ActionResult Index(int page = 1)
        {
            ViewBag.Page = page;
            return View();
        }

        #region product

        public PartialViewResult CreateProduct()
        {
            var model = new ProductModel();
            return PartialView("_AddEditProduct", model);
        }

        public PartialViewResult EditProduct(int id)
        {
            var product = _productService.Get(id);

            var model = new ProductModel
            {
                Id = product.Id,
                ArtNo = product.ArtNo,
                Name = product.Name,
                Created = product.Created,
                Modified = product.Modified,
                Enabled = product.Enabled
            };

            return PartialView("_AddEditProduct", model);
        }

        [HttpPost]
        public JsonResult AddEditProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { result = false, errorMessage = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            try
            {
                var product = new Product
                {
                    Id = model.Id,
                    ArtNo = model.ArtNo,
                    Name = model.Name,
                    Enabled = model.Enabled,
                    Modified = DateTime.Now
                };

                if (product.Id == 0)
                {
                    product.Created = DateTime.Now;
                    _productService.Create(product);
                }
                else
                    _productService.Update(product);

                return Json(new { result = true });
            }
            catch
            {
                return Json(new { result = false, errorMessage = "Ошбика при сохранении." });
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                _productService.Delete(id);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = "Ошибка при удалении." });
            }
        }

        public PartialViewResult GetProducts(int page = 1)
        {
            var products = _productService.GetAll();
            var model = products.Select(x => new ProductModel
            {
                Id = x.Id,
                ArtNo = x.ArtNo,
                Name = x.Name,
                Created = x.Created,
                Modified = x.Modified,
                Enabled = x.Enabled
            }).OrderBy(x => x.Id).ToPagedList(page, _pageSize);

            return PartialView("_ProductsList", model);
        }

        #endregion

        #region offer

        public PartialViewResult CreateOffer(int ProductId)
        {
            var model = new OfferModel()
            {
                ProductId = ProductId
            };
            return PartialView("_AddEditOffer", model);
        }

        public PartialViewResult EditOffer(int id)
        {
            var offer = _offerService.Get(id);

            var model = new OfferModel
            {
                Id = offer.Id,
                ProductId = offer.ProductId,
                Amount = offer.Amount,
                Price = offer.Price,
                ArtNo = offer.ArtNo,
                Main = offer.Main
            };

            return PartialView("_AddEditOffer", model);
        }

        [HttpPost]
        public JsonResult AddEditOffer(OfferModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { result = false, errorMessage = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            try
            {
                var offer = new Offer
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    Amount = model.Amount,
                    Price = model.Price,
                    ArtNo = model.ArtNo,
                    Main = model.Main
                };

                if (offer.Id == 0)
                    _offerService.Create(offer);
                else
                    _offerService.Update(offer);

                return Json(new { result = true });
            }
            catch
            {
                return Json(new { result = false, errorMessage = "Ошбика при сохранении." });
            }
        }

        [HttpPost]
        public JsonResult DeleteOffer(int id)
        {
            try
            {
                _offerService.Delete(id);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = "Ошибка при удалении." });
            }
        }

        public PartialViewResult GetOffers(int ProductId, int page = 1)
        {
            var offers = _offerService.GetAll().Where(x => x.ProductId == ProductId);
            var model = offers.Select(x => new OfferModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Amount = x.Amount,
                Price = x.Price,
                ArtNo = x.ArtNo,
                Main = x.Main
            }).OrderBy(x => x.Id).ToPagedList(page, _pageSize);

            return PartialView("_OffersList", model);
        }

        #endregion
    }
}
