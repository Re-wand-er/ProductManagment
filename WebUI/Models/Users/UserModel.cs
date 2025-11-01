using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Логин обязателен для заполнения.")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Логин не может быть меньше 4 символов.")]
        public string Login { get; set; } = null!;
        public string? SystemRole { get; set; }

        [Required(ErrorMessage = "Email обязателен для заполнения.")]
        [EmailAddress(ErrorMessage = "Некорректный формат email.")]
        [StringLength(100, ErrorMessage = "Email не может превышать 100 символов.")]
        public string Email { get; set; } = null!;
        public bool IsBlocked { get; set; }

        public UserModel() { }  
    }
}
