using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectBackAndFrontend.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле \"Артикул\" обязательно для заполнения")]
        public string ArtNo { get; set; }

        [Required(ErrorMessage = "Поле \"Название\" обязательно для заполнения")]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool Enabled { get; set; }
    }
}