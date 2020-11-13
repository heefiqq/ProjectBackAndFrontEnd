using System.ComponentModel.DataAnnotations;

namespace ProjectBackAndFrontend.Web.Models
{
    public class OfferModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Поле \"Количество\" обязательно для заполнения.")]
        public float Amount { get; set; }

        [Required(ErrorMessage = "Поле \"Цена\" обязательно для заполнения.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Поле \"Артикул\" обязательно для заполнения.")]
        public string ArtNo { get; set; }

        public bool Main { get; set; }
    }
}