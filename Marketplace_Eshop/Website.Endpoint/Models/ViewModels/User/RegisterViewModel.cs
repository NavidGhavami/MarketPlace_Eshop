using System.ComponentModel.DataAnnotations;

namespace Website.EndPoint.Models.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید")]
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "نام و نام خانوادگی نباید بیشتر از 100 کاراکتر باشد")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبور و تکرار آن برابر نمی باشند")]
        [Display(Name = "تکرار کلمه عبور")]
        public string RePassword { get; set; }


        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }

    }
}
