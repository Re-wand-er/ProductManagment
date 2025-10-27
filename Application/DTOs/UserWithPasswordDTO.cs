namespace ProductManagment.Application.DTOs
{
    public record class UserWithPasswordDTO
    (
        int Id,
        string Login,
        int SystemRoleId,
        string SystemRole,
        string Email,
        string Password
    );
}
