namespace ProductManagment.WebUI.Models
{
    public class CreateUserModel : UserModel
    {
        public string Password { get; set; } = null!;
        public int SystemRoleId { get; set; }
        public List<RoleModel>? Roles { get; set; }
    }
}
