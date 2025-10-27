namespace ProductManagment.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsBlocked { get; set; } 

        public Role Role { get; set; } = null!;
    }
}
