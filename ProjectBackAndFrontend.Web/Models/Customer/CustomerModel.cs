using ProjectBackAndFrontend.Core.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectBackAndFrontend.Web.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Поле \"Имя\" обязательно для заполнения.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле \"Фамилия\" обязательно для заполнения.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" обязательно для заполнения.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле \"Телефон\" обязательно для заполнения.")]
        public string StandardPhone { get; set; }

        [Required(ErrorMessage = "Поле \"Email\" обязательно для заполнения.")]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}