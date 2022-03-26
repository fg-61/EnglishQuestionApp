using System.ComponentModel.DataAnnotations;

namespace EnglishQuestionApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı alanı gereklidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}