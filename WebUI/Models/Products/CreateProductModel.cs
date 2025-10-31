namespace ProductManagment.WebUI.Models
{
    public class CreateProductModel : ProductModel
    {
        public int CategoryId { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; } = [];

        public CreateProductModel() { }
        public CreateProductModel(ProductWithCategoryIdModel product, IEnumerable<CategoryModel> categories)
        {
            Id = product.Id;
            Name = product.Name;
            CategoryId = product.CategoryId;
            Category = product.Category;    
            Description = product.Description;
            Cost = product.Cost;
            GeneralNote = product.GeneralNote;
            SpecialNote = product.SpecialNote;
            Categories = categories;
        }
    }
}
