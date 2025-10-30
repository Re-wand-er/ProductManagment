namespace ProductManagment.Application.DTOs
{
    public record class ProductDTO
    (
        int Id,
        string Name,
        int CategoryId,
        string? Category,
        string? Description,
        decimal Cost,
        string? GeneralNote,
        string? SpecialNote
    );
}
