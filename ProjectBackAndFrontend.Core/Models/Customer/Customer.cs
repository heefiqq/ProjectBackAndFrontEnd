using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBackAndFrontend.Core.Models
{
    [Table("Customer", Schema = "Customer")]
    public class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(70)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70)]
        public string LastName { get; set; }

        [Required]
        [StringLength(16)]
        public string StandartPhone { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
