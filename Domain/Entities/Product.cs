namespace ProductManagment.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public string? GeneralNote { get; set; }
        public string? SpecialNote { get; set; }

        public Category Category { get; set; } = null!;
    } 
}
