namespace ProductManagment.WebUI.Models
{
    public class CreateUserModel : UserModel
    {
        public string Password { get; set; } = null!;
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
