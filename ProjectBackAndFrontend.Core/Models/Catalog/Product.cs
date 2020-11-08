using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBackAndFrontend.Core.Models
{
    [Table("Product", Schema = "Catalog")]
    public class Product
    {
        public Product()
        {
            Offer = new HashSet<Offer>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ArtNo { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<Offer> Offer { get; set; }
    }
}
