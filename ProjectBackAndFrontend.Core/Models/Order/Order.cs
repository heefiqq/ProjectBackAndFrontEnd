using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBackAndFrontend.Core.Models
{
    [Table("Order", Schema = "Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Number { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Status { get; set; }

        public float Sum { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Offer> Offer { get; set; }
    }
}
