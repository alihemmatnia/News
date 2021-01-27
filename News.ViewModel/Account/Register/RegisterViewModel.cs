using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.ViewModel.Account.Register
{
    public record RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری ")]
        [MinLength(4)]
        [MaxLength(12)]
        public string Username { get; set; }
        [MinLength(3)]
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا پسورد خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا تکرا  پسورد را وارد کنید")]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "پسورد ها یکسان نیستند")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
