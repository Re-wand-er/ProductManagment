namespace ProductManagment.WebUI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string SystemRole { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsBlocked { get; set; }

        public UserModel() { }  
    }
}
