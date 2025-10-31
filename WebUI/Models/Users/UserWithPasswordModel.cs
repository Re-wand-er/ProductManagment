namespace ProductManagment.WebUI.Models
{
    public record class UserWithPasswordModel
    (
        int Id,
        string Login,
        int SystemRoleId,
        string? SystemRole,
        string Email,
        string Password
    );
}
