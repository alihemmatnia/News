using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.ViewModel.Account.Login
{
    public record LoginViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "لطفا پسورد خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool Remem { get; set; }
    }
}
