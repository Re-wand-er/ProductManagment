using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения.")]
        [StringLength(30, ErrorMessage = "Логин не может превышать 100 символов.")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен для заполнения.")]
        [StringLength(255, ErrorMessage = "Пароль не может превышать 255 символов.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool IsBlocked { get; set; }
    }
}
