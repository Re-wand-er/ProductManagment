using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class CreateUserModel : UserModel
    {
        [Required(ErrorMessage = "Пароль обязателен для заполнения.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Пароль должен быть минимум 8 символов.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо выбрать роль.")]
        public int SystemRoleId { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; } = [];

        public CreateUserModel() { }
        public CreateUserModel(UserWithPasswordModel user, IEnumerable<RoleModel>? roles) 
        {
            Id = user.Id;
            Login = user.Login;
            SystemRoleId = user.SystemRoleId;
            SystemRole = user.SystemRole ?? "";
            Email = user.Email;
            Roles = roles ?? [];
        }
    }
}
