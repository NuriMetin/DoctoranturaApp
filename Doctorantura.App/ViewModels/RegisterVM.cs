using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using Doctorantura.App.Models;

namespace Doctorantura.App.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(75, ErrorMessage = "İcazə verilən maksimum simvol sayı 75")]
        [MinLength(3, ErrorMessage = "İcazə verilən minimum simvol sayı 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(75, ErrorMessage = "İcazə verilən maksimum simvol sayı 75")]
        [MinLength(3, ErrorMessage = "İcazə verilən minimum simvol sayı 3")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(75, ErrorMessage = "İcazə verilən maksimum simvol sayı 75")]
        [MinLength(3, ErrorMessage = "İcazə verilən minimum simvol sayı 3")]
        public string FatherName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Daxil edilmiş məlumat 'email' formatında deyil!!")]
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(150, ErrorMessage = "İcazə verilən maksimum simvol sayı 150")]
        [MinLength(10, ErrorMessage = "İcazə verilən minimum simvol sayı 10")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(30, ErrorMessage = "İcazə verilən maksimum simvol sayı 30")]
        [MinLength(6, ErrorMessage = "İcazə verilən minimum simvol sayı 6")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolla təstiq parolu eyni deyil!!")]
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(30, ErrorMessage = "İcazə verilən maksimum simvol sayı 30")]
        [MinLength(6, ErrorMessage = "İcazə verilən minimum simvol sayı 6")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Boş ola bilməz")]
        public int GenderId { get; set; }
        public IEnumerable<Gender> Genders { get; set; }

        [Required(ErrorMessage = "Boş ola bilməz")]
        public DateTime Born { get; set; }

        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
