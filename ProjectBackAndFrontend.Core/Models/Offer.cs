using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBackAndFrontend.Core.Models
{
    [Table("Offer", Schema = "Catalog")]
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public float Amount { get; set; }

        public float Price { get; set; }

        [Required]
        [StringLength(100)]
        public string ArtNo { get; set; }

        public bool Main { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
