namespace ProductManagment.Application.DTOs
{
    public record class UserDTO
    (
        int Id,
        string Login,
        string SystemRole,
        string Email,
        bool IsBlocked
    );
}
