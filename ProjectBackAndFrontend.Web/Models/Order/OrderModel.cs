using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBackAndFrontend.Web.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Поле \"Номер заказа\" обязательно для заполнения")]
        public string Number { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required(ErrorMessage = "Поле \"Статус\" обязательно для заполнения")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Поле \"Cумма\" обязательно для заполнения")]
        public float Sum { get; set; }

        public List<SelectListItem> OffersArtNo { get; set; }
    }
}